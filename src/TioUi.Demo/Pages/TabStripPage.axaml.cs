using System.Collections.ObjectModel;
using System.Linq;
using Avalonia;
using Avalonia.Styling;

namespace TioUi.Demo.Pages;

public partial class TabStripPage : PageModelBase
{
    public TabStripPage()
    {
        InitializeComponent();
        SelectedTheme = TabStripThemes[2];
        DataContext = this;
    }

    public ObservableCollection<string> Items => new(Enumerable.Range(1, 10).Select(a => "Tab " + a));

    public TabStripTheme SelectedTheme
    {
        get;
        set => SetField(ref field, value);
    }

    public ObservableCollection<TabStripTheme> TabStripThemes { get; } =
    [
        new("Default", (ControlTheme)Application.Current!.Resources["DefaultTabStrip"]!),
        new("Line", (ControlTheme)Application.Current!.Resources["LineTabStrip"]!),
        new("Card", (ControlTheme)Application.Current!.Resources["CardTabStrip"]!),
        new("Button", (ControlTheme)Application.Current!.Resources["ButtonTabStrip"]!)
    ];
}

public class TabStripTheme(string name, ControlTheme theme)
{
    public string Name { get; set; } = name;
    public ControlTheme Theme { get; set; } = theme;

    public override string ToString()
    {
        return Name;
    }
}