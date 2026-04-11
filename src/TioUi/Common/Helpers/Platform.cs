using System.Runtime.InteropServices;

namespace TioUi.Common.Helpers;

public class Platform
{
    public static DesktopType DetectPlatform()
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows)) return DesktopType.Windows;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux)) return DesktopType.Linux;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX)) return DesktopType.MacOs;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.FreeBSD)) return DesktopType.FreeBSD;

        return DesktopType.Unknown;
    }
}