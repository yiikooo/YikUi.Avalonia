using Avalonia.Controls;

namespace TioUi.Demo.Pages;

public partial class IconButtonPage : UserControl
{
    public IconButtonPage()
    {
        InitializeComponent();
        AddClasses(DangerIconButton);
        AddClasses(DangerIconDropDownButton);
        AddClasses(DangerIconSplitButton);
        AddClasses(DangerIconToggleButton);
    }

    private void AddClasses(WrapPanel panel)
    {
        foreach (var child in panel.Children)
        {
            child.Classes.Add("Danger");
        }
    }
}