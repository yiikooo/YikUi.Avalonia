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
            Icon =
                "F1 M640,640z M0,0z M187.2,100.9C174.8,94.1 159.8,94.4 147.6,101.6 135.4,108.8 128,121.9 128,136L128,504C128,518.1 135.5,531.2 147.6,538.4 159.7,545.6 174.8,545.9 187.2,539.1L523.2,355.1C536,348.1 544,334.6 544,320 544,305.4 536,291.9 523.2,284.9L187.2,100.9z",
            Content = new ButtonPage(),
            Children =
            [
                new()
                {
                    Title = "Button",
                    Icon =
                        "F1 M640,640z M0,0z M187.2,100.9C174.8,94.1 159.8,94.4 147.6,101.6 135.4,108.8 128,121.9 128,136L128,504C128,518.1 135.5,531.2 147.6,538.4 159.7,545.6 174.8,545.9 187.2,539.1L523.2,355.1C536,348.1 544,334.6 544,320 544,305.4 536,291.9 523.2,284.9L187.2,100.9z",
                    Content = new ButtonPage(),
                    Children =
                    [
                        new()
                        {
                            Title = "Button",
                            Icon =
                                "F1 M640,640z M0,0z M187.2,100.9C174.8,94.1 159.8,94.4 147.6,101.6 135.4,108.8 128,121.9 128,136L128,504C128,518.1 135.5,531.2 147.6,538.4 159.7,545.6 174.8,545.9 187.2,539.1L523.2,355.1C536,348.1 544,334.6 544,320 544,305.4 536,291.9 523.2,284.9L187.2,100.9z",
                            Content = new ButtonPage(),
                        },
                    ]
                },
            ]
        },
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