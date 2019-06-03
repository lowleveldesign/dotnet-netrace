using LowLevelDesign.NTrace.EventHandlers.System.Diagnostics;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;

namespace LowLevelDesign.NTrace.EventHandlers
{
    internal sealed class SystemDiagnosticsTraceEventHandler : ITraceEventHandler
    {
        private readonly ITraceOutput traceOutput;
        private readonly int pid;

        public KernelTraceEventParser.Keywords RequiredKernelFlags => KernelTraceEventParser.Keywords.None;

        public SystemDiagnosticsTraceEventHandler(int pid, ITraceOutput output)
        {
            traceOutput = output;
            this.pid = pid;
        }

        public void SubscribeToSession(TraceEventSession session)
        {
            var parser = new SystemDiagnosticsTraceEventParser(session.Source);
            parser.ReceiveLog += OnReceiveLog;
            session.EnableProvider(SystemDiagnosticsTraceEventParser.ProviderGuid, TraceEventLevel.Always, 0xFFFFFFFF);
        }

        private void OnReceiveLog(StringTraceData data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID,
                    data.EventName, data.Value);
            }
        }
    }
}