using System;
using System.Timers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace YikUi.Test;

public partial class Model : ObservableObject
{
    [ObservableProperty] private DateTime _time;
    private Timer _timer;

    public Model()
    {
        Time = DateTime.Now;
        _timer = new Timer(1000);
        _timer.Elapsed += TimerOnElapsed;
        _timer.Start();
    }

    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        Time = DateTime.Now;
    }
}