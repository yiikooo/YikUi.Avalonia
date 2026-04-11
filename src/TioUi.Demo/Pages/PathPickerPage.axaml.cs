using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace TioUi.Demo.Pages;

public partial class PathPickerPage : UserControl
{
    public PathPickerPage()
    {
        InitializeComponent();
        DataContext = new PathPickerDemoViewModel();
    }
}

public partial class PathPickerDemoViewModel : ObservableObject
{
    [ObservableProperty] private int _commandTriggerCount = 0;
    [ObservableProperty] private string? _path;
    [ObservableProperty] private IReadOnlyList<string>? _paths;

    [RelayCommand]
    private void Selected(IReadOnlyList<string> paths)
    {
        CommandTriggerCount++;
    }
}