using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.Input;

namespace YikUi.Demo.Pages;

public partial class ButtonGroupPage : UserControl
{
    public ButtonGroupPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public ObservableCollection<ButtonItem> Items { get; set; } = new()
    {
        new ButtonItem() { Name = "Apple" },
        new ButtonItem() { Name = "Banana" },
        new ButtonItem() { Name = "Pear" },
        new ButtonItem() { Name = "Orange" },
        new ButtonItem() { Name = "Grape" },
    };
}

public class ButtonItem
{
    public ButtonItem()
    {
        InvokeCommand = new AsyncRelayCommand(Invoke);
    }

    public string? Name { get; set; }
    public ICommand InvokeCommand { get; set; }

    private async Task Invoke()
    {
    }
}