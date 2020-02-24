using System.Diagnostics.Tracing;
using LowLevelDesign.NTrace.Parsers;
using Microsoft.Diagnostics.NETCore.Client;
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
            
            Providers = new [] {
                new EventPipeProvider(SystemDiagnosticsTraceEventParser.ProviderName, EventLevel.LogAlways),
            };
        }

        public void Subscribe(TraceEventSource source)
        {
            var parser = new SystemDiagnosticsTraceEventParser(source);
            parser.ReceiveLog += OnReceiveLog;
        }

        public EventPipeProvider[] Providers { get; }

        public void Subscribe(TraceEventSession session)
        {
            Subscribe(session.Source);
            
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