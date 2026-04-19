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
    private TioNotificationManager? _tioWindowNotificationManager;
    private TioToastManager? _tioWindowToastManager;

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

    public TioNotificationManager NotificationManager
    {
        get
        {
            _tioWindowNotificationManager ??= new TioNotificationManager(TopLevel.GetTopLevel(this));
            return _tioWindowNotificationManager;
        }
    }

    public TioToastManager ToastManager
    {
        get
        {
            _tioWindowToastManager ??= new TioToastManager(TopLevel.GetTopLevel(this));
            return _tioWindowToastManager;
        }
    }
}