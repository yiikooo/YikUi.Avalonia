using System;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class DatePickerTioPage : UserControl
{
    public DatePickerTioPage()
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