using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using YikUi.Demo.Pages;

namespace YikUi.Demo;

public sealed class MainWindowModel : INotifyPropertyChanged
{
    public MainWindowModel()
    {
        SelectedPage = Pages[0];
        Pages = Pages.OrderBy(x => x.Title).ToList();
    }

    public Page SelectedPage
    {
        get;
        set => SetField(ref field, value);
    }

    public static List<Page> Pages { get; set; } =
    [
        new()
        {
            Title = "Button",
            Content = new ButtonPage(),
        },
        new()
        {
            Title = "Text",
            Content = new TextPage(),
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