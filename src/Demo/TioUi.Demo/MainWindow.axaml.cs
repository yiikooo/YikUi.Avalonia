using System;
using System.Windows.Input;
using Avalonia;
using TioUi.Common;
using TioUi.Common.Helpers;
using TioUi.Controls;

namespace TioUi.Demo;

public partial class MainWindow : TioWindow
{
    private MainWindowModel _mainWindowModel;

    public MainWindow()
    {
        InitializeComponent();
        _mainWindowModel = new MainWindowModel();
        DataContext = _mainWindowModel;
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

    public TioWindowNotificationManager notification => _mainWindowModel.MainView.notification;
    public TioWindowToastManager toast => _mainWindowModel.MainView.toast;
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