using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Input;
using Avalonia.Styling;
using TioUi.Controls;

namespace TioUi.Demo;

public partial class MainView : UserControl
{
    public readonly MainViewModel _mainViewModel;
    public readonly TioWindowNotificationManager notification;
    public readonly TioWindowToastManager toast;

    public MainView()
    {
        InitializeComponent();
        _mainViewModel = new MainViewModel();
        DataContext = _mainViewModel;
        toast = new TioWindowToastManager(TopLevel.GetTopLevel(this));
        notification = new TioWindowNotificationManager(TopLevel.GetTopLevel(this))
        {
            Position = NotificationPosition.TopRight
        };
        NavMenu.SearchPlaceholderText += $"   ({_mainViewModel.Items} items)";
        KeyBindings.Add(new KeyBinding
        {
            Gesture = KeyGesture.Parse("Ctrl+Q"),
            Command = new ActionCommand(() => ToggleTheme())
        });
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