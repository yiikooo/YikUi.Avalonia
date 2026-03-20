using System.Collections.Generic;
using YikUi.Demo.Pages;

namespace YikUi.Demo.Navs;

public class Buttons
{
    public static readonly List<Page> ButtonsList =
    [
        new()
        {
            Title = "Button",
            Content = new ButtonPage(),
        },
        new()
        {
            Title = "IconButton",
            Content = new IconButtonPage(),
        },
        new()
        {
            Title = "ToggleSwitch",
            Content = new ToggleSwitchPage(),
        },
        new()
        {
            Title = "RadioButton",
            Content = new RadioButtonPage(),
        },
        new()
        {
            Title = "HyperlinkButton",
            Content = new HyperlinkButtonPage(),
        },
        new()
        {
            Title = "ToggleButton",
            Content = new ToggleButtonPage(),
        },
        new()
        {
            Title = "CheckBox",
            Content = new CheckBoxPage(),
        },
        new()
        {
            Title = "DropDownButton",
            Content = new DropDownButtonPage(),
        },
        new()
        {
            Title = "SplitButton",
            Content = new SplitButtonPage(),
        },
    ];
}