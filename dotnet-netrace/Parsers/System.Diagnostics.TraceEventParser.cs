#nullable disable

using System;
using Microsoft.Diagnostics.Tracing;

namespace LowLevelDesign.NTrace.Parsers
{
    public sealed class SystemDiagnosticsTraceEventParser : TraceEventParser
    {
        public static readonly Guid ProviderGuid = Guid.Parse("e4144c8f-cc80-4797-a7cc-cfe14de522ea");
        public const string ProviderName = "System.Diagnostics";

        public SystemDiagnosticsTraceEventParser(TraceEventSource source) : base(source) { }

        private static volatile TraceEvent[] templates;

        protected override void EnumerateTemplates(Func<string, string, EventFilterResponse> eventsToObserve, Action<TraceEvent> callback)
        {
            if (templates == null) {
                if (templates == null) {
                    var t = new TraceEvent[1];
                    t[0] = LogTemplate(null);
                    templates = t;
                }
                foreach (var template in templates)
                    if (eventsToObserve == null || eventsToObserve(template.ProviderName, template.EventName) == EventFilterResponse.AcceptEvent)
                        callback(template);
            }
        }

        public event Action<StringTraceData> ReceiveLog {
            add => source.RegisterEventTemplate(LogTemplate(value));
            remove => source.UnregisterEventTemplate(value, 0, ProviderGuid);
        }

        protected override string GetProviderName()
        {
            return ProviderName;
        }

        private static StringTraceData LogTemplate(Action<StringTraceData> action)
        {
            return new StringTraceData(action, 0, 0, "Log", default(Guid), 0, string.Empty,
                SystemDiagnosticsTraceEventParser.ProviderGuid,
                SystemDiagnosticsTraceEventParser.ProviderName, true);
        }
    }
}