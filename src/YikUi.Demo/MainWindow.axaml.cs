using Avalonia.Controls;
using Avalonia.Platform;

namespace YikUi.Demo;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        ExtendClientAreaChromeHints = ExtendClientAreaChromeHints.NoChrome;
        ExtendClientAreaToDecorationsHint = true;
    }
}