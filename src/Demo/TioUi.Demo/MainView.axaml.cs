using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using TioUi.Controls;
using TioUi.Demo.Models;

namespace TioUi.Demo;

public partial class MainView : TioView, IView
{
    private TioWindowNotificationManager? _tioWindowNotificationManager;
    private TioWindowToastManager? _tioWindowToastManager;

    public MainView()
    {
        InitializeComponent();
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

    public TioWindowNotificationManager NotificationManager
    {
        get
        {
            _tioWindowNotificationManager ??= new TioWindowNotificationManager(TopLevel.GetTopLevel(this));
            return _tioWindowNotificationManager;
        }
    }

    public TioWindowToastManager ToastManager
    {
        get
        {
            _tioWindowToastManager ??= new TioWindowToastManager(TopLevel.GetTopLevel(this));
            return _tioWindowToastManager;
        }
    }
}