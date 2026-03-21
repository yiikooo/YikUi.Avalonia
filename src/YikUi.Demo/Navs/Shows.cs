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
            Title = "Avatar",
            Content = new AvatarPage(),
        },
        new()
        {
            Title = "QrCode",
            Content = new QrCodePage(),
        },
        new()
        {
            Title = "ImageViewer",
            Content = new ImageViewerPage(),
        },
        new()
        {
            Title = "Timeline",
            Content = new TimelinePage(),
        },
        new()
        {
            Title = "TwoTonePathIcon",
            Content = new TwoTonePathIconPage(),
        },
        new()
        {
            Title = "NumberDisplayer",
            Content = new NumberDisplayerPage(),
        },
        new()
        {
            Title = "Marquee",
            Content = new MarqueePage(),
        },
        new()
        {
            Title = "Banner",
            Content = new BannerPage(),
        },
        new()
        {
            Title = "Badge",
            Content = new BadgePage(),
        },
        new()
        {
            Title = "Descriptions",
            Content = new DescriptionsPage(),
        },
        new()
        {
            Title = "DataGrid",
            Content = new DataGridPage(),
        },
        // new()
        // {
        //     Title = "TreeDataGrid",
        //     Content = new TreeDataGridPage(),
        // },
        new()
        {
            Title = "ToolTip",
            Content = new ToolTipPage(),
        },
        new()
        {
            Title = "DisableContainer",
            Content = new DisableContainerPage(),
        },
        new()
        {
            Title = "DualBadge",
            Content = new DualBadgePage(),
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