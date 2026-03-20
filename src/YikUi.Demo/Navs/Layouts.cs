using System.Collections.Generic;
using YikUi.Demo.Pages;

namespace YikUi.Demo.Navs;

public static class Layouts
{
    public static readonly List<Page> LayoutsList =
    [
        new()
        {
            Title = "GroupBorder",
            Content = new GroupBorderPage(),
        },
        new()
        {
            Title = "GridSplitter",
            Content = new GridSplitterPage(),
        },
        new()
        {
            Title = "ScrollViewer",
            Content = new ScrollViewerPage(),
        },
        new()
        {
            Title = "ThemeVariantScope",
            Content = new ThemeVariantScopePage(),
        },
        new()
        {
            Title = "HeaderedContent",
            Content = new HeaderedContentPage(),
        },
        new()
        {
            Title = "SplitView",
            Content = new SplitViewPage(),
        },
    ];
}