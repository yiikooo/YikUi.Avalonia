using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Controls;

namespace TioUi.Demo.Pages;

public partial class ScrollToButtonPage : UserControl
{
    public ScrollToButtonPage()
    {
        InitializeComponent();
        Items = new ObservableCollection<string>(Enumerable.Range(0, 1000).Select(a => "Item " + a));
        DataContext = this;
    }

    public ObservableCollection<string> Items { get; set; }
}