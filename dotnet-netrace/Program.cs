using System;
using System.Reflection;
using System.Runtime.InteropServices;
using LowLevelDesign.NTrace.Utilities;

namespace LowLevelDesign.NTrace
{
    internal static class Program
    {
        private static readonly Assembly AppAssembly = Assembly.GetExecutingAssembly();
        private static readonly AssemblyName AppName = AppAssembly.GetName();

        [STAThread()]
        public static void Main(string[] args)
        {
            if ((RuntimeInformation.FrameworkDescription ?? "").Contains(".NET Framework")) {
                Unpack();
            }

            DoMain(args);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private static void DoMain(string[] args)
        {
            var flags = new[] { "b", "bytes", "c", "children", "h", "?", "help" };
            var parsedArgs = CommandLineHelper.ParseArgs(flags, args);

            if (parsedArgs.flags.Contains("h") || parsedArgs.flags.Contains("help") ||
                parsedArgs.flags.Contains("?")) {
                ShowHelp();
                return;
            }

            try {
                if (parsedArgs.theRest.Length == 0) {
                    throw new CommandLineArgumentException(
                        "You need to provide either a process ID or a path to the process executable.");
                }

                if (!parsedArgs.args.TryGetValue("filter", out var eventNameFilter)) {
                    eventNameFilter = string.Empty;
                }

                var traceSession = new TraceSession(new ConsoleTraceOutput(eventNameFilter));
                var traceOptions = new TraceSession.TraceOptions {
                    PrintPacketBytes = parsedArgs.flags.Contains("b") || parsedArgs.flags.Contains("bytes"),
                };

                SetConsoleCtrlCHook(traceSession);

                var procArgs = parsedArgs.theRest;
                if (!int.TryParse(procArgs[0], out var pid)) {
                    traceSession.TraceNewProcess(procArgs, traceOptions);
                } else {
                    traceSession.TraceRunningProcess(pid, traceOptions);
                }
            } catch (Exception ex) when (ex is CommandLineArgumentException || ex is ArgumentException) {
                Console.WriteLine($"[error] {ex.Message}");
                Console.WriteLine();
                Console.WriteLine($"{AppName.Name} -help to see usage info.");
                Console.WriteLine();
            } catch (Exception ex) {
                Console.WriteLine($"[critical] {ex.Message}");
                Console.WriteLine("If this error persists, please report it at https://github.com/lowleveldesign/dotnet-netrace/issues, " +
                                  "providing the below details.");
                Console.WriteLine("=== Details ===");
                Console.WriteLine($"{ex.GetType()}: {ex.Message}");
                Console.WriteLine();
                Console.WriteLine($"Command line: {Environment.CommandLine}");
                Console.WriteLine($"OS: {Environment.OSVersion}");
                Console.WriteLine($"x64 (OS): {Environment.Is64BitOperatingSystem}");
                Console.WriteLine($"x64 (Process): {Environment.Is64BitProcess}");
                Console.WriteLine($"Exception details: {ex}");
            }
        }

        private static void SetConsoleCtrlCHook(TraceSession processTraceRunner)
        {
            // Set up Ctrl-C to stop both user mode and kernel mode sessions
            Console.CancelKeyPress += (sender, cancelArgs) => {
                cancelArgs.Cancel = true;
                processTraceRunner.Stop();
            };
        }

        private static void ShowHelp()
        {
            var customAttrs = AppAssembly.GetCustomAttributes(typeof(AssemblyCompanyAttribute), true);
            Console.WriteLine($"{AppName.Name} v{AppName.Version} - collects network traces from .NET applications");
            Console.WriteLine($"Copyright (C) {DateTime.Today.Year} {((AssemblyCompanyAttribute)customAttrs[0]).Company}");
            Console.WriteLine();
            Console.WriteLine($"Usage: {AppName.Name} [OPTIONS] pid|image-name args");
            Console.WriteLine();
            Console.WriteLine("Options:");
            Console.WriteLine(" -f, --filter=VALUE    Display only events which names contain the given keyword (case insensitive).");
            Console.WriteLine("                       Does not impact the summary.");
            Console.WriteLine(" -b, --bytes           Dump packet bytes to the console.");
            Console.WriteLine(" -h, --help            Show this message and exit.");
            Console.WriteLine(" -?                    Show this message and exit.");
            Console.WriteLine();
        }

        /// <summary>
        /// Unpacks all the support files associated with this program.   
        /// </summary>
        private static void Unpack()
        {
            SupportFiles.UnpackResourcesIfNeeded();
        }
    }
}