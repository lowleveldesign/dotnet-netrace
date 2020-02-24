using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LowLevelDesign.NTrace
{
    public sealed class CommandLineArgumentException : Exception
    {
        public CommandLineArgumentException(string message) : base(message) { }
    }

    public static class CommandLineHelper
    {
        public static (HashSet<string> flags, Dictionary<string, string> args, string[] theRest) ParseArgs(string[] flagArgs, string[] rawArgs)
        {
            var args = rawArgs.SelectMany(arg => arg.Split(new[] { '=' },
                StringSplitOptions.RemoveEmptyEntries)).ToArray();
            bool IsFlag(string v) => Array.IndexOf(flagArgs, v) >= 0;

            var parsedArgs = new Dictionary<string, string>(StringComparer.Ordinal);
            var enabledFlags = new HashSet<string>(StringComparer.Ordinal);
            var theRest = new List<string>();
            
            var lastArg = string.Empty;
            foreach (var arg in args) {
                switch (arg) {
                    case var s when s.StartsWith("-", StringComparison.Ordinal):
                        var option = s.TrimStart('-');
                        if (IsFlag(option)) {
                            Debug.Assert(lastArg == string.Empty);
                            enabledFlags.Add(option);
                        } else {
                            Debug.Assert(lastArg == string.Empty);
                            lastArg = option;
                        }
                        break;
                    default:
                        if (lastArg != string.Empty) {
                            parsedArgs.Add(lastArg, arg);
                            lastArg = string.Empty;
                        } else {
                            theRest.Add(arg);
                        }
                        break;
                }
            }
            return (enabledFlags, parsedArgs, theRest.ToArray());
        }
    }
}