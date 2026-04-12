using System.Collections.Generic;
using TioUi.Demo.Models;
using TioUi.Demo.Pages;

namespace TioUi.Demo.Navs;

public class Basics
{
    public static readonly List<Page> BasicsList =
    [
        new()
        {
            Title = "Label",
            Content = new LabelPage(),
        },
        new()
        {
            Title = "TextBlock",
            Content = new TextBlockPage(),
        },
        new()
        {
            Title = "SelectableTextBlock",
            Content = new SelectableTextBlockPage(),
        },
    ];
}
