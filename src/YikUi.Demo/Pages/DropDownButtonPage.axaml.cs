using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace YikUi.Demo.Pages;

public partial class DropDownButtonPage : UserControl
{
    public DropDownButtonPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public void Tip()
    {
        var lifetime = Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        var w = lifetime.MainWindow as MainWindow;
        w.toast.Show("Action !");
    }
}