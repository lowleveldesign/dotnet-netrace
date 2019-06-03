using System;
using System.Text;
using LowLevelDesign.Hexify;
using Microsoft.Diagnostics.Tracing;
using Parsers = Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Session;

namespace LowLevelDesign.NTrace.EventHandlers
{
    internal sealed class SystemNetTraceEventHandler : ITraceEventHandler
    {
        private readonly ITraceOutput traceOutput;
        private readonly int pid;
        private readonly bool printPacketBytes;
        private readonly int outputBytesLimit;

        public Parsers.KernelTraceEventParser.Keywords RequiredKernelFlags => Parsers.KernelTraceEventParser.Keywords.None;

        public SystemNetTraceEventHandler(int pid, ITraceOutput output, bool printPacketBytes, int outputBytesLimit = 0)
        {
            traceOutput = output;
            this.pid = pid;
            this.printPacketBytes = printPacketBytes;
            this.outputBytesLimit = outputBytesLimit;
        }

        public void SubscribeToSession(TraceEventSession session)
        {
            TraceEventParser parser = new Parsers.MicrosoftSystemNetHttpTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetHttpTraceEventParser.ProviderGuid);
            
            parser = new Parsers.MicrosoftSystemNetHttpListenerTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetHttpListenerTraceEventParser.ProviderGuid);
            
            parser = new Parsers.MicrosoftSystemNetMailTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetMailTraceEventParser.ProviderGuid);
            
            parser = new Parsers.MicrosoftSystemNetNameResolutionTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetNameResolutionTraceEventParser.ProviderGuid);
            
            parser = new Parsers.MicrosoftSystemNetNetworkInformationTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetNetworkInformationTraceEventParser.ProviderGuid);
            
            parser = new Parsers.MicrosoftSystemNetPingTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetPingTraceEventParser.ProviderGuid);
            
            parser = new Parsers.MicrosoftSystemNetPrimitivesTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetPrimitivesTraceEventParser.ProviderGuid);
            
            parser = new Parsers.MicrosoftSystemNetRequestsTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetRequestsTraceEventParser.ProviderGuid);
            
            parser = new Parsers.MicrosoftSystemNetSecurityTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetSecurityTraceEventParser.ProviderGuid);
            
            parser = new Parsers.MicrosoftSystemNetSocketsTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetSocketsTraceEventParser.ProviderGuid);
            
            parser = new Parsers.MicrosoftSystemNetWebHeaderCollectionTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetWebHeaderCollectionTraceEventParser.ProviderGuid);
            
            parser = new Parsers.MicrosoftSystemNetWebSocketsClientTraceEventParser(session.Source);
            parser.All += OnAll;
            session.EnableProvider(Parsers.MicrosoftSystemNetWebSocketsClientTraceEventParser.ProviderGuid);
        }

        private void OnAll(TraceEvent data)
        {
            if (data.ProcessID == pid) {
                switch (data) {
                    case Parsers.MicrosoftSystemNetSockets.DumpBufferArgs d:
                        DumpPacketBytes(d, d.thisOrContextObject, d.memberName, d.buffer);
                        break;
                    case Parsers.MicrosoftSystemNetSecurity.DumpBufferArgs d:
                        DumpPacketBytes(d, d.thisOrContextObject, d.memberName, d.buffer);
                        break;
                    case Parsers.MicrosoftSystemNetHttpListener.DumpBufferArgs d:
                        DumpPacketBytes(d, d.thisOrContextObject, d.memberName, d.buffer);
                        break;
                    case Parsers.MicrosoftSystemNetNetworkInformation.DumpBufferArgs d:
                        DumpPacketBytes(d, d.thisOrContextObject, d.memberName, d.buffer);
                        break;
                    case Parsers.MicrosoftSystemNetPing.DumpBufferArgs d:
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