using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Controls.Notifications;
using Avalonia.Input;
using Avalonia.Styling;
using YikUi.Controls;

namespace YikUi.Demo;

public partial class MainWindow : YikWindow
{
    private readonly YikWindowNotificationManager notification;
    public readonly YikWindowToastManager toast;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowModel();
        toast = new YikWindowToastManager(GetTopLevel(this));
        notification = new YikWindowNotificationManager(GetTopLevel(this))
        {
            Position = NotificationPosition.TopRight
        };
        KeyBindings.Add(new KeyBinding
        {
            Gesture = KeyGesture.Parse("Ctrl+Q"),
            Command = new ActionCommand(ToggleTheme)
        });
    }

    private void ToggleTheme()
    {
        if (Application.Current != null)
        {
            Application.Current.RequestedThemeVariant =
                Application.Current.ActualThemeVariant == ThemeVariant.Dark
                    ? ThemeVariant.Light
                    : ThemeVariant.Dark;
        }
    }
}

public class ActionCommand(Action execute) : ICommand
{
    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) => execute();
    public event EventHandler? CanExecuteChanged;
}