using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using CommunityToolkit.Mvvm.ComponentModel;
using TioUi.Common;
using TioUi.Common.Helpers;

namespace TioUi.Demo.Pages;

public partial class MacOsWindowPage : UserControl
{
    public MacOsWindowPage()
    {
        AvaloniaXamlLoader.Load(this);
        DataContext = new MacOsWindowPageViewModel(this);
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        if (Platform.DetectPlatform() != DesktopType.MacOs) return;
        var testWindow = new MacOsDemoWindow();
        testWindow.Show();
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
}