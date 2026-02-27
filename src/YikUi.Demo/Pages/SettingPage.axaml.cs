using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;

namespace YikUi.Demo.Pages;

public partial class SettingPage : PageModelBase
{
    public SettingPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public ThemeVariant SelectedThemeVariant
    {
        set
        {
            SetField(ref field, value);
            Application.Current.RequestedThemeVariant = value;
        }
        get;
    } = ThemeVariant.Default;

    public List<ThemeVariant> ThemeVariants { get; } =
    [
        ThemeVariant.Default,
        ThemeVariant.Light,
        ThemeVariant.Dark,
    ];

    private void InputElement_OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.Key is Key.Enter)
        {
            var yikTheme = Application.Current?.Styles
                .OfType<YikUiTheme>()
                .FirstOrDefault();
            yikTheme?.SetThemeColor(Color.Parse(ThemeColor.Text!));
        }
    }
}