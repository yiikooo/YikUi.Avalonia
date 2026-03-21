using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace YikUi.Demo.Pages;

public partial class TimePickerYikPage : UserControl
{
    public TimePickerYikPage()
    {
        InitializeComponent();
        DataContext = new TimePickerDemoViewModel();
    }
}

public partial class TimePickerDemoViewModel : ObservableObject
{
    [ObservableProperty] private TimeSpan? _time;

    public TimePickerDemoViewModel()
    {
        Time = new TimeSpan(12, 20, 0);
    }
}