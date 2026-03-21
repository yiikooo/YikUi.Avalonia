using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Input;
using Avalonia.Styling;
using YikUi.Controls;

namespace YikUi.Test;

public partial class MainWindow : YikWindow
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new Model();
        KeyBindings.Add(new KeyBinding
        {
            Gesture = KeyGesture.Parse("Ctrl+Q"),
            Command = new ActionCommand(ToggleTheme)
        });
    }

    private void ToggleTheme()
    {
        Application.Current.RequestedThemeVariant =
            Application.Current.ActualThemeVariant == ThemeVariant.Dark
                ? ThemeVariant.Light
                : ThemeVariant.Dark;
    }
}

public class ActionCommand : ICommand
{
    private readonly Action _execute;

    public ActionCommand(Action execute)
    {
        _execute = execute;
    }

    public bool CanExecute(object? parameter) => true;
    public void Execute(object? parameter) => _execute();
#pragma warning disable CS0067
    public event EventHandler? CanExecuteChanged;
#pragma warning restore CS0067
}