using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;

namespace TioUi.Demo.Pages;

public partial class ImageViewerPage : UserControl
{
    IImage? oldImg;

    public ImageViewerPage()
    {
        InitializeComponent();
    }

    private void Button_Click(object? sender, RoutedEventArgs e)
    {
        if (viewer.Source != null)
        {
            oldImg = viewer.Source;
            viewer.Source = null;
        }
        else
        {
            viewer.Source = oldImg;
        }
    }
}