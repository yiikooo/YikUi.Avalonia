using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class BreadcrumbPage : UserControl
{
    public BreadcrumbPage()
    {
        InitializeComponent();
        DataContext = new BreadcrumbDemoViewModel();
    }
}

public class BreadcrumbDemoViewModel : ObservableObject
{
    public ObservableCollection<BreadcrumbDemoItem> Items1 { get; set; } =
    [
        new BreadcrumbDemoItem { Section = "Home", Icon = "Home" },
        new BreadcrumbDemoItem { Section = "Page 1", Icon = "Page" },
        new BreadcrumbDemoItem { Section = "Page 2", Icon = "Page" },
        new BreadcrumbDemoItem { Section = "Page 3", Icon = "Page" },
        new BreadcrumbDemoItem { Section = "Page 4", Icon = "Page", IsReadOnly = true }
    ];
}

public partial class BreadcrumbDemoItem : ObservableObject
{
    [ObservableProperty] private bool _isReadOnly;
    public string? Section { get; set; }
    public string? Icon { get; set; }
}