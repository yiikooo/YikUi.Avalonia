using System.Diagnostics;
using Avalonia.Controls.Chrome;
using Avalonia.Media;
using YikUi.Controls;

namespace YikUi.Demo;

public partial class MainWindow : YikWindow
{
    public MainWindow()
    {
        InitializeComponent();
    }

    public override bool OnClose()
    {
        Debug.WriteLine(366934983609630909);
        return true;
    }
}