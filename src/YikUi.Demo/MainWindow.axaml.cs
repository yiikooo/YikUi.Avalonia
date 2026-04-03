using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Notifications;
using Avalonia.Input;
using Avalonia.Styling;
using YikUi.Common;
using YikUi.Common.Helpers;
using YikUi.Controls;

namespace YikUi.Demo;

public partial class MainWindow : YikWindow
{
    public readonly YikWindowNotificationManager notification;
    public readonly YikWindowToastManager toast;
    private MainWindowModel _mainWindowModel;

    public MainWindow()
    {
        InitializeComponent();
        _mainWindowModel = new MainWindowModel();
        DataContext = _mainWindowModel;
        toast = new YikWindowToastManager(GetTopLevel(this));
        notification = new YikWindowNotificationManager(GetTopLevel(this))
        {
            Position = NotificationPosition.TopRight
        };
        KeyBindings.Add(new KeyBinding
        {
            Gesture = KeyGesture.Parse("Ctrl+Q"),
            Command = new ActionCommand(() => ToggleTheme())
        });
        NavMenu.SearchPlaceholderText += $"   ({_mainWindowModel.Items} items)";
        if (Platform.DetectPlatform() == DesktopType.MacOs)
        {
            Separator.IsVisible = false;
            Left.Margin = new Thickness(50, 0, 0, 0);
            IsMaxBtnShow = false;
            IsMinBtnShow = false;
            IsCloseBtnShow = false;
            PropertyChanged += (_, _) =>
            {
                var platform = TryGetPlatformHandle();
                if (platform is null) return;
                var nsWindow = platform.Handle;
                if (nsWindow == IntPtr.Zero) return;
                try
                {
                    MacOsWindowHandler.RefreshTitleBarButtonPosition(nsWindow);
                    MacOsWindowHandler.HideZoomButton(nsWindow);
                }
                catch
                {
                    // ignored
                }
            };
        }
    }

    private void ToggleTheme(string? theme = null)
    {
        if (Application.Current != null && theme == null)
        {
            Application.Current.RequestedThemeVariant =
                Application.Current.ActualThemeVariant == ThemeVariant.Dark
                    ? ThemeVariant.Light
                    : ThemeVariant.Dark;
        }

        if (theme == "a")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Default;
        else if (theme == "l")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Light;
        else if (theme == "d")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Dark;
    }
}

public class ActionCommand : ICommand
{
    private readonly Action _execute;

    public ActionCommand(Action execute)
    {
        _execute = execute;
    }

    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) => _execute();
    public event EventHandler? CanExecuteChanged;
}