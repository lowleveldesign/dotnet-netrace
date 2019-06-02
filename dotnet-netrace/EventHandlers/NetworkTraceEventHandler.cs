using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;

namespace LowLevelDesign.NTrace.EventHandlers
{
    internal class NetworkTraceEventHandler : ITraceEventHandler
    {
        private readonly ITraceOutput traceOutput;
        private readonly int pid;

        public KernelTraceEventParser.Keywords RequiredKernelFlags => KernelTraceEventParser.Keywords.NetworkTCPIP;

        public NetworkTraceEventHandler(int pid, ITraceOutput output)
        {
            traceOutput = output;
            this.pid = pid;
        }

        public void SubscribeToSession(TraceEventSession session)
        {
            var kernel = session.Source.Kernel;
            kernel.TcpIpAccept += HandleTcpIpConnect;
            kernel.TcpIpAcceptIPV6 += HandleTcpIpV6Connect;
            kernel.TcpIpARPCopy += HandleTcpIp;
            kernel.TcpIpConnect += HandleTcpIpConnect;
            kernel.TcpIpConnectIPV6 += HandleTcpIpV6Connect;
            kernel.TcpIpDisconnect += HandleTcpIp;
            kernel.TcpIpDisconnectIPV6 += HandleTcpIpV6;
            kernel.TcpIpDupACK += HandleTcpIp;
            kernel.TcpIpFail += HandleTcpIpFail;
            kernel.TcpIpFullACK += HandleTcpIp;
            kernel.TcpIpPartACK += HandleTcpIp;
            kernel.TcpIpReconnect += HandleTcpIp;
            kernel.TcpIpReconnectIPV6 += HandleTcpIpV6;
            kernel.TcpIpRecv += HandleTcpIpRev;
            kernel.TcpIpRecvIPV6 += HandleTcpIpV6Rev;
            kernel.TcpIpRetransmit += HandleTcpIp;
            kernel.TcpIpRetransmitIPV6 += HandleTcpIpV6;
            kernel.TcpIpSend += HandleTcpIpSend;
            kernel.TcpIpSendIPV6 += HandleTcpIpV6Send;
            kernel.TcpIpTCPCopy += HandleTcpIp;
            kernel.TcpIpTCPCopyIPV6 += HandleTcpIpV6;
        }

        private void HandleTcpIpConnect(TcpIpConnectTraceData data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName, 
                    $"{data.saddr}:{data.sport} -> {data.daddr}:{data.dport} (0x{data.connid:X})");
            }
        }

        private void HandleTcpIpV6Connect(TcpIpV6ConnectTraceData data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"{data.saddr}:{data.sport} -> {data.daddr}:{data.dport} (0x{data.connid:X})");
            }
        }

        private void HandleTcpIp(TcpIpTraceData data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"{data.saddr}:{data.sport} -> {data.daddr}:{data.dport} (0x{data.connid:X})");
            }
        }

        private void HandleTcpIpV6(TcpIpV6TraceData data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"{data.saddr}:{data.sport} -> {data.daddr}:{data.dport} (0x{data.connid:X})");
            }
        }

        private void HandleTcpIpRev(TcpIpTraceData data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"{data.daddr}:{data.dport} <- {data.saddr}:{data.sport} (0x{data.connid:X})");
            }
        }

        private void HandleTcpIpV6Rev(TcpIpV6TraceData data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"{data.daddr}:{data.dport} <- {data.saddr}:{data.sport} (0x{data.connid:X})");
            }
        }

        private void HandleTcpIpFail(TcpIpFailTraceData data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"0x{data.FailureCode:X}");
            }
        }

        private void HandleTcpIpSend(TcpIpSendTraceData data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"{data.saddr}:{data.sport} -> {data.daddr}:{data.dport} (0x{data.connid:X})");
            }
        }

        private void HandleTcpIpV6Send(TcpIpV6SendTraceData data)
        {
            if (data.ProcessID == pid) {
                traceOutput.Write(data.TimeStampRelativeMSec, data.ProcessID, data.ThreadID, data.EventName,
                    $"{data.saddr}:{data.sport} -> {data.daddr}:{data.dport} (0x{data.connid:X})");
            }
        }
    }
}
