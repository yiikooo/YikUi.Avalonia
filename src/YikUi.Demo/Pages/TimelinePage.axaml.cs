using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Controls.Templates;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using YikUi.Controls;

namespace YikUi.Demo.Pages;

public partial class TimelinePage : UserControl
{
    public TimelinePage()
    {
        InitializeComponent();
        DataContext = new TimelineDemoViewModel();
    }
}

public class TimelineIconTemplateSelector : ResourceDictionary, IDataTemplate
{
    public Control? Build(object? param)
    {
        if (param is TimelineItemType t)
        {
            string s = t.ToString();
            if (ContainsKey(s))
            {
                object? o = this[s];
                if (o is SolidColorBrush c)
                {
                    var ellipse = new Ellipse() { Width = 12, Height = 12, Fill = c };
                    return ellipse;
                }
            }
        }

        return null;
    }

    public bool Match(object? data)
    {
        return data is TimelineItemType;
    }
}

public class TimelineDemoViewModel : ObservableObject
{
    public TimelineItemViewModel[] Items { get; } =
    {
        new()
        {
            Time = DateTime.Now,
            Description = "Item 1",
            Header = "审核中",
            ItemType = TimelineItemType.Success,
        },
        new()
        {
            Time = DateTime.Now,
            Description = "Item 2",
            Header = "发布成功",
            ItemType = TimelineItemType.Ongoing,
        },
        new()
        {
            Time = DateTime.Now,
            Description = "Item 3",
            Header = "审核失败",
            ItemType = TimelineItemType.Error,
        }
    };
}

public class TimelineItemViewModel : ObservableObject
{
    public DateTime Time { get; set; }
    public string? TimeFormat { get; set; }
    public string? Description { get; set; }
    public string? Header { get; set; }
    public TimelineItemType ItemType { get; set; }
}