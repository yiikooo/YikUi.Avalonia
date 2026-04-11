using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Styling;
using TioUi.Common.Language;
using TioUi.Demo.Navs;
using TioUi.Demo.Pages;

namespace TioUi.Demo;

public sealed class MainWindowModel : INotifyPropertyChanged
{
    public MainWindowModel()
    {
        SelectedPage = Pages[0];
        foreach (var page in Pages)
        {
            if (page.Children is { Count: > 0 })
            {
                Items += page.Children.Count;
                page.Children = page.Children.OrderBy(child => child.Title).ToList();
            }
            else
            {
                Items += 1;
            }
        }
    }

    public Page SelectedPage
    {
        get;
        set => SetField(ref field, value);
    }

    public int Items { get; set; }

    public static List<Page> Pages { get; set; } =
    [
        new()
        {
            Title = "Overview",
            Content = new OverviewPage()
        },
        new()
        {
            Title = "Setting",
            Content = new SettingPage()
        },
        new()
        {
            Title = "Basic",
            Children = Basics.BasicsList
        },
        new()
        {
            Title = "Button",
            Children = Buttons.ButtonsList
        },
        new()
        {
            Title = "Input",
            Children = Inputs.InputsList
        },
        new()
        {
            Title = "Menu",
            Children = Menus.MenusList
        },
        new()
        {
            Title = "Time",
            Children = Times.TimesList
        },
        new()
        {
            Title = "Show",
            Children = Shows.ShowsList
        },
        new()
        {
            Title = "Feedback",
            Children = Feedbacks.FeedbacksList
        },
        new()
        {
            Title = "Layout",
            Children = Layouts.LayoutsList
        },
        new()
        {
            Title = "Platform",
            Children = Platform.PlatformList
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

    public void ToggleTheme(string? theme = null)
    {
        if (Application.Current != null && theme != null)
        {
            Application.Current.RequestedThemeVariant =
                Application.Current.ActualThemeVariant == ThemeVariant.Dark
                    ? ThemeVariant.Light
                    : ThemeVariant.Dark;
        }

        if (theme == "a")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Default;
        else if (theme == "l")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Light;
        else if (theme == "d")
            Application.Current!.RequestedThemeVariant = ThemeVariant.Dark;
    }


    public void ToggleLang(string? l = null)
    {
        if (l == null) return;

        if (l == "c")
            LangManager.SetLanguage(Languages.zh_cn);
        else if (l == "e")
            LangManager.SetLanguage(Languages.en_us);
    }
}

public class Page
{
    public string Title { get; set; }
    public string Icon { get; set; }
    public UserControl Content { get; set; }
    public List<Page>? Children { get; set; }
}