using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using TioUi.Demo.Navs;
using TioUi.Demo.Pages;

namespace TioUi.Demo.Models;

public class DemoViewModel : ModelBase
{
    public DemoViewModel()
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
}

public class Page
{
    public string Title { get; set; }
    public string Icon { get; set; }
    public UserControl Content { get; set; }
    public List<Page>? Children { get; set; }
}