using System;
using System.Diagnostics.Tracing;
using System.Text;
using LowLevelDesign.Hexify;
using Microsoft.Diagnostics.NETCore.Client;
using Microsoft.Diagnostics.Tracing;
using Microsoft.Diagnostics.Tracing.Session;
using EtwParsers = Microsoft.Diagnostics.Tracing.Parsers;

namespace LowLevelDesign.NTrace.EventHandlers
{
    internal sealed class SystemNetTraceEventHandler : ITraceEventHandler
    {
        private readonly ITraceOutput traceOutput;
        private readonly int pid;
        private readonly bool printPacketBytes;
        private readonly int outputBytesLimit;

        public SystemNetTraceEventHandler(int pid, ITraceOutput output, bool printPacketBytes, int outputBytesLimit = 0)
        {
            traceOutput = output;
            this.pid = pid;
            this.printPacketBytes = printPacketBytes;
            this.outputBytesLimit = outputBytesLimit;

            Providers = new[] {
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetHttpTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF),
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetHttpListenerTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF),
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetMailTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF),
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetNameResolutionTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF),
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetNetworkInformationTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF),
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetPingTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF),
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetPrimitivesTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF),
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetRequestsTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF),
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetSecurityTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF),
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetSocketsTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF),
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetWebHeaderCollectionTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF),
                new EventPipeProvider(EtwParsers.MicrosoftSystemNetWebSocketsClientTraceEventParser.ProviderName, EventLevel.Verbose, 0xFFFFFFFF)
            };
        }

        public void Subscribe(TraceEventSource source)
        {
            TraceEventParser parser = new EtwParsers.MicrosoftSystemNetHttpTraceEventParser(source);
            parser.All += OnAll;

            parser = new EtwParsers.MicrosoftSystemNetHttpListenerTraceEventParser(source);
            parser.All += OnAll;

            parser = new EtwParsers.MicrosoftSystemNetMailTraceEventParser(source);
            parser.All += OnAll;

            parser = new EtwParsers.MicrosoftSystemNetNameResolutionTraceEventParser(source);
            parser.All += OnAll;

            parser = new EtwParsers.MicrosoftSystemNetNetworkInformationTraceEventParser(source);
            parser.All += OnAll;

            parser = new EtwParsers.MicrosoftSystemNetPingTraceEventParser(source);
            parser.All += OnAll;

            parser = new EtwParsers.MicrosoftSystemNetPrimitivesTraceEventParser(source);
            parser.All += OnAll;

            parser = new EtwParsers.MicrosoftSystemNetRequestsTraceEventParser(source);
            parser.All += OnAll;

            parser = new EtwParsers.MicrosoftSystemNetSecurityTraceEventParser(source);
            parser.All += OnAll;

            parser = new EtwParsers.MicrosoftSystemNetSocketsTraceEventParser(source);
            parser.All += OnAll;

            parser = new EtwParsers.MicrosoftSystemNetWebHeaderCollectionTraceEventParser(source);
            parser.All += OnAll;

            parser = new EtwParsers.MicrosoftSystemNetWebSocketsClientTraceEventParser(source);
            parser.All += OnAll;
        }

        public EventPipeProvider[] Providers { get; }

        public void Subscribe(TraceEventSession session)
        {
            Subscribe(session.Source);

            session.EnableProvider(EtwParsers.MicrosoftSystemNetHttpTraceEventParser.ProviderGuid);
            session.EnableProvider(EtwParsers.MicrosoftSystemNetHttpListenerTraceEventParser.ProviderGuid);
            session.EnableProvider(EtwParsers.MicrosoftSystemNetMailTraceEventParser.ProviderGuid);
            session.EnableProvider(EtwParsers.MicrosoftSystemNetNameResolutionTraceEventParser.ProviderGuid);
            session.EnableProvider(EtwParsers.MicrosoftSystemNetNetworkInformationTraceEventParser.ProviderGuid);
            session.EnableProvider(EtwParsers.MicrosoftSystemNetPingTraceEventParser.ProviderGuid);
            session.EnableProvider(EtwParsers.MicrosoftSystemNetPrimitivesTraceEventParser.ProviderGuid);
            session.EnableProvider(EtwParsers.MicrosoftSystemNetRequestsTraceEventParser.ProviderGuid);
            session.EnableProvider(EtwParsers.MicrosoftSystemNetSecurityTraceEventParser.ProviderGuid);
            session.EnableProvider(EtwParsers.MicrosoftSystemNetSocketsTraceEventParser.ProviderGuid);
            session.EnableProvider(EtwParsers.MicrosoftSystemNetWebHeaderCollectionTraceEventParser.ProviderGuid);
            session.EnableProvider(EtwParsers.MicrosoftSystemNetWebSocketsClientTraceEventParser.ProviderGuid);
        }

        private void OnAll(TraceEvent data)
        {
            if (data.ProcessID == pid) {
                switch (data) {
                    case EtwParsers.MicrosoftSystemNetSockets.DumpBufferArgs d:
                        DumpPacketBytes(d, d.thisOrContextObject, d.memberName, d.buffer);
                        break;
                    case EtwParsers.MicrosoftSystemNetSecurity.DumpBufferArgs d:
                        DumpPacketBytes(d, d.thisOrContextObject, d.memberName, d.buffer);
                        break;
                    case EtwParsers.MicrosoftSystemNetHttpListener.DumpBufferArgs d:
                        DumpPacketBytes(d, d.thisOrContextObject, d.memberName, d.buffer);
                        break;
                    case EtwParsers.MicrosoftSystemNetNetworkInformation.DumpBufferArgs d:
                        DumpPacketBytes(d, d.thisOrContextObject, d.memberName, d.buffer);
                        break;
                    case EtwParsers.MicrosoftSystemNetPing.DumpBufferArgs d:
                        DumpPacketBytes(d, d.thisOrContextObject, d.memberName, d.buffer);
                        break;
                    default:
                        LogMessage(data);
                        break;
                }
            }
        }

        private void DumpPacketBytes(TraceEvent data, string context, string memeber, byte[] buffer)
        {
            if (printPacketBytes) {
                int length = outputBytesLimit == 0 ? buffer.Length : outputBytesLimit;
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"[{context}::{memeber}] \n{Hex.PrettyPrint(buffer, 0, length)}");
            }
        }

        private void LogMessage(TraceEvent data)
        {
            const int Message = 0;
            const int Context = Message + 1;
            const int Member = Context + 1;
            const int HttpRequest = Member + 1;
            const int HttpResponse = HttpRequest + 1;
            const int HttpClient = HttpResponse + 1;
            const int Socket = HttpClient + 1;
            const int SecureChannel = Socket + 1;

            var buffer = new StringBuilder();
            var indexes = new int[SecureChannel + 1];
            indexes[Message] = data.PayloadIndex("message");
            indexes[Context] = data.PayloadIndex("thisOrContextObject");
            indexes[Member] = data.PayloadIndex("memberName");
            indexes[HttpRequest] = data.PayloadIndex("httpRequestHash");
            indexes[HttpResponse] = data.PayloadIndex("httpResponseHash");
            indexes[HttpClient] = data.PayloadIndex("httpClientHash");
            indexes[Socket] = data.PayloadIndex("socketHash");
            indexes[SecureChannel] = data.PayloadIndex("secureChannelHash");

            if (indexes[Context] >= 0) {
                buffer.AppendFormat("[{0}", data.PayloadString(indexes[Context]));
                if (indexes[Member] >= 0) {
                    buffer.AppendFormat("::{0}", data.PayloadString(indexes[Member]));
                }
                buffer.Append("] ");
            } else {
                if (indexes[HttpRequest] >= 0) {
                    buffer.AppendFormat("[HttpRequest#{0}] ", data.PayloadValue(indexes[HttpRequest]));
                }
                if (indexes[HttpResponse] >= 0) {
                    buffer.AppendFormat("[HttpResponse#{0}] ", data.PayloadValue(indexes[HttpResponse]));
                }
                if (indexes[HttpClient] >= 0) {
                    buffer.AppendFormat("[HttpClient#{0}] ", data.PayloadValue(indexes[HttpClient]));
                }
                if (indexes[Socket] >= 0) {
                    buffer.AppendFormat("[Socket#{0}] ", data.PayloadValue(indexes[Socket]));
                }
                if (indexes[SecureChannel] >= 0) {
                    buffer.AppendFormat("[SecureChannel#{0}] ", data.PayloadValue(indexes[SecureChannel]));
                }
            }
            if (indexes[Message] >= 0) {
                buffer.AppendFormat("{0} ", data.PayloadString(indexes[Message]));
            }

            var braceOpened = false;
            for (var i = 0; i < data.PayloadNames.Length; i++) {
                if (Array.IndexOf(indexes, i) >= 0) {
                    continue; // skip
                }
                if (!braceOpened) {
                    buffer.Append("{");
                    braceOpened = true;
                }
                buffer.AppendFormat(" {0}:{1}", data.PayloadNames[i], data.PayloadValue(i));
            }
            if (braceOpened) {
                buffer.Append(" }");
            }

            traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID,
                data.EventName, buffer.ToString());
        }
    }
}