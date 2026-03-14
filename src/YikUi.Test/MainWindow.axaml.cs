using YikUi.Controls;

namespace YikUi.Test;

public partial class MainWindow : YikWindow
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