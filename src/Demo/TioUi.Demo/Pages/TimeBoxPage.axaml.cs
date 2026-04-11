using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TioUi.Demo.Pages;

public partial class TimeBoxPage : UserControl
{
    public TimeBoxPage()
    {
        InitializeComponent();
        DataContext = new TimeBoxDemoViewModel();
    }
}

public partial class TimeBoxDemoViewModel : ObservableObject
{
    [ObservableProperty] private TimeSpan? _timeSpan;

    public TimeBoxDemoViewModel()
    {
        TimeSpan = new TimeSpan(0, 21, 11, 36, 54);
    }

    [RelayCommand]
    private void ChangeRandomTime()
    {
        TimeSpan = new TimeSpan(Random.Shared.NextInt64(0x00000000FFFFFFFF));
    }
}