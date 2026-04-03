using System.Collections.Generic;
using YikUi.Demo.Pages;

namespace YikUi.Demo.Navs;

public static class Platform
{
    public static readonly List<Page> PlatformList = new()
    {
        new()
        {
            Title = "MacOsWindowHandler",
            Content = new MacOsWindowPage(),
        },
    };
}