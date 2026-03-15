using System;
using System.Windows.Input;
using Avalonia;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Styling;
using YikUi.Controls;

namespace YikUi.Test;

public partial class MainWindow : YikWindow
{
    IImage? oldImg;

    public MainWindow()
    {
#if DEBUG
        InitializeComponent(attachDevTools: false);
#else
        InitializeComponent();
#endif
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

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        if (viewer.Source != null)
        {
            oldImg = viewer.Source;
            viewer.Source = null;
        }
        else
        {
            viewer.Source = oldImg;
        }
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
    public event EventHandler? CanExecuteChanged;
}