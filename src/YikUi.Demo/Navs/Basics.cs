using System.Collections.Generic;
using YikUi.Demo.Pages;

namespace YikUi.Demo.Navs;

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
            Content = new TextBoxPage(),
        },
        new()
        {
            Title = "SelectableTextBlock",
            Content = new SelectableTextBlockPage(),
        },
    ];
}