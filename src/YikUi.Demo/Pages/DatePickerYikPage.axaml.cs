using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace YikUi.Demo.Pages;

public partial class DatePickerYikPage : UserControl
{
    public DatePickerYikPage()
    {
        InitializeComponent();
        DataContext = new DatePickerDemoViewModel();
    }
}

public partial class DatePickerDemoViewModel : ObservableObject
{
    [ObservableProperty] private DateTime? _selectedDate;

    public DatePickerDemoViewModel()
    {
        SelectedDate = DateTime.Today;
    }
}