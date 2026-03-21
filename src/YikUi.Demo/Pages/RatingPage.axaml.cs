using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace YikUi.Demo.Pages;

public partial class RatingPage : UserControl
{
    public RatingPage()
    {
        InitializeComponent();
        DataContext = new RatingDemoViewModel();
    }
}

public partial class RatingDemoViewModel : ObservableObject
{
    [ObservableProperty] private bool _allowClear = true;
    [ObservableProperty] private bool _allowHalf = true;
    [ObservableProperty] private int _count = 5;
    [ObservableProperty] private double _defaultValue = 2.3;
    [ObservableProperty] private bool _isEnabled = true;
    [ObservableProperty] private double _value;
}