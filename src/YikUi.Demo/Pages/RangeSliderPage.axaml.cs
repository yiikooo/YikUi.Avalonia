using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Layout;
using CommunityToolkit.Mvvm.ComponentModel;

namespace YikUi.Demo.Pages;

public partial class RangeSliderPage : UserControl
{
    public RangeSliderPage()
    {
        InitializeComponent();
        DataContext = new RangeSliderDemoViewModel();
    }
}

public partial class RangeSliderDemoViewModel : ObservableObject
{
    [ObservableProperty] private Orientation _orientation;

    public ObservableCollection<Orientation> Orientations { get; set; } = new ObservableCollection<Orientation>()
    {
        Orientation.Horizontal,
        Orientation.Vertical
    };
}