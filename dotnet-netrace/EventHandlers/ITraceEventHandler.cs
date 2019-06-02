using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;

namespace LowLevelDesign.NTrace.EventHandlers
{
    public interface ITraceEventHandler
    {
        KernelTraceEventParser.Keywords RequiredKernelFlags { get; }

        void SubscribeToSession(TraceEventSession session); 
    }
}
