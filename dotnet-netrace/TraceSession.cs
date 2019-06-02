using System;
using System.Collections.Generic;
using System.Threading;
using LowLevelDesign.NTrace.EventHandlers;
using LowLevelDesign.NTrace.Utilities;
using Microsoft.Diagnostics.Tracing.Parsers;
using PInvoke;

// ReSharper disable AccessToDisposedClosure

namespace LowLevelDesign.NTrace
{
    internal sealed class TraceSession
    {
        public class TraceOptions
        {
            public bool PrintPacketBytes { get; set; }

            public bool TraceChildProcesses { get; set; }
        }

        private const string WinTraceUserTraceSessionName = "ntrace-events";

        private readonly ManualResetEvent stopEvent = new ManualResetEvent(false);
        private readonly ITraceOutput traceOutput;
        private Action stopTraceCollectors;

        public TraceSession(ITraceOutput traceOutput)
        {
            this.traceOutput = traceOutput;
        }

        public void TraceNewProcess(IEnumerable<string> procargs, bool spawnNewConsoleWindow, TraceOptions traceOptions)
        {
            using (var process = new ProcessCreator(procargs) {SpawnNewConsoleWindow = spawnNewConsoleWindow}) {
                process.StartSuspended();

                using (var kernelTraceCollector = new TraceCollector(KernelTraceEventParser.KernelSessionName))
                using (var customTraceCollector = new TraceCollector(WinTraceUserTraceSessionName)) {
                    InitializeProcessHandlers(kernelTraceCollector, customTraceCollector,
                        process.ProcessId, traceOptions);

                    ThreadPool.QueueUserWorkItem((o) => {
                        process.Join();
                        StopCollectors(kernelTraceCollector, customTraceCollector);
                        stopEvent.Set();
                    });

                    stopTraceCollectors = () => { StopCollectors(kernelTraceCollector, customTraceCollector); };

                    ThreadPool.QueueUserWorkItem((o) => { kernelTraceCollector.Start(); });
                    ThreadPool.QueueUserWorkItem((o) => { customTraceCollector.Start(); });

                    Thread.Sleep(1000);

                    // resume thread
                    process.Resume();

                    stopEvent.WaitOne();
                }
            }
        }

        public void TraceRunningProcess(int pid, TraceOptions traceOptions)
        {
            using (var hProcess = Kernel32.OpenProcess(Kernel32.ACCESS_MASK.StandardRight.SYNCHRONIZE, false, pid)) {
                if (hProcess.IsInvalid) {
                    Console.Error.WriteLine("ERROR: the process with a given PID was not found or you don't have access to it.");
                    return;
                }

                using (var kernelTraceCollector = new TraceCollector(KernelTraceEventParser.KernelSessionName))
                using (var userTraceCollector = new TraceCollector(WinTraceUserTraceSessionName)) {
                    InitializeProcessHandlers(kernelTraceCollector, userTraceCollector,
                        pid, traceOptions);

                    ThreadPool.QueueUserWorkItem((o) => {
                        Kernel32.WaitForSingleObject(hProcess, Constants.INFINITE);
                        StopCollectors(kernelTraceCollector, userTraceCollector);
                        stopEvent.Set();
                    });

                    stopTraceCollectors = () => { StopCollectors(kernelTraceCollector, userTraceCollector); };

                    ThreadPool.QueueUserWorkItem((o) => { kernelTraceCollector.Start(); });
                    ThreadPool.QueueUserWorkItem((o) => { userTraceCollector.Start(); });

                    stopEvent.WaitOne();
                }
            }
        }
        
        private void InitializeProcessHandlers(TraceCollector kernelTraceCollector, TraceCollector userTraceCollector,
            int pid, TraceOptions traceOptions)
        {
            //kernelCollector.AddHandler(new NetworkTraceEventHandler(pid, traceOutput));
            if (traceOptions.TraceChildProcesses) {
                kernelTraceCollector.AddHandler(new ProcessThreadsTraceEventHandler(pid, traceOutput,
                    processId => { InitializeProcessHandlers(kernelTraceCollector, userTraceCollector, processId, traceOptions); }));
            }

            userTraceCollector.AddHandler(new SystemNetSocketsEventHandler(pid, traceOptions.PrintPacketBytes, traceOutput));
        }

        private static void StopCollectors(TraceCollector collector1, TraceCollector collector2)
        {
            collector1.Stop();
            collector2.Stop();
        }

        public void Stop()
        {
            if (stopTraceCollectors != null) {
                stopTraceCollectors();
                stopTraceCollectors = null;
            }

            stopEvent.Set();
        }

        public void Stop(bool overridenPrintSummary)
        {
            if (stopTraceCollectors != null) {
                stopTraceCollectors();
                stopTraceCollectors = null;
            }

            stopEvent.Set();
        }
    }
}