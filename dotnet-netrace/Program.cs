using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using LowLevelDesign.NTrace.Utilities;
using Microsoft.Diagnostics.Tracing.Session;
using NDesk.Options;

namespace LowLevelDesign.NTrace
{
    internal static class Program
    {
        [STAThread()]
        public static void Main(string[] args)
        {
            Unpack();

            DoMain(args);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.NoInlining)]
        private static void DoMain(string[] args)
        {
            List<string> procargs = null;
            var showhelp = false;
            var spawnNewConsoleWindow = false;
            var traceOptions = new TraceSession.TraceOptions();
            string eventNameFilter = null;

            var p = new OptionSet {
                {
                    "f|filter=", "Display only events which names contain the given keyword " +
                                 "(case insensitive). Does not impact the summary.",
                    v => { eventNameFilter = v; }
                },
                {"b|bytes", "Dump packet bytes to the console.", v => { traceOptions.PrintPacketBytes = v != null; }},
                {"c|children", "Trace process and all its children.", v => { traceOptions.TraceChildProcesses = v != null; }},
                {"newconsole", "Start the process in a new console window.", v => { spawnNewConsoleWindow = v != null; }},
                {"h|help", "Show this message and exit.", v => showhelp = v != null},
                {"?", "Show this message and exit.", v => showhelp = v != null}
            };

            try {
                procargs = p.Parse(args);
            } catch (OptionException ex) {
                Console.Error.Write("ERROR: invalid argument");
                Console.Error.WriteLine(ex.Message);
                Console.Error.WriteLine();
                showhelp = true;
            } catch (FormatException) {
                Console.Error.WriteLine("ERROR: invalid number in one of the constraints");
                Console.Error.WriteLine();
                showhelp = true;
            }
            
            if (traceOptions.TraceChildProcesses && TraceEventSession.IsElevated() != true) {
                Console.Error.WriteLine("Must run elevated (Admin) to trace process children.");
                return;
            }


            if (!showhelp && procargs != null && procargs.Count == 0) {
                Console.Error.WriteLine("ERROR: please provide either process name or PID");
                Console.Error.WriteLine();
                showhelp = true;
            }

            if (showhelp) {
                ShowHelp(p);
                return;
            }

            // for diagnostics information
#if DEBUG
            Trace.Listeners.Add(new ConsoleTraceListener());
#endif

            var traceSession = new TraceSession(new ConsoleTraceOutput(eventNameFilter));

            SetConsoleCtrlCHook(traceSession);

            try {
                if (!int.TryParse(procargs[0], out var pid)) {
                    traceSession.TraceNewProcess(procargs, spawnNewConsoleWindow, traceOptions);
                } else {
                    traceSession.TraceRunningProcess(pid, traceOptions);
                }
            } catch (COMException ex) {
                if ((uint) ex.HResult == 0x800700B7) {
                    Console.Error.WriteLine("ERROR: could not start the kernel logger - make sure it is not running.");
                }
            } catch (Win32Exception ex) {
                Console.Error.WriteLine(
                    $"ERROR: an error occurred while trying to start or open the process, hr: 0x{ex.HResult:X8}, " +
                    $"code: 0x{ex.NativeErrorCode:X8} ({ex.Message}).");
            }
#if !DEBUG
            catch (Exception ex) {
                Console.Error.WriteLine($"ERROR: severe error happened when starting application: {ex.Message}");
            }
#endif
        }

        private static void SetConsoleCtrlCHook(TraceSession processTraceRunner)
        {
            // Set up Ctrl-C to stop both user mode and kernel mode sessions
            Console.CancelKeyPress += (sender, cancelArgs) => {
                cancelArgs.Cancel = true;
                processTraceRunner.Stop();
            };
        }

        private static void ShowHelp(OptionSet p)
        {
            var assemblyName = Assembly.GetExecutingAssembly().GetName();
            Console.WriteLine($"{assemblyName.Name} v{assemblyName.Version} - collects network traces from .NET applications");
            Console.WriteLine("Copyright (C) 2019 Sebastian Solnica (@lowleveldesign)");
            Console.WriteLine();
            Console.WriteLine($"Usage: {assemblyName.Name} [OPTIONS] pid|image-name args");
            Console.WriteLine();
            Console.WriteLine("Options:");
            p.WriteOptionDescriptions(Console.Out);
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