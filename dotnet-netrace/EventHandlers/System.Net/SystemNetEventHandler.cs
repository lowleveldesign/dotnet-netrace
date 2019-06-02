using LowLevelDesign.Hexify;
using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.MicrosoftSystemNetSockets;
using Microsoft.Diagnostics.Tracing.Session;

namespace LowLevelDesign.NTrace.EventHandlers.System.Net
{
    internal class SystemNetEventHandler : ITraceEventHandler
    {
        private readonly ITraceOutput traceOutput;
        private readonly int pid;
        private readonly bool printPacketBytes;

        public KernelTraceEventParser.Keywords RequiredKernelFlags => KernelTraceEventParser.Keywords.None;

        public SystemNetEventHandler(int pid, bool printPacketBytes, ITraceOutput output)
        {
            traceOutput = output;
            this.printPacketBytes = printPacketBytes;
            this.pid = pid;
        }

        public void SubscribeToSession(TraceEventSession session)
        {
            var systemNetSocketsParser = new MicrosoftSystemNetSocketsTraceEventParser(session.Source);

            systemNetSocketsParser.Enter += SystemNetSocketsParserOnEnter;
            systemNetSocketsParser.Exit += SystemNetSocketsParserOnExit;
            systemNetSocketsParser.Info += SystemNetSocketsParserOnInfo;
            systemNetSocketsParser.Accepted += SystemNetSocketsParserOnAccepted;
            systemNetSocketsParser.Connected += SystemNetSocketsParserOnConnected;
            systemNetSocketsParser.ConnectedAsyncDns += SystemNetSocketsParserOnConnectedAsyncDns;
            systemNetSocketsParser.ErrorMessage += SystemNetSocketsParserOnErrorMessage;
            systemNetSocketsParser.CriticalFailure += SystemNetSocketsParserOnCriticalFailure;
            systemNetSocketsParser.EventSourceMessage += SystemNetSocketsParserOnEventSourceMessage;
            if (printPacketBytes) {
                systemNetSocketsParser.DumpBuffer += SystemNetSocketsParserOnDumpBuffer;
            }

            session.EnableProvider(MicrosoftSystemNetSocketsTraceEventParser.ProviderGuid);
        }

        private void SystemNetSocketsParserOnEventSourceMessage(EventSourceMessageArgs data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"{data.message}");
            }
        }

        private void SystemNetSocketsParserOnCriticalFailure(CriticalFailureArgs data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"[{data.thisOrContextObject}::{data.memberName}] CRITICAL {data.message}");
            }
        }

        private void SystemNetSocketsParserOnErrorMessage(ErrorMessageArgs data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"[{data.thisOrContextObject}::{data.memberName}] ERROR {data.message}");
            }
        }

        private void SystemNetSocketsParserOnConnectedAsyncDns(ConnectedAsyncDnsArgs data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"[Socket#{data.socketHash}::Accept]");
            }
        }

        private void SystemNetSocketsParserOnConnected(ConnectedArgs data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"[Socket#{data.socketHash}::Connect] {data.remoteEp} => {data.localEp}");
            }
        }

        private void SystemNetSocketsParserOnAccepted(AcceptedArgs data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"[Socket#{data.socketHash}::Connect] {data.remoteEp} => {data.localEp}");
            }
        }

        private void SystemNetSocketsParserOnInfo(InfoArgs data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"[{data.thisOrContextObject}::{data.memberName}] {data.message}");
            }
        }

        private void SystemNetSocketsParserOnExit(ExitArgs data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"[{data.thisOrContextObject}::{data.memberName}] -> {data.result}");
            }
        }

        private void SystemNetSocketsParserOnEnter(EnterArgs data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"[{data.thisOrContextObject}::{data.memberName}] ({data.parameters})");
            }
        }

        private void SystemNetSocketsParserOnDumpBuffer(DumpBufferArgs data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"[{data.thisOrContextObject}::{data.memberName}] \n{Hex.PrettyPrint(data.buffer)}");
            }
        }
    }
}