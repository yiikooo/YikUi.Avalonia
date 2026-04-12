using System.Collections.Generic;
using TioUi.Demo.Models;
using TioUi.Demo.Pages;

namespace TioUi.Demo.Navs;

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
            Title = "AspectRatioLayout",
            Content = new AspectRatioLayoutPage(),
        },
        new()
        {
            Title = "Divider",
            Content = new DividerPage(),
        },
        new()
        {
            Title = "ElasticWrapPanel",
            Content = new ElasticWrapPanelPage(),
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