//<autogenerated/>
using System;
using System.Diagnostics;
using System.Text;
using Address = System.UInt64;
// ReSharper disable All

#pragma warning disable 1591        // disable warnings on XML comments not being present

// This code was automatically generated by the TraceParserGen tool, which converts
// an ETW event manifest into strongly typed C# classes.
namespace Microsoft.Diagnostics.Tracing.Parsers
{
    using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftSystemNetWebSocketsClient;

    [System.CodeDom.Compiler.GeneratedCode("traceparsergen", "2.0")]
    public sealed class MicrosoftSystemNetWebSocketsClientTraceEventParser : TraceEventParser 
    {
        public static string ProviderName = "Microsoft-System-Net-WebSockets-Client";
        public static Guid ProviderGuid = new Guid(unchecked((int) 0x71cddde3), unchecked((short) 0xcf58), unchecked((short) 0x52d5), 0x09, 0x4f, 0x92, 0x78, 0x28, 0xa0, 0x93, 0x37);
        public enum Keywords : long
        {
            Default = 0x1,
            Debug = 0x2,
            Enterexit = 0x4,
            Session3 = 0x100000000000,
            Session2 = 0x200000000000,
            Session1 = 0x400000000000,
            Session0 = 0x800000000000,
        };

        public MicrosoftSystemNetWebSocketsClientTraceEventParser(TraceEventSource source) : base(source) {}

        public event Action<EnterArgs> Enter
        {
            add
            {
                source.RegisterEventTemplate(EnterTemplate(value));
            }
            remove
            {
                source.UnregisterEventTemplate(value, 1, ProviderGuid);
            }
        }
        public event Action<ErrorMessageArgs> ErrorMessage
        {
            add
            {
                source.RegisterEventTemplate(ErrorMessageTemplate(value));
            }
            remove
            {
                source.UnregisterEventTemplate(value, 5, ProviderGuid);
            }
        }
        public event Action<EventSourceMessageArgs> EventSourceMessage
        {
            add
            {
                source.RegisterEventTemplate(EventSourceMessageTemplate(value));
            }
            remove
            {
                source.UnregisterEventTemplate(value, 0, ProviderGuid);
            }
        }
        public event Action<ExitArgs> Exit
        {
            add
            {
                source.RegisterEventTemplate(ExitTemplate(value));
            }
            remove
            {
                source.UnregisterEventTemplate(value, 2, ProviderGuid);
            }
        }

        #region private
        protected override string GetProviderName() { return ProviderName; }

        static private EnterArgs EnterTemplate(Action<EnterArgs> action)
        {                  // action, eventid, taskid, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName
            return new EnterArgs(action, 1, 65533, "Enter", Guid.Empty, 0, "", ProviderGuid, ProviderName );
        }
        static private ErrorMessageArgs ErrorMessageTemplate(Action<ErrorMessageArgs> action)
        {                  // action, eventid, taskid, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName
            return new ErrorMessageArgs(action, 5, 65529, "ErrorMessage", Guid.Empty, 0, "", ProviderGuid, ProviderName );
        }
        static private EventSourceMessageArgs EventSourceMessageTemplate(Action<EventSourceMessageArgs> action)
        {                  // action, eventid, taskid, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName
            return new EventSourceMessageArgs(action, 0, 65534, "EventSourceMessage", Guid.Empty, 0, "", ProviderGuid, ProviderName );
        }
        static private ExitArgs ExitTemplate(Action<ExitArgs> action)
        {                  // action, eventid, taskid, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName
            return new ExitArgs(action, 2, 65532, "Exit", Guid.Empty, 0, "", ProviderGuid, ProviderName );
        }

        static private volatile TraceEvent[] s_templates;
        protected override void EnumerateTemplates(Func<string, string, EventFilterResponse> eventsToObserve, Action<TraceEvent> callback)
        {
            if (s_templates == null)
            {
                var templates = new TraceEvent[4];
                templates[0] = EventSourceMessageTemplate(null);
                templates[1] = EnterTemplate(null);
                templates[2] = ExitTemplate(null);
                templates[3] = ErrorMessageTemplate(null);
                s_templates = templates;
            }
            foreach (var template in s_templates)
                if (eventsToObserve == null || eventsToObserve(template.ProviderName, template.EventName) == EventFilterResponse.AcceptEvent)
                    callback(template);
        }

        #endregion
    }
}

namespace Microsoft.Diagnostics.Tracing.Parsers.MicrosoftSystemNetWebSocketsClient
{
    public sealed class EnterArgs : TraceEvent
    {
        public string thisOrContextObject { get { return GetUnicodeStringAt(0); } }
        public string memberName { get { return GetUnicodeStringAt(SkipUnicodeString(0)); } }
        public string parameters { get { return GetUnicodeStringAt(SkipUnicodeString(SkipUnicodeString(0))); } }

        #region Private
        internal EnterArgs(Action<EnterArgs> target, int eventID, int task, string taskName, Guid taskGuid, int opcode, string opcodeName, Guid providerGuid, string providerName)
            : base(eventID, task, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName)
        {
            this.m_target = target;
        }
        protected override void Dispatch()
        {
            m_target(this);
        }
        protected override void Validate()
        {
            Debug.Assert(!(Version == 0 && EventDataLength != SkipUnicodeString(SkipUnicodeString(SkipUnicodeString(0)))));
            Debug.Assert(!(Version > 0 && EventDataLength < SkipUnicodeString(SkipUnicodeString(SkipUnicodeString(0)))));
        }
        protected override Delegate Target
        {
            get { return m_target; }
            set { m_target = (Action<EnterArgs>) value; }
        }
        public override StringBuilder ToXml(StringBuilder sb)
        {
             Prefix(sb);
             XmlAttrib(sb, "thisOrContextObject", thisOrContextObject);
             XmlAttrib(sb, "memberName", memberName);
             XmlAttrib(sb, "parameters", parameters);
             sb.Append("/>");
             return sb;
        }

        public override string[] PayloadNames
        {
            get
            {
                if (payloadNames == null)
                    payloadNames = new string[] { "thisOrContextObject", "memberName", "parameters"};
                return payloadNames;
            }
        }

        public override object PayloadValue(int index)
        {
            switch (index)
            {
                case 0:
                    return thisOrContextObject;
                case 1:
                    return memberName;
                case 2:
                    return parameters;
                default:
                    Debug.Assert(false, "Bad field index");
                    return null;
            }
        }

        public static ulong GetKeywords() { return 4; }
        public static string GetProviderName() { return "Microsoft-System-Net-WebSockets-Client"; }
        public static Guid GetProviderGuid() { return new Guid("71cddde3-cf58-52d5-094f-927828a09337"); }
        private event Action<EnterArgs> m_target;
        #endregion
    }
    public sealed class ErrorMessageArgs : TraceEvent
    {
        public string thisOrContextObject { get { return GetUnicodeStringAt(0); } }
        public string memberName { get { return GetUnicodeStringAt(SkipUnicodeString(0)); } }
        public string message { get { return GetUnicodeStringAt(SkipUnicodeString(SkipUnicodeString(0))); } }

        #region Private
        internal ErrorMessageArgs(Action<ErrorMessageArgs> target, int eventID, int task, string taskName, Guid taskGuid, int opcode, string opcodeName, Guid providerGuid, string providerName)
            : base(eventID, task, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName)
        {
            this.m_target = target;
        }
        protected override void Dispatch()
        {
            m_target(this);
        }
        protected override void Validate()
        {
            Debug.Assert(!(Version == 0 && EventDataLength != SkipUnicodeString(SkipUnicodeString(SkipUnicodeString(0)))));
            Debug.Assert(!(Version > 0 && EventDataLength < SkipUnicodeString(SkipUnicodeString(SkipUnicodeString(0)))));
        }
        protected override Delegate Target
        {
            get { return m_target; }
            set { m_target = (Action<ErrorMessageArgs>) value; }
        }
        public override StringBuilder ToXml(StringBuilder sb)
        {
             Prefix(sb);
             XmlAttrib(sb, "thisOrContextObject", thisOrContextObject);
             XmlAttrib(sb, "memberName", memberName);
             XmlAttrib(sb, "message", message);
             sb.Append("/>");
             return sb;
        }

        public override string[] PayloadNames
        {
            get
            {
                if (payloadNames == null)
                    payloadNames = new string[] { "thisOrContextObject", "memberName", "message"};
                return payloadNames;
            }
        }

        public override object PayloadValue(int index)
        {
            switch (index)
            {
                case 0:
                    return thisOrContextObject;
                case 1:
                    return memberName;
                case 2:
                    return message;
                default:
                    Debug.Assert(false, "Bad field index");
                    return null;
            }
        }

        public static ulong GetKeywords() { return 1; }
        public static string GetProviderName() { return "Microsoft-System-Net-WebSockets-Client"; }
        public static Guid GetProviderGuid() { return new Guid("71cddde3-cf58-52d5-094f-927828a09337"); }
        private event Action<ErrorMessageArgs> m_target;
        #endregion
    }
    public sealed class EventSourceMessageArgs : TraceEvent
    {
        public string message { get { return GetUnicodeStringAt(0); } }

        #region Private
        internal EventSourceMessageArgs(Action<EventSourceMessageArgs> target, int eventID, int task, string taskName, Guid taskGuid, int opcode, string opcodeName, Guid providerGuid, string providerName)
            : base(eventID, task, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName)
        {
            this.m_target = target;
        }
        protected override void Dispatch()
        {
            m_target(this);
        }
        protected override void Validate()
        {
            Debug.Assert(!(Version == 0 && EventDataLength != SkipUnicodeString(0)));
            Debug.Assert(!(Version > 0 && EventDataLength < SkipUnicodeString(0)));
        }
        protected override Delegate Target
        {
            get { return m_target; }
            set { m_target = (Action<EventSourceMessageArgs>) value; }
        }
        public override StringBuilder ToXml(StringBuilder sb)
        {
             Prefix(sb);
             XmlAttrib(sb, "message", message);
             sb.Append("/>");
             return sb;
        }

        public override string[] PayloadNames
        {
            get
            {
                if (payloadNames == null)
                    payloadNames = new string[] { "message"};
                return payloadNames;
            }
        }

        public override object PayloadValue(int index)
        {
            switch (index)
            {
                case 0:
                    return message;
                default:
                    Debug.Assert(false, "Bad field index");
                    return null;
            }
        }

        public static ulong GetKeywords() { return 0; }
        public static string GetProviderName() { return "Microsoft-System-Net-WebSockets-Client"; }
        public static Guid GetProviderGuid() { return new Guid("71cddde3-cf58-52d5-094f-927828a09337"); }
        private event Action<EventSourceMessageArgs> m_target;
        #endregion
    }
    public sealed class ExitArgs : TraceEvent
    {
        public string thisOrContextObject { get { return GetUnicodeStringAt(0); } }
        public string memberName { get { return GetUnicodeStringAt(SkipUnicodeString(0)); } }
        public string result { get { return GetUnicodeStringAt(SkipUnicodeString(SkipUnicodeString(0))); } }

        #region Private
        internal ExitArgs(Action<ExitArgs> target, int eventID, int task, string taskName, Guid taskGuid, int opcode, string opcodeName, Guid providerGuid, string providerName)
            : base(eventID, task, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName)
        {
            this.m_target = target;
        }
        protected override void Dispatch()
        {
            m_target(this);
        }
        protected override void Validate()
        {
            Debug.Assert(!(Version == 0 && EventDataLength != SkipUnicodeString(SkipUnicodeString(SkipUnicodeString(0)))));
            Debug.Assert(!(Version > 0 && EventDataLength < SkipUnicodeString(SkipUnicodeString(SkipUnicodeString(0)))));
        }
        protected override Delegate Target
        {
            get { return m_target; }
            set { m_target = (Action<ExitArgs>) value; }
        }
        public override StringBuilder ToXml(StringBuilder sb)
        {
             Prefix(sb);
             XmlAttrib(sb, "thisOrContextObject", thisOrContextObject);
             XmlAttrib(sb, "memberName", memberName);
             XmlAttrib(sb, "result", result);
             sb.Append("/>");
             return sb;
        }

        public override string[] PayloadNames
        {
            get
            {
                if (payloadNames == null)
                    payloadNames = new string[] { "thisOrContextObject", "memberName", "result"};
                return payloadNames;
            }
        }

        public override object PayloadValue(int index)
        {
            switch (index)
            {
                case 0:
                    return thisOrContextObject;
                case 1:
                    return memberName;
                case 2:
                    return result;
                default:
                    Debug.Assert(false, "Bad field index");
                    return null;
            }
        }

        public static ulong GetKeywords() { return 4; }
        public static string GetProviderName() { return "Microsoft-System-Net-WebSockets-Client"; }
        public static Guid GetProviderGuid() { return new Guid("71cddde3-cf58-52d5-094f-927828a09337"); }
        private event Action<ExitArgs> m_target;
        #endregion
    }
}
