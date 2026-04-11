using System;
using System.Timers;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class ClockPage : UserControl
{
    public ClockPage()
    {
        InitializeComponent();
        DataContext = new ClockDemoViewModel();
    }
}

public partial class ClockDemoViewModel : ObservableObject, IDisposable
{
    [ObservableProperty] private DateTime _time;
    private Timer _timer;

    public ClockDemoViewModel()
    {
        Time = DateTime.Now;
        _timer = new Timer(1000);
        _timer.Elapsed += TimerOnElapsed;
        _timer.Start();
    }

    public void Dispose()
    {
        _timer.Stop();
        _timer.Elapsed -= TimerOnElapsed;
        _timer.Dispose();
    }

    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        Time = DateTime.Now;
    }
}