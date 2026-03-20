using System.Collections.Generic;
using YikUi.Demo.Pages;

namespace YikUi.Demo.Navs;

public class Inputs
{
    public static readonly List<Page> InputsList =
    [
        new()
        {
            Title = "ComboBox",
            Content = new ComboBoxPage(),
        },
        new()
        {
            Title = "ColorPicker",
            Content = new ColorPickerPage(),
        },
        new()
        {
            Title = "TextBox",
            Content = new TextBoxPage(),
        },
        new()
        {
            Title = "AutoCompleteBox",
            Content = new AutoCompleteBoxPage(),
        },
        new()
        {
            Title = "AutoCompleteBox(yik)",
            Content = new AutoCompleteBoxYikPage(),
        },
        new()
        {
            Title = "ClassesInput",
            Content = new ClassInputPage(),
        },
        new()
        {
            Title = "ButtonSpinnerPage",
            Content = new ButtonSpinnerPage(),
        },
        new()
        {
            Title = "NumericUpDown",
            Content = new NumericUpDownPage(),
        },
        new()
        {
            Title = "Slider",
            Content = new SliderPage(),
        },
        new()
        {
            Title = "ManagedFileChooser",
            Content = new ManagedFileChooserPage(),
        },
    ];
}