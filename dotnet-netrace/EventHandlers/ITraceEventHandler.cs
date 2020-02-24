using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;

namespace LowLevelDesign.NTrace.EventHandlers
{
    public interface ITraceEventHandler
    {
        EventPipeProvider[] Providers { get; }
        
        void Subscribe(TraceEventSource source); 
        
        void Subscribe(TraceEventSession session); 
    }
}
