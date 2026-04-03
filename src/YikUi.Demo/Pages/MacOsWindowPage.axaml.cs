using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YikUi.Common.Helpers;
using YikUi.Controls;

namespace YikUi.Demo.Pages;

public partial class MacOsWindowPage : UserControl
{
    public MacOsWindowPage()
    {
        AvaloniaXamlLoader.Load(this);
        DataContext = new MacOsWindowPageViewModel(this);
    }
}

public partial class MacOsWindowPageViewModel : ObservableObject
{
    private readonly MacOsWindowPage _page;

    public MacOsWindowPageViewModel(MacOsWindowPage page)
    {
        _page = page;
    }

    public string SystemInfo => $"当前系统: {Platform.DetectPlatform()}";

    [RelayCommand]
    private void OpenTestWindow()
    {
        var xCoordinate = _page.FindControl<NumericDoubleUpDown>("XCoordinate");
        var yCoordinate = _page.FindControl<NumericDoubleUpDown>("YCoordinate");
        var buttonSpacing = _page.FindControl<NumericDoubleUpDown>("ButtonSpacing");

        var testWindow = new MacOsTestWindow
        {
            XCoordinate = xCoordinate?.Value ?? 20,
            YCoordinate = yCoordinate?.Value ?? -4,
            ButtonSpacing = buttonSpacing?.Value ?? 20
        };

        testWindow.Show();
    }

    [RelayCommand]
    private void ResetDefaults()
    {
        var xCoordinate = _page.FindControl<NumericDoubleUpDown>("XCoordinate");
        var yCoordinate = _page.FindControl<NumericDoubleUpDown>("YCoordinate");
        var buttonSpacing = _page.FindControl<NumericDoubleUpDown>("ButtonSpacing");

        if (xCoordinate != null) xCoordinate.Value = 20;
        if (yCoordinate != null) yCoordinate.Value = -4;
        if (buttonSpacing != null) buttonSpacing.Value = 20;
    }

    [RelayCommand]
    private void ApplyPreset(string preset)
    {
        var xCoordinate = _page.FindControl<NumericDoubleUpDown>("XCoordinate");
        var yCoordinate = _page.FindControl<NumericDoubleUpDown>("YCoordinate");
        var buttonSpacing = _page.FindControl<NumericDoubleUpDown>("ButtonSpacing");

        switch (preset)
        {
            case "standard":
                if (xCoordinate != null) xCoordinate.Value = 20;
                if (yCoordinate != null) yCoordinate.Value = -4;
                if (buttonSpacing != null) buttonSpacing.Value = 20;
                break;
            case "compact":
                if (xCoordinate != null) xCoordinate.Value = 15;
                if (yCoordinate != null) yCoordinate.Value = -4;
                if (buttonSpacing != null) buttonSpacing.Value = 15;
                break;
            case "loose":
                if (xCoordinate != null) xCoordinate.Value = 25;
                if (yCoordinate != null) yCoordinate.Value = -4;
                if (buttonSpacing != null) buttonSpacing.Value = 25;
                break;
            case "down":
                if (xCoordinate != null) xCoordinate.Value = 20;
                if (yCoordinate != null) yCoordinate.Value = 0;
                if (buttonSpacing != null) buttonSpacing.Value = 20;
                break;
        }
    }
}