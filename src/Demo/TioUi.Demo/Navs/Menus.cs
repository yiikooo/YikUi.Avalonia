using System.Collections.Generic;
using TioUi.Demo.Models;
using TioUi.Demo.Pages;

namespace TioUi.Demo.Navs;

public class Menus
{
    public static readonly List<Page> MenusList =
    [
        new()
        {
            Title = "Menu",
            Content = new MenuPage(),
        },
        new()
        {
            Title = "NavMenu",
            Content = new NavMenuPage(),
        },
        new()
        {
            Title = "Breadcrumb",
            Content = new BreadcrumbPage(),
        },
        new()
        {
            Title = "Pagination",
            Content = new PaginationPage(),
        },
        new()
        {
            Title = "ToolBar",
            Content = new ToolBarPage(),
        },
        new()
        {
            Title = "Anchor",
            Content = new AnchorPage(),
        },
        new()
        {
            Title = "TabStrip",
            Content = new TabStripPage(),
        },
        new()
        {
            Title = "TabControl",
            Content = new TabControlPage(),
        },
    ];
}