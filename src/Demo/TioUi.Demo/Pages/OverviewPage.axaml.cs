using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Platform.Storage;
using TioUi.Demo.Models;

namespace TioUi.Demo.Pages;

public partial class OverviewPage : UserControl
{
    public OverviewPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public Setting Setting { get; } = Setting.Instance;

    private async void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var sp = TopLevel.GetTopLevel(this).StorageProvider;
        if (sp is null) return;
        var result = await sp.OpenFilePickerAsync(new FilePickerOpenOptions()
        {
            Title = "Open File",
            FileTypeFilter =
            [
                FilePickerFileTypes.All,
                FilePickerFileTypes.TextPlain
            ],
            AllowMultiple = true,
        });
    }
}