using System;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TioUi.Demo.Pages;

public partial class NumberDisplayerPage : UserControl
{
    public NumberDisplayerPage()
    {
        InitializeComponent();
        DataContext = new NumberDisplayerDemoViewModel();
    }
}

public partial class NumberDisplayerDemoViewModel : ObservableObject
{
    [ObservableProperty] private DateTime _dateValue;
    [ObservableProperty] private double _doubleValue;
    [ObservableProperty] private long _longValue;
    [ObservableProperty] private int _value;

    public NumberDisplayerDemoViewModel()
    {
        IncreaseCommand = new RelayCommand(OnChange);
        Value = 0;
        LongValue = 0L;
        DoubleValue = 0d;
        DateValue = DateTime.Now;
    }

    public ICommand IncreaseCommand { get; }

    private void OnChange()
    {
        Random r = new Random();
        Value = r.Next(int.MaxValue);
        LongValue = ((long)r.Next(int.MaxValue)) * 1000 + r.Next(1000);
        DoubleValue = r.NextDouble() * 100000;
        DateValue = DateTime.Today.AddDays(r.Next(1000));
    }
}