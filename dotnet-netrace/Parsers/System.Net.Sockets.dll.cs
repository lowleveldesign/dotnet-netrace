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
    using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftSystemNetSockets;

    [System.CodeDom.Compiler.GeneratedCode("traceparsergen", "2.0")]
    public sealed class MicrosoftSystemNetSocketsTraceEventParser : TraceEventParser 
    {
        public static string ProviderName = "Microsoft-System-Net-Sockets";
        public static Guid ProviderGuid = new Guid(unchecked((int) 0xe03c0352), unchecked((short) 0xf9c9), unchecked((short) 0x56ff), 0x0e, 0xa7, 0xb9, 0x4b, 0xa8, 0xca, 0xbc, 0x6b);
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

        public MicrosoftSystemNetSocketsTraceEventParser(TraceEventSource source) : base(source) {}

        public event Action<AcceptedArgs> Accepted
        {
            add
            {
                source.RegisterEventTemplate(AcceptedTemplate(value));
            }
            remove
            {
                source.UnregisterEventTemplate(value, 17, ProviderGuid);
            }
        }
        public event Action<ConnectedArgs> Connected
        {
            add
            {
                source.RegisterEventTemplate(ConnectedTemplate(value));
            }
            remove
            {
                source.UnregisterEventTemplate(value, 18, ProviderGuid);
            }
        }
        public event Action<ConnectedAsyncDnsArgs> ConnectedAsyncDns
        {
            add
            {
                source.RegisterEventTemplate(ConnectedAsyncDnsTemplate(value));
            }
            remove
            {
                source.UnregisterEventTemplate(value, 19, ProviderGuid);
            }
        }
        public event Action<CriticalFailureArgs> CriticalFailure
        {
            add
            {
                source.RegisterEventTemplate(CriticalFailureTemplate(value));
            }
            remove
            {
                source.UnregisterEventTemplate(value, 6, ProviderGuid);
            }
        }
        public event Action<DumpBufferArgs> DumpBuffer
        {
            add
            {
                source.RegisterEventTemplate(DumpBufferTemplate(value));
            }
            remove
            {
                source.UnregisterEventTemplate(value, 7, ProviderGuid);
            }
        }
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
        public event Action<InfoArgs> Info
        {
            add
            {
                source.RegisterEventTemplate(InfoTemplate(value));
            }
            remove
            {
                source.UnregisterEventTemplate(value, 4, ProviderGuid);
            }
        }

        #region private
        protected override string GetProviderName() { return ProviderName; }

        static private AcceptedArgs AcceptedTemplate(Action<AcceptedArgs> action)
        {                  // action, eventid, taskid, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName
            return new AcceptedArgs(action, 17, 65517, "Accepted", Guid.Empty, 0, "", ProviderGuid, ProviderName );
        }
        static private ConnectedArgs ConnectedTemplate(Action<ConnectedArgs> action)
        {                  // action, eventid, taskid, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName
            return new ConnectedArgs(action, 18, 65516, "Connected", Guid.Empty, 0, "", ProviderGuid, ProviderName );
        }
        static private ConnectedAsyncDnsArgs ConnectedAsyncDnsTemplate(Action<ConnectedAsyncDnsArgs> action)
        {                  // action, eventid, taskid, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName
            return new ConnectedAsyncDnsArgs(action, 19, 65515, "ConnectedAsyncDns", Guid.Empty, 0, "", ProviderGuid, ProviderName );
        }
        static private CriticalFailureArgs CriticalFailureTemplate(Action<CriticalFailureArgs> action)
        {                  // action, eventid, taskid, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName
            return new CriticalFailureArgs(action, 6, 65528, "CriticalFailure", Guid.Empty, 0, "", ProviderGuid, ProviderName );
        }
        static private DumpBufferArgs DumpBufferTemplate(Action<DumpBufferArgs> action)
        {                  // action, eventid, taskid, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName
            return new DumpBufferArgs(action, 7, 65527, "DumpBuffer", Guid.Empty, 0, "", ProviderGuid, ProviderName );
        }
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
        static private InfoArgs InfoTemplate(Action<InfoArgs> action)
        {                  // action, eventid, taskid, taskName, taskGuid, opcode, opcodeName, providerGuid, providerName
            return new InfoArgs(action, 4, 65530, "Info", Guid.Empty, 0, "", ProviderGuid, ProviderName );
        }

        static private volatile TraceEvent[] s_templates;
        protected override void EnumerateTemplates(Func<string, string, EventFilterResponse> eventsToObserve, Action<TraceEvent> callback)
        {
            if (s_templates == null)
            {
                var templates = new TraceEvent[10];
                templates[0] = EventSourceMessageTemplate(null);
                templates[1] = AcceptedTemplate(null);
                templates[2] = ConnectedTemplate(null);
                templates[3] = ConnectedAsyncDnsTemplate(null);
                templates[4] = EnterTemplate(null);
                templates[5] = ExitTemplate(null);
                templates[6] = InfoTemplate(null);
                templates[7] = ErrorMessageTemplate(null);
                templates[8] = CriticalFailureTemplate(null);
                templates[9] = DumpBufferTemplate(null);
                s_templates = templates;
            }
            foreach (var template in s_templates)
                if (eventsToObserve == null || eventsToObserve(template.ProviderName, template.EventName) == EventFilterResponse.AcceptEvent)
                    callback(template);
        }

        #endregion
    }
}

namespace Microsoft.Diagnostics.Tracing.Parsers.MicrosoftSystemNetSockets
{
    public sealed class AcceptedArgs : TraceEvent
    {
        public string remoteEp { get { return GetUnicodeStringAt(0); } }
        public string localEp { get { return GetUnicodeStringAt(SkipUnicodeString(0)); } }
        public int socketHash { get { return GetInt32At(SkipUnicodeString(SkipUnicodeString(0))); } }

        #region Private
        internal AcceptedArgs(Action<AcceptedArgs> target, int eventID, int task, string taskName, Guid taskGuid, int opcode, string opcodeName, Guid providerGuid, string providerName)
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
            Debug.Assert(!(Version == 0 && EventDataLength != SkipUnicodeString(SkipUnicodeString(0))+4));
            Debug.Assert(!(Version > 0 && EventDataLength < SkipUnicodeString(SkipUnicodeString(0))+4));
        }
        protected override Delegate Target
        {
            get { return m_target; }
            set { m_target = (Action<AcceptedArgs>) value; }
        }
        public override StringBuilder ToXml(StringBuilder sb)
        {
             Prefix(sb);
             XmlAttrib(sb, "remoteEp", remoteEp);
             XmlAttrib(sb, "localEp", localEp);
             XmlAttrib(sb, "socketHash", socketHash);
             sb.Append("/>");
             return sb;
        }

        public override string[] PayloadNames
        {
            get
            {
                if (payloadNames == null)
                    payloadNames = new string[] { "remoteEp", "localEp", "socketHash"};
                return payloadNames;
            }
        }

        public override object PayloadValue(int index)
        {
            switch (index)
            {
                case 0:
                    return remoteEp;
                case 1:
                    return localEp;
                case 2:
                    return socketHash;
                default:
                    Debug.Assert(false, "Bad field index");
                    return null;
            }
        }

        public static ulong GetKeywords() { return 1; }
        public static string GetProviderName() { return "Microsoft-System-Net-Sockets"; }
        public static Guid GetProviderGuid() { return new Guid("e03c0352-f9c9-56ff-0ea7-b94ba8cabc6b"); }
        private event Action<AcceptedArgs> m_target;
        #endregion
    }
    public sealed class ConnectedArgs : TraceEvent
    {
        public string localEp { get { return GetUnicodeStringAt(0); } }
        public string remoteEp { get { return GetUnicodeStringAt(SkipUnicodeString(0)); } }
        public int socketHash { get { return GetInt32At(SkipUnicodeString(SkipUnicodeString(0))); } }

        #region Private
        internal ConnectedArgs(Action<ConnectedArgs> target, int eventID, int task, string taskName, Guid taskGuid, int opcode, string opcodeName, Guid providerGuid, string providerName)
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
            Debug.Assert(!(Version == 0 && EventDataLength != SkipUnicodeString(SkipUnicodeString(0))+4));
            Debug.Assert(!(Version > 0 && EventDataLength < SkipUnicodeString(SkipUnicodeString(0))+4));
        }
        protected override Delegate Target
        {
            get { return m_target; }
            set { m_target = (Action<ConnectedArgs>) value; }
        }
        public override StringBuilder ToXml(StringBuilder sb)
        {
             Prefix(sb);
             XmlAttrib(sb, "localEp", localEp);
             XmlAttrib(sb, "remoteEp", remoteEp);
             XmlAttrib(sb, "socketHash", socketHash);
             sb.Append("/>");
             return sb;
        }

        public override string[] PayloadNames
        {
            get
            {
                if (payloadNames == null)
                    payloadNames = new string[] { "localEp", "remoteEp", "socketHash"};
                return payloadNames;
            }
        }

        public override object PayloadValue(int index)
        {
            switch (index)
            {
                case 0:
                    return localEp;
                case 1:
                    return remoteEp;
                case 2:
                    return socketHash;
                default:
                    Debug.Assert(false, "Bad field index");
                    return null;
            }
        }

        public static ulong GetKeywords() { return 1; }
        public static string GetProviderName() { return "Microsoft-System-Net-Sockets"; }
        public static Guid GetProviderGuid() { return new Guid("e03c0352-f9c9-56ff-0ea7-b94ba8cabc6b"); }
        private event Action<ConnectedArgs> m_target;
        #endregion
    }
    public sealed class ConnectedAsyncDnsArgs : TraceEvent
    {
        public int socketHash { get { return GetInt32At(0); } }

        #region Private
        internal ConnectedAsyncDnsArgs(Action<ConnectedAsyncDnsArgs> target, int eventID, int task, string taskName, Guid taskGuid, int opcode, string opcodeName, Guid providerGuid, string providerName)
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
            Debug.Assert(!(Version == 0 && EventDataLength != 4));
            Debug.Assert(!(Version > 0 && EventDataLength < 4));
        }
        protected override Delegate Target
        {
            get { return m_target; }
            set { m_target = (Action<ConnectedAsyncDnsArgs>) value; }
        }
        public override StringBuilder ToXml(StringBuilder sb)
        {
             Prefix(sb);
             XmlAttrib(sb, "socketHash", socketHash);
             sb.Append("/>");
             return sb;
        }

        public override string[] PayloadNames
        {
            get
            {
                if (payloadNames == null)
                    payloadNames = new string[] { "socketHash"};
                return payloadNames;
            }
        }

        public override object PayloadValue(int index)
        {
            switch (index)
            {
                case 0:
                    return socketHash;
                default:
                    Debug.Assert(false, "Bad field index");
                    return null;
            }
        }

        public static ulong GetKeywords() { return 1; }
        public static string GetProviderName() { return "Microsoft-System-Net-Sockets"; }
        public static Guid GetProviderGuid() { return new Guid("e03c0352-f9c9-56ff-0ea7-b94ba8cabc6b"); }
        private event Action<ConnectedAsyncDnsArgs> m_target;
        #endregion
    }
    public sealed class CriticalFailureArgs : TraceEvent
    {
        public string thisOrContextObject { get { return GetUnicodeStringAt(0); } }
        public string memberName { get { return GetUnicodeStringAt(SkipUnicodeString(0)); } }
        public string message { get { return GetUnicodeStringAt(SkipUnicodeString(SkipUnicodeString(0))); } }

        #region Private
        internal CriticalFailureArgs(Action<CriticalFailureArgs> target, int eventID, int task, string taskName, Guid taskGuid, int opcode, string opcodeName, Guid providerGuid, string providerName)
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
            set { m_target = (Action<CriticalFailureArgs>) value; }
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

        public static ulong GetKeywords() { return 2; }
        public static string GetProviderName() { return "Microsoft-System-Net-Sockets"; }
        public static Guid GetProviderGuid() { return new Guid("e03c0352-f9c9-56ff-0ea7-b94ba8cabc6b"); }
        private event Action<CriticalFailureArgs> m_target;
        #endregion
    }
    public sealed class DumpBufferArgs : TraceEvent
    {
        public string thisOrContextObject { get { return GetUnicodeStringAt(0); } }
        public string memberName { get { return GetUnicodeStringAt(SkipUnicodeString(0)); } }
        public int bufferSize { get { return GetInt32At(SkipUnicodeString(SkipUnicodeString(0))); } }
        public byte[] buffer { get { return GetByteArrayAt(SkipUnicodeString(SkipUnicodeString(0))+4, bufferSize); } }

        #region Private
        internal DumpBufferArgs(Action<DumpBufferArgs> target, int eventID, int task, string taskName, Guid taskGuid, int opcode, string opcodeName, Guid providerGuid, string providerName)
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
            Debug.Assert(!(Version == 0 && EventDataLength != SkipUnicodeString(SkipUnicodeString(0))+4));
            Debug.Assert(!(Version > 0 && EventDataLength < SkipUnicodeString(SkipUnicodeString(0))+4));
        }
        protected override Delegate Target
        {
            get { return m_target; }
            set { m_target = (Action<DumpBufferArgs>) value; }
        }
        public override StringBuilder ToXml(StringBuilder sb)
        {
             Prefix(sb);
             XmlAttrib(sb, "thisOrContextObject", thisOrContextObject);
             XmlAttrib(sb, "memberName", memberName);
             XmlAttrib(sb, "bufferSize", bufferSize);
             sb.Append("/>");
             return sb;
        }

        public override string[] PayloadNames
        {
            get
            {
                if (payloadNames == null)
                    payloadNames = new string[] { "thisOrContextObject", "memberName", "bufferSize", "buffer"};
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
                    return bufferSize;
                default:
                    Debug.Assert(false, "Bad field index");
                    return null;
            }
        }

        public static ulong GetKeywords() { return 2; }
        public static string GetProviderName() { return "Microsoft-System-Net-Sockets"; }
        public static Guid GetProviderGuid() { return new Guid("e03c0352-f9c9-56ff-0ea7-b94ba8cabc6b"); }
        private event Action<DumpBufferArgs> m_target;
        #endregion
    }
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
        public static string GetProviderName() { return "Microsoft-System-Net-Sockets"; }
        public static Guid GetProviderGuid() { return new Guid("e03c0352-f9c9-56ff-0ea7-b94ba8cabc6b"); }
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
        public static string GetProviderName() { return "Microsoft-System-Net-Sockets"; }
        public static Guid GetProviderGuid() { return new Guid("e03c0352-f9c9-56ff-0ea7-b94ba8cabc6b"); }
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
        public static string GetProviderName() { return "Microsoft-System-Net-Sockets"; }
        public static Guid GetProviderGuid() { return new Guid("e03c0352-f9c9-56ff-0ea7-b94ba8cabc6b"); }
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
        public static string GetProviderName() { return "Microsoft-System-Net-Sockets"; }
        public static Guid GetProviderGuid() { return new Guid("e03c0352-f9c9-56ff-0ea7-b94ba8cabc6b"); }
        private event Action<ExitArgs> m_target;
        #endregion
    }
    public sealed class InfoArgs : TraceEvent
    {
        public string thisOrContextObject { get { return GetUnicodeStringAt(0); } }
        public string memberName { get { return GetUnicodeStringAt(SkipUnicodeString(0)); } }
        public string message { get { return GetUnicodeStringAt(SkipUnicodeString(SkipUnicodeString(0))); } }

        #region Private
        internal InfoArgs(Action<InfoArgs> target, int eventID, int task, string taskName, Guid taskGuid, int opcode, string opcodeName, Guid providerGuid, string providerName)
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
            set { m_target = (Action<InfoArgs>) value; }
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
        public static string GetProviderName() { return "Microsoft-System-Net-Sockets"; }
        public static Guid GetProviderGuid() { return new Guid("e03c0352-f9c9-56ff-0ea7-b94ba8cabc6b"); }
        private event Action<InfoArgs> m_target;
        #endregion
    }
}
