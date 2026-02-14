using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using YikUi.Demo.Pages;

namespace YikUi.Demo;

public sealed class MainWindowModel : INotifyPropertyChanged
{
    public Page SelectedPage { get; set; } = Pages[0];

    public static List<Page> Pages { get; } =
    [
        new()
        {
            Title = "Button",
            Content = new ButtonPage(),
        }
    ];


    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}

public class Page
{
    public string Title { get; set; }
    public string Icon { get; set; }
    public UserControl Content { get; set; }
    public List<Page> Children { get; set; }
}