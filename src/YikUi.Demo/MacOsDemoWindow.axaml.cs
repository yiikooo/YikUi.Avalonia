using System;
using Avalonia.Interactivity;
using YikUi.Common;
using YikUi.Common.Helpers;
using YikUi.Controls;

namespace YikUi.Demo;

public partial class MacOsDemoWindow : YikWindow
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