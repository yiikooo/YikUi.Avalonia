using System.Collections.Generic;
using YikUi.Demo.Pages;

namespace YikUi.Demo.Navs;

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