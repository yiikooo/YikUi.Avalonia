using System.Collections.Generic;
using TioUi.Demo.Models;
using TioUi.Demo.Pages;

namespace TioUi.Demo.Navs;

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
            Title = "AutoCompleteBox(tio)",
            Content = new AutoCompleteBoxTioPage(),
        },
        new()
        {
            Title = "ClassesInput",
            Content = new ClassInputPage(),
        },
        new()
        {
            Title = "EnumSelector",
            Content = new EnumSelectorPage(),
        },
        new()
        {
            Title = "Form",
            Content = new FormPage(),
        },
        new()
        {
            Title = "MultiComboBox",
            Content = new MultiComboBoxPage(),
        },
        new()
        {
            Title = "NumPad",
            Content = new NumPadPage(),
        },
        new()
        {
            Title = "Rating",
            Content = new RatingPage(),
        },
        new()
        {
            Title = "SelectionList",
            Content = new SelectionListPage(),
        },
        new()
        {
            Title = "MultiAutoCompleteBox",
            Content = new MultiAutoCompleteBoxPage(),
        },
        new()
        {
            Title = "RangeSlider",
            Content = new RangeSliderPage(),
        },
        new()
        {
            Title = "TreeComboBox",
            Content = new TreeComboBoxPage(),
        },
        new()
        {
            Title = "KeyGestureInput",
            Content = new KeyGestureInputPage(),
        },
        new()
        {
            Title = "IPv4Box",
            Content = new IPv4BoxPage(),
        },
        new()
        {
            Title = "PathPicker",
            Content = new PathPickerPage(),
        },
        new()
        {
            Title = "PinCode",
            Content = new PinCodePage(),
        },
        new()
        {
            Title = "ButtonSpinnerPage",
            Content = new ButtonSpinnerPage(),
        },
        new()
        {
            Title = "TagInput",
            Content = new TagInputPage(),
        },
        new()
        {
            Title = "NumericUpDown",
            Content = new NumericUpDownPage(),
        },
        new()
        {
            Title = "NumericUpDown(tio)",
            Content = new NumericUpDownTioPage(),
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