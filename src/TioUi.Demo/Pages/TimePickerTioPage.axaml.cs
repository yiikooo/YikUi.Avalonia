using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class TimePickerTioPage : UserControl
{
    public TimePickerTioPage()
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