using System;
using System.Diagnostics;
using System.IO;

/* Slightly modified code from Microsoft.Diagnostics.Runtime (https://github.com/microsoft/clrmd) */
namespace LowLevelDesign.NTrace
{
    /// <summary>
    /// Returns the "flavor" of CLR this module represents.
    /// </summary>
    public enum ClrFlavor
    {
        /// <summary>
        /// This is the full version of CLR included with windows.
        /// </summary>
        Desktop = 0,

        /// <summary>
        /// For .NET Core
        /// </summary>
        Core = 3
    }
    
    /// <summary>
    /// Represents the platform.
    /// </summary>
    public enum Platform
    {
        Windows,
        Linux
    }
    
    /// <summary>
    /// Infers clr info from module names, provides corresponding DAC details.
    /// </summary>
    public static class ClrInfoProvider
    {
        private const string DesktopModuleName1 = "clr";
        private const string DesktopModuleName2 = "mscorwks";
        private const string CoreModuleName = "coreclr";
        private const string LinuxCoreModuleName = "libcoreclr";

        private static bool TryGetModuleName(ProcessModule moduleInfo, out string? moduleName)
        {
            moduleName = Path.GetFileNameWithoutExtension(moduleInfo.FileName);
            if (moduleName is null)
                return false;

#pragma warning disable CA1304 // Specify CultureInfo
            moduleName = moduleName.ToLower();
#pragma warning restore CA1304 // Specify CultureInfo
            return true;
        }

        /// <summary>
        /// Checks if the provided module corresponds to a supported runtime, gets clr details inferred from the module name.
        /// </summary>
        /// <param name="moduleInfo">Module info.</param>
        /// <param name="flavor">CLR flavor.</param>
        /// <param name="platform">Platform.</param>
        /// <returns>true if module corresponds to a supported runtime.</returns>
        public static bool IsSupportedRuntime(ProcessModule moduleInfo, out ClrFlavor flavor, out Platform platform)
        {
            if (moduleInfo is null)
                throw new ArgumentNullException(nameof(moduleInfo));

            flavor = default;
            platform = default;

            if (!TryGetModuleName(moduleInfo, out var moduleName))
                return false;

            switch (moduleName) {
                case DesktopModuleName1:
                case DesktopModuleName2:
                    flavor = ClrFlavor.Desktop;
                    platform = Platform.Windows;
                    return true;

                case CoreModuleName:
                    flavor = ClrFlavor.Core;
                    platform = Platform.Windows;
                    return true;

                case LinuxCoreModuleName:
                    flavor = ClrFlavor.Core;
                    platform = Platform.Linux;
                    return true;

                default:
                    return false;
            }
        }
    }
}