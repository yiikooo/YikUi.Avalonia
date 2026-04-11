using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class SelectionListPage : UserControl
{
    public SelectionListPage()
    {
        InitializeComponent();
        DataContext = new SelectionListDemoViewModel();
    }
}

public partial class SelectionListDemoViewModel : ObservableObject
{
    [ObservableProperty] private string? _selectedItem;

    public SelectionListDemoViewModel()
    {
        Items = new ObservableCollection<string>()
        {
            "Apple", "Banana", "Pear", "Orange", "Grape"
        };
        SelectedItem = Items[0];
    }

    public ObservableCollection<string> Items { get; set; }

    public void Clear()
    {
        SelectedItem = null;
    }
}