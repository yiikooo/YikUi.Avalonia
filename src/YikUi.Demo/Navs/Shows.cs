using System.Collections.Generic;
using YikUi.Demo.Pages;

namespace YikUi.Demo.Navs;

public static class Shows
{
    public static readonly List<Page> ShowsList =
    [
        new()
        {
            Title = "Flyout",
            Content = new FlyoutPage(),
        },
        new()
        {
            Title = "DataGrid",
            Content = new DataGridPage(),
        },
        new()
        {
            Title = "TreeDataGrid",
            Content = new TreeDataGridPage(),
        },
        new()
        {
            Title = "ToolTip",
            Content = new ToolTipPage(),
        },
        new()
        {
            Title = "ProgressBar",
            Content = new ProgressBarPage(),
        },
        new()
        {
            Title = "Expander",
            Content = new ExpanderPage(),
        },
        new()
        {
            Title = "ListBox",
            Content = new ListBoxPage(),
        },
        new()
        {
            Title = "TreeView",
            Content = new TreeViewPage(),
        },
        new()
        {
            Title = "Carousel",
            Content = new CarouselPage(),
        },
    ];
}