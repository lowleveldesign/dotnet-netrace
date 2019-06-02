using System;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;

namespace LowLevelDesign.NTrace.EventHandlers
{
    internal sealed class ProcessThreadsTraceEventHandler : ITraceEventHandler
    {
        private readonly ITraceOutput traceOutput;
        private readonly int pid;
        private readonly Action<int> actionToPerformWhenNewProcessIsCreated;

        public KernelTraceEventParser.Keywords RequiredKernelFlags => KernelTraceEventParser.Keywords.Process
            | KernelTraceEventParser.Keywords.Thread;

        public ProcessThreadsTraceEventHandler(int pid, ITraceOutput output, Action<int> actionToPerformWhenNewProcessIsCreated)
        {
            traceOutput = output;
            this.pid = pid;
            this.actionToPerformWhenNewProcessIsCreated = actionToPerformWhenNewProcessIsCreated;
        }

        public void SubscribeToSession(TraceEventSession session)
        {
            var kernel = session.Source.Kernel;
            kernel.ProcessStart += HandleProcessStart;
            kernel.ThreadStart += HandleThreadStart;
        }

        private void HandleThreadStart(ThreadTraceData data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"{data.ParentProcessID} ({data.ParentThreadID})");
            }
        }

        private void HandleProcessStart(ProcessTraceData data)
        {
            if (data.ParentID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"{data.ProcessID} '{data.CommandLine}'");

                actionToPerformWhenNewProcessIsCreated(data.ProcessID);
            }
        }
    }
}
