using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using TioUi.Demo.Models;

namespace TioUi.Demo;

public partial class DemoView : UserControl
{
    public DemoView()
    {
        InitializeComponent();
        DataContext = new DemoViewModel();
    }
}