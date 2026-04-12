using Avalonia.Interactivity;
using Avalonia.Styling;
using TioUi.Demo.Models;

namespace TioUi.Demo.Pages;

public partial class ThemeVariantScopePage : PageModelBase
{
    public ThemeVariantScopePage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public ThemeVariant SelectedThemeVariant
    {
        set => SetField(ref field, value);
        get;
    } = ThemeVariant.Dark;

    private void OnIsCheckedChanged(object? sender, RoutedEventArgs e)
    {
        if (Default.IsChecked == true) SelectedThemeVariant = ThemeVariant.Default;
        else if (Dark.IsChecked == true) SelectedThemeVariant = ThemeVariant.Dark;
        else if (Light.IsChecked == true) SelectedThemeVariant = ThemeVariant.Light;
    }
}