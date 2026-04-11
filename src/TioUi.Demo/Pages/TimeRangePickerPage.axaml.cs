using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class TimeRangePickerPage : UserControl
{
    public TimeRangePickerPage()
    {
        InitializeComponent();
        DataContext = new TimeRangePickerDemoViewModel();
    }
}

public partial class TimeRangePickerDemoViewModel : ObservableObject
{
    [ObservableProperty] private TimeSpan? _endTime;
    [ObservableProperty] private TimeSpan? _startTime;

    public TimeRangePickerDemoViewModel()
    {
        StartTime = new TimeSpan(8, 21, 0);
        EndTime = new TimeSpan(18, 22, 0);
    }
}