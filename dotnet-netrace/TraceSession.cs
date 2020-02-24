using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LowLevelDesign.NTrace.EventHandlers;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

namespace LowLevelDesign.NTrace
{
    public sealed class TraceSession
    {
        private const string WinTraceUserTraceSessionName = "ntrace-events";

        public sealed class TraceOptions
        {
            public bool PrintPacketBytes { get; set; }
        }

        private readonly CancellationTokenSource cts;
        private readonly ITraceOutput traceOutput;

        public TraceSession(ITraceOutput traceOutput)
        {
            this.traceOutput = traceOutput;
            cts = new CancellationTokenSource();
        }

        public void TraceNewProcess(string[] procargs, TraceOptions traceOptions)
        {
            Debug.Assert(procargs.Length >= 1);
            var startInfo = new ProcessStartInfo(procargs[0],
                string.Join(" ", procargs, 1, procargs.Length - 1)) {
                UseShellExecute = false,
            };
            var process = Process.Start(startInfo);
            if (process == null) {
                throw new ArgumentException("Can't start the process.");
            }
            var processId = process.Id;
            process.Close();

            TraceRunningProcess(processId, traceOptions);
        }


        public void TraceRunningProcess(int processId, TraceOptions traceOptions)
        {
            const int MaxTrialsCount = 5;

            var currentTrial = 0;
            while (currentTrial < MaxTrialsCount) {
                using var process = Process.GetProcessById(processId);
                foreach (var module in process.Modules.Cast<ProcessModule>()) {
                    if (ClrInfoProvider.IsSupportedRuntime(module, out var clrFlavor, out var platform)) {
                        Console.WriteLine($"[{processId}] Detected CLR '{clrFlavor}' on platform '{platform}'");

                        Task.Run(() => {
                            // ReSharper disable once AccessToDisposedClosure
                            process.WaitForExit();
                            Console.WriteLine($"[{processId}] Process exited. Closing trace.");
                            cts.Cancel();
                        });

                        // blocking wait to collect traces
                        switch (clrFlavor) {
                            case ClrFlavor.Core:
                                TraceDotnetCoreProcess(processId, traceOptions);
                                break;
                            default:
                                Debug.Assert(platform == Platform.Windows);
                                Debug.Assert(clrFlavor == ClrFlavor.Desktop);
                                TraceDotnetFullProcess(processId);
                                break;
                        }

                        return;
                    }
                }
                Thread.Sleep(TimeSpan.FromMilliseconds(200));
            }

            Console.WriteLine($"[{processId}] Cannot attach to the process - it is native or running an unknown CLR version.");
        }

        private void TraceDotnetCoreProcess(int processId, TraceOptions traceOptions)
        {
            var client = new DiagnosticsClient(processId);

            var providers = new[] {
                new EventPipeProvider("Microsoft-System-Net-Sockets", EventLevel.Verbose, keywords: 0xFFFFFFFF)
            };

            // we are not interested in the rundown events as we only trace network data in real-mode
            using var eventPipeSession = client.StartEventPipeSession(providers, false, 1024);
            // ReSharper disable once AccessToDisposedClosure
            using var reg = cts.Token.Register(() => eventPipeSession.Stop());
            var eventSource = new EventPipeEventSource(eventPipeSession.EventStream);

            new SystemNetTraceEventHandler(processId, traceOutput,
                traceOptions.PrintPacketBytes).Subscribe(eventSource);
            eventSource.Process();
        }

        private void TraceDotnetFullProcess(int processId)
        {
            using var etwSession = new TraceEventSession(WinTraceUserTraceSessionName);
            // ReSharper disable once AccessToDisposedClosure
            using var reg = cts.Token.Register(() => etwSession.Stop());

            new SystemDiagnosticsTraceEventHandler(processId, traceOutput).Subscribe(etwSession);
            etwSession.Source.Process();
        }

        public void Stop()
        {
            cts.Cancel();
        }
    }
}