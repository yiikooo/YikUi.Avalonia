using System;
using Avalonia.Interactivity;
using TioUi.Common;
using TioUi.Common.Helpers;
using TioUi.Controls;

namespace TioUi.Demo;

public partial class MacOsDemoWindow : TioWindow
{
    public MacOsDemoWindow()
    {
        InitializeComponent();
        if (Platform.DetectPlatform() == DesktopType.MacOs)
        {
            PropertyChanged += (_, _) =>
            {
                var platform = TryGetPlatformHandle();
                if (platform is null) return;
                var nsWindow = platform.Handle;
                if (nsWindow == IntPtr.Zero) return;
                try
                {
                    MacOsWindowHandler.RefreshTitleBarButtonPosition(nsWindow, XCoordinate.Value ?? 14,
                        YCoordinate.Value ?? 2, ButtonSpacing.Value ?? 20);
                    MacOsWindowHandler.HideZoomButton(nsWindow);
                }
                catch
                {
                    // ignored
                }
            };
        }
    }

    private void SetButton_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Platform.DetectPlatform() == DesktopType.MacOs)
            PropertyChanged += (_, _) =>
            {
                var platform = TryGetPlatformHandle();
                if (platform is null) return;
                var nsWindow = platform.Handle;
                if (nsWindow == IntPtr.Zero) return;
                try
                {
                    MacOsWindowHandler.RefreshTitleBarButtonPosition(nsWindow, XCoordinate.Value ?? 14,
                        YCoordinate.Value ?? 2, ButtonSpacing.Value ?? 20);
                    MacOsWindowHandler.HideZoomButton(nsWindow);
                }
                catch
                {
                    // ignored
                }
            };
    }
}