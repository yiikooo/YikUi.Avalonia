using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace YikUi.Common.Helpers;

[SupportedOSPlatform("MacOS")]
public static class MacOsWindowHandler
{
    public static void HideZoomButton(IntPtr nsWindow)
    {
        var selStandardWindowButton = sel_registerName("standardWindowButton:");
        var selSetHidden = sel_registerName("setHidden:");

        var zoomButton = objc_msgSend_IntPtr_IntPtr(nsWindow, selStandardWindowButton, new IntPtr(2));
        objc_msgSend_Bool(zoomButton, selSetHidden, true);
    }

    public static void RefreshTitleBarButtonPosition(IntPtr nsWindow, double x = 14, double y = 2, double spacing = 20)
    {
        var selStandardWindowButton = sel_registerName("standardWindowButton:");
        var selSetFrameOrigin = sel_registerName("setFrameOrigin:");

        if (selStandardWindowButton == IntPtr.Zero || selSetFrameOrigin == IntPtr.Zero)
            throw new NullReferenceException("Failed to get selector for standardWindowButton or setFrameOrigin");

        var closeButton = objc_msgSend_IntPtr_IntPtr(nsWindow, selStandardWindowButton, IntPtr.Zero);
        var minimizeButton = objc_msgSend_IntPtr_IntPtr(nsWindow, selStandardWindowButton, new IntPtr(1));
        var zoomButton = objc_msgSend_IntPtr_IntPtr(nsWindow, selStandardWindowButton, new IntPtr(2));

        var newCloseButtonPosition = new CGPoint(x, y);
        var newMinimizeButtonPosition = new CGPoint(x + spacing, y);
        var newZoomButtonPosition = new CGPoint(x + spacing * 2, y);

        objc_msgSend_CGPoint(closeButton, selSetFrameOrigin, newCloseButtonPosition);
        objc_msgSend_CGPoint(minimizeButton, selSetFrameOrigin, newMinimizeButtonPosition);
        objc_msgSend_CGPoint(zoomButton, selSetFrameOrigin, newZoomButtonPosition);
    }

    [DllImport("libobjc.dylib", EntryPoint = "objc_msgSend")]
    public static extern IntPtr objc_msgSend_IntPtr_IntPtr(IntPtr receiver, IntPtr selector, IntPtr arg1);

    [DllImport("libobjc.dylib", EntryPoint = "objc_msgSend")]
    public static extern void objc_msgSend_CGPoint(IntPtr receiver, IntPtr selector, CGPoint arg1);

    [DllImport("libobjc.dylib", EntryPoint = "objc_msgSend")]
    public static extern IntPtr objc_msgSend_Bool(IntPtr receiver, IntPtr selector, bool arg1);

    [DllImport("libobjc.dylib", EntryPoint = "sel_registerName")]
    public static extern IntPtr sel_registerName(string name);
}

[StructLayout(LayoutKind.Sequential)]
public struct CGPoint
{
    public double X;
    public double Y;

    public CGPoint(double x, double y)
    {
        X = x;
        Y = y;
    }
}