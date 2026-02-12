using System.Runtime.InteropServices;
using Avalonia.Logging;

namespace YikUi.Common.Helper;

public class Platform
{
    public static DesktopType DetectPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            return DesktopType.Windows;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            return DesktopType.Linux;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
        {
            return DesktopType.MacOs;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD))
        {
            return DesktopType.FreeBSD;
        }
        else
        {
            return DesktopType.Unknown;
        }
    }
}