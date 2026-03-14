using Avalonia.Controls;

namespace YikUi.Test;

public partial class MainWindow : Window
{
    public MainWindow()
    {
#if DEBUG
        InitializeComponent(attachDevTools: false);
#else
        InitializeComponent();
#endif
        DataContext = new Model();
    }
}