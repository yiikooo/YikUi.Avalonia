using System.Collections.Generic;
using TioUi.Demo.Models;
using TioUi.Demo.Pages;

namespace TioUi.Demo.Navs;

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