using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;

namespace TioUi.Demo.Pages;

public partial class RefreshContainerPage : UserControl
{
    public RefreshContainerPage()
    {
        InitializeComponent();
        Items = new ObservableCollection<string>(Enumerable.Range(1, 200).Select(i => $"Item {i}"));
        DataContext = this;
    }

    public ObservableCollection<string> Items { get; }

    private async void RefreshContainerPage_RefreshRequested(object? sender, RefreshRequestedEventArgs e)
    {
        var deferral = e.GetDeferral();
        await AddToTop();
        deferral.Complete();
    }

    public async Task AddToTop()
    {
        await Task.Delay(1000);
        Items.Insert(0, $"Item {200 - Items.Count}");
    }
}