using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using LowLevelDesign.NTrace.EventHandlers;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;

namespace LowLevelDesign.NTrace
{
    public sealed class TraceCollector : IDisposable
    {
        private bool disposed;

        private readonly TraceEventSession traceSession;
        private readonly List<ITraceEventHandler> eventHandlers;
        private KernelTraceEventParser.Keywords kernelFlags = KernelTraceEventParser.Keywords.None;

        public TraceCollector(string sessionName)
        {
            traceSession = new TraceEventSession(sessionName) { StopOnDispose = true };
            eventHandlers = new List<ITraceEventHandler>();
        }

        public void AddHandler(ITraceEventHandler handler)
        {
            if (traceSession.IsActive) {
                // live handler adding
                handler.SubscribeToSession(traceSession);
            } else {
                kernelFlags |= handler.RequiredKernelFlags;
            }
            eventHandlers.Add(handler);
        }

        public void Start()
        {
            if (kernelFlags != KernelTraceEventParser.Keywords.None) {
                traceSession.EnableKernelProvider(kernelFlags);
            }

            if (eventHandlers.Count > 0) {
                foreach (var handler in eventHandlers) {
                    handler.SubscribeToSession(traceSession);
                }

                traceSession.Source.Process();
            }
        }

        public void Stop()
        {
            if (traceSession.IsActive) {
                var eventsLost = traceSession.EventsLost;

                Trace.WriteLine($"### Stopping {traceSession.SessionName} session...");
                traceSession.Stop();

                // This timeout is needed to handle all the DCStop events 
                // (in case we ever are going to do anything about them)
                Thread.Sleep(1500);

                Trace.WriteLine($"### {traceSession.SessionName} session stopped. Number of lost events: {eventsLost:#,0}");
            }
        }

        public void Dispose()
        {
            if (disposed) {
                return;
            }
            traceSession.Dispose();
            disposed = true;
        }
    }
}
