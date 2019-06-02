using LowLevelDesign.Hexify;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftSystemNetSockets;
using Microsoft.Diagnostics.Tracing.Session;

namespace LowLevelDesign.NTrace.EventHandlers
{
    internal class SystemNetSecurityEventHandler : ITraceEventHandler
    {
        private readonly ITraceOutput traceOutput;
        private readonly int pid;
        private readonly bool printPacketBytes;

        public KernelTraceEventParser.Keywords RequiredKernelFlags => KernelTraceEventParser.Keywords.None;

        public SystemNetSecurityEventHandler(int pid, bool printPacketBytes, ITraceOutput output)
        {
            traceOutput = output;
            this.printPacketBytes = printPacketBytes;
            this.pid = pid;
        }

        public void SubscribeToSession(TraceEventSession session)
        {
            var systemNetSocketsParser = new MicrosoftSystemNetSecurityTraceEventParser(session.Source);

            if (printPacketBytes) {
                //systemNetSocketsParser.DumpBuffer += SystemNetSocketsParserOnDumpBuffer;
            }

            session.EnableProvider(MicrosoftSystemNetSocketsTraceEventParser.ProviderGuid);
        }

    }
}