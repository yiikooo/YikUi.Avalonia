using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace YikUi.Demo.Pages;

public partial class DateRangePickerPage : UserControl
{
    public DateRangePickerPage()
    {
        InitializeComponent();
        DataContext = new DateRangePickerDemoViewModel();
    }
}

public partial class DateRangePickerDemoViewModel : ObservableObject
{
    [ObservableProperty] private DateTime? _endDate;
    [ObservableProperty] private DateTime? _startDate;

    public DateRangePickerDemoViewModel()
    {
        StartDate = DateTime.Today;
        EndDate = DateTime.Today.AddDays(7);
    }
}