using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace YikUi.Demo.Pages;

public partial class BannerPage : UserControl
{
    public BannerPage()
    {
        InitializeComponent();
        DataContext = new BannerDemoViewModel();
    }
}

public partial class BannerDemoViewModel : ObservableObject
{
    [ObservableProperty] private bool _bordered;
    [ObservableProperty] private string? _content = "This is the Demo of Ursa Banner.";
    private string? _oldContent = string.Empty;
    private string? _oldTitle = string.Empty;
    [ObservableProperty] private bool _setContentNull = true;

    [ObservableProperty] private bool _setTitleNull = true;
    [ObservableProperty] private string? _title = "Welcome to Ursa";

    partial void OnSetTitleNullChanged(bool value)
    {
        if (value)
        {
            Title = _oldTitle;
        }
        else
        {
            _oldTitle = Title;
            Title = null;
        }
    }

    partial void OnSetContentNullChanged(bool value)
    {
        if (value)
        {
            Content = _oldContent;
        }
        else
        {
            _oldContent = Content;
            Content = null;
        }
    }
}