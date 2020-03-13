
# .NET network trace (dotnet-netrace)

This application will trace in real-time all the network logs produced by a .NET process. It works on Windows and Linux and requires .NET 4.7.2+ or .NET Core 2.1+. Dotnet-netrace can trace both .NET Core and Full .NET applications. It stops when the traced process exits, or if you issue Ctrl+C in the command line window.

The available options are:

```
dotnet-netrace v2.0.20073.7 - collects network traces from .NET applications
Copyright (C) 2020 Sebastian Solnica (https://lowleveldesign.org)

Usage: dotnet-netrace [OPTIONS] pid|image-name args

Options:
 -f, --filter=VALUE    Display only events which names contain the given keyword (case insensitive).
                       Does not impact the summary.
 -b, --bytes           Dump packet bytes to the console.
 -h, --help            Show this message and exit.
 -?                    Show this message and exit.
 ``` 

## .NET Core

.NET Core network logs are produced by a set of [**event providers**](https://github.com/dotnet/corefx/blob/master/Documentation/debugging/windows-instructions.md#systemnet-namespaces). Dotnet-netrace listens to all of them and dumps events in the console output. A trace for a simple ASP.NET Core application might look as follows:

```
PS me> dotnet-netrace.exe 23520
...
3128,0142 (23520.22544) Info [Socket#52017160::SetToConnected] now connected
3128,2324 (23520.22544) Enter [Socket#52017160::InternalSetBlocking] { parameters:desired:True willBlock:True willBlockInternal:True }
3128,4725 (23520.22544) Info [Socket#52017160::InternalSetBlocking] Interop.Winsock.ioctlsocket returns errorCode:Success
3128,4902 (23520.22544) Info [Socket#52017160::InternalSetBlocking] errorCode:Success willBlock:True willBlockInternal:True
3134,3724 (23520.22544) Accepted [Socket#52017160] { remoteEp:IPEndPoint#1980980866 localEp:IPEndPoint#1980994208 }
3134,5414 (23520.22544) Info [Socket#52017160::SetSocketOption] optionLevel:Tcp optionName:Debug optionValue:1
3134,5814 (23520.22544) Enter [Socket#52017160::SetSocketOption] { parameters:optionLevel:Tcp optionName:Debug optionValue:1 silent:False }
3134,6196 (23520.22544) Info [Socket#52017160::SetSocketOption] Interop.Winsock.setsockopt returns errorCode:Success
3134,6694 (23520.22544) Info [SocketAwaitableEventArgs#53575368::InitializeInternals] new PreAllocatedOverlapped PreAllocatedOverlapped#31230885
3134,6758 (23520.22544) Info [SocketAwaitableEventArgs#36283542::InitializeInternals] new PreAllocatedOverlapped PreAllocatedOverlapped#45234717
3134,7602 (23520.22544) Enter [Socket#52017160::ReceiveAsync] { parameters:(SocketAwaitableEventArgs#53575368) }
3134,7783 (23520.22544) Info [SafeCloseSocket#32481668::GetOrAllocateThreadPoolBoundHandle] calling ThreadPool.BindHandle()
3134,8036 (23520.22544) Exit [Socket#52017160::ReceiveAsync] { result:False }
3134,8134 (23520.22544) Enter [Socket#52017160::ReceiveAsync] { parameters:(SocketAwaitableEventArgs#53575368) }
3134,8259 (23520.22544) Exit [Socket#52017160::ReceiveAsync] { result:False }
3134,8312 (23520.22544) Enter [Socket#52017160::ReceiveAsync] { parameters:(SocketAwaitableEventArgs#53575368) }
3134,8390 (23520.22544) Exit [Socket#52017160::ReceiveAsync] { result:True }
3134,9131 (23520.22544) Enter [Socket#19255579::AcceptAsync] { parameters:(System.Net.Sockets.Socket+TaskSocketAsyncEventArgs`1[System.Net.Sockets.Socket]) }
3134,9609 (23520.22544) Enter [Socket#45166203::.ctor] { parameters:(InterNetworkV6) }
3134,9829 (23520.17016) Enter [SecureChannel#55744491::.ctor] { parameters:(, (null)) }
3135,0072 (23520.17016) SecureChannelCtor [SecureChannel#55744491] { hostname: clientCertificatesCount:0 encryptionPolicy:RequireEncryption }
3135,0162 (23520.17016) Enter [(null)::EnumerateSecurityPackages] { parameters: }
3135,0191 (23520.17016) Exit [(null)::EnumerateSecurityPackages] { result: }
3135,0247 (23520.17016) Exit [SecureChannel#55744491::.ctor] { result: }
3135,0530 (23520.22544) Info [(null)::CreateSocket] SafeCloseSocket:18710213(0x2760)
3135,0586 (23520.17016) Info [LazyAsyncResult#60261809::.ctor]
3135,0621 (23520.22544) Exit [Socket#45166203::.ctor] { result: }
3135,0786 (23520.22544) Exit [Socket#19255579::AcceptAsync] { result:True }
3135,0828 (23520.17016) Enter [SslState#2916651::GetRemainingFrameSize] { parameters:(System.Byte[5], 0, 5) }
3135,0857 (23520.17016) Exit [SslState#2916651::GetRemainingFrameSize] { result:512 }
3135,0928 (23520.17016) Enter [SecureChannel#55744491::NextMessage] { parameters: }
3135,1002 (23520.17016) Enter [SecureChannel#55744491::AcquireServerCredentials] { parameters: }
3156,6801 (23520.17016) LocatingPrivateKey [SecureChannel#55744491] { x509Certificate:[Version]
  V3

[Subject]
  CN=localhost
  Simple Name: localhost
  DNS Name: localhost

[Issuer]
  CN=localhost
  Simple Name: localhost
  DNS Name: localhost

[Serial Number]
  7B1E0C5B3322684D

[Not Before]
  21.10.2018 16:05:03

[Not After]
  21.10.2019 16:05:03

[Thumbprint]
  EB66159A58282734DC79FF92EDD73E7F5CF4143C

[Signature Algorithm]
  sha256RSA(1.2.840.113549.1.1.11)

[Public Key]
  Algorithm: RSA
  Length: 2048
  Key Blob: 30 82 01 0a 02 82 01 01 00 d1 33 55 fe 2f 45 04 9d ee 69 4b 37 d3 7d 13 79 21 00 8c 51 61 18 14 70 db b6 e9 a3 39 1b 27 33 39 26 a4 cb 19 aa 4d 59 d6 3b 3c 22 7b 70 dc 17 19 39 29 42 a6 fe 2e b7 fc 13 9e c6 4c d1 27 0c 1a f6 fe db 7d a3 f1 32 a4 70 f4 46 d5 70 eb be 95 0e 04 25 d4 df 74 64 7e 16 8d 07 3c 37 75 a4 2c d2 fc 23 58 c0 9d 26 14 88 35 be c1 c8 2f 2b e6 48 4f 29 af 53 fe 41 46 44 42 37 67 ac d8 29 27 e9 61 3f ae db bf 8a 2c e4 76 e2 d8 da 75 3a 40 e3 2a c6 10 91 9f d1 ff 7a 6a 31 c4 12 4e f4 ae 42 7e 20 9e b6 42 e3 c6 95 ca ea 68 66 ea 05 dc ac 94 3f 56 6b cc 80 35 50 94 95 4d 63 bb e6 11 19 be ef ae 0c 70 1f 39 77 0f 7b ad f3 75 69 a0 4e 0b c1 bf f9 0d b6 63 99 52 15 ac db 6d b6 e3 41 e5 3c 19 1d 4f cc e1 30 7e 2c 20 67 0a 85 c3 75 87 a5 59 de 4f 48 37 f5 d4 75 94 5d 8e 7d 02 03 01 00 01
  Parameters: 05 00
...
```

As you see, you may learn a lot about what your app does when it serves or sends requests. This might be invaluable information when you are struggling with network or security issues (for example, invalid TLS certificate, unsupported TLS version, etc.). If you want to see raw bytes, you may use the **-b** option. Unfortunately, you won't see all of the packet bytes - there is a hard limit on the buffer size, but it still might be useful.

## Full .NET Framework

"Old" .NET Framework uses the **System.Diagnostics** objects to send the network logs. Unfortunately, it's not that flexible, and it's impossible to enable the logs while the application is running. If you want to collect the logs, you need to configure a set of [**TraceSources**](https://docs.microsoft.com/en-us/dotnet/framework/network-programming/how-to-configure-network-tracing). For your convenience, I added support for the old .NET logs in the dotnet-netrace too. You need to redirect the logs to a custom **EventProviderTraceListener** with an id: **e4144c8f-cc80-4797-a7cc-cfe14de522ea**.

A sample app.config file might look as follows:

```
<system.diagnostics>
  <trace autoflush="true" />
  <sharedListeners>
      <add name="netrace" initializeData="e4144c8f-cc80-4797-a7cc-cfe14de522ea" type="System.Diagnostics.Eventing.EventProviderTraceListener, System.Core, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" />
  </sharedListeners>
  <sources>
    <source name="System.Net.Http" switchValue="Verbose">
      <listeners>
        <add name="netrace" />
      </listeners>
    </source>
    <source name="System.Net.HttpListener" switchValue="Verbose">
      <listeners>
        <add name="netrace" />
      </listeners>
    </source>
    <source name="System.Net" switchValue="Verbose">
      <listeners>
        <add name="netrace" />
      </listeners>
    </source>
    <source name="System.Net.Sockets" switchValue="Verbose">
      <listeners>
        <add name="netrace" />
      </listeners>
    </source>
  </sources>
</system.diagnostics>
```

The impact on the performance should be much smaller than when logging to a text file. With this app.config in place, you may run dotnet-trace and collect the logs:

```
PS me> dotnet-netrace.exe 20564
...
11135,5401 (20564.12616) Log [12616] Entering Socket#258006::Socket()
11135,5863 (20564.12616) Log [12616] Exiting Socket#258006::Socket()
11138,4309 (20564.12616) Log [12616] Entering TcpListener#463695::EndAcceptTcpClient()
11139,9691 (20564.12616) Log [12616] Entering Socket#26430654::EndAccept(AcceptAsyncResult#30152281)
11142,7079 (20564.12616) Log [12616] Socket#258006 - Accepted connection from 127.0.0.1:57416 to 127.0.0.1:33899.
11142,7302 (20564.12616) Log [12616] Exiting Socket#26430654::EndAccept()       -> Socket#258006
11142,7472 (20564.12616) Log [12616] Exiting TcpListener#463695::EndAcceptTcpClient()   -> Socket#258006
11142,9986 (20564.12616) Log [12616] Entering TcpClient#10578254::TcpClient(Socket#258006)
11143,0157 (20564.12616) Log [12616] Exiting TcpClient#10578254::TcpClient()
11143,4496 (20564.12616) Log [12616] Entering TcpClient#10578254::GetStream()
11144,2617 (20564.12616) Log [12616] Exiting TcpClient#10578254::GetStream()    -> NetworkStream#31055270
11146,3342 (20564.12616) Log [12616] Entering Socket#258006::BeginSend()
11154,8908 (20564.12616) Log [12616] Exiting Socket#258006::BeginSend()         -> OverlappedAsyncResult#65306533
11159,2557 (20564.23044) Log [23044] Data from Socket#258006::PostCompletion
11160,8629 (20564.23044) Log [23044] 00000000 : 74 65 73 74                                     : test
11162,7668 (20564.23044) Log [23044] Entering Socket#258006::EndSend(OverlappedAsyncResult#65306533)
11163,8469 (20564.23044) Log [23044] Exiting Socket#258006::EndSend()   -> Int32#4
11166,1400 (20564.23044) Log [23044] Entering Socket#258006::Dispose()
11167,1662 (20564.23044) Log [23044] Entering TcpClient#10578254::Close()
11167,6693 (20564.23044) Log [23044] Entering TcpClient#10578254::Dispose()
11167,6929 (20564.23044) Log [23044] Exiting TcpClient#10578254::Dispose()
11167,7112 (20564.23044) Log [23044] Exiting TcpClient#10578254::Close()
...
```
