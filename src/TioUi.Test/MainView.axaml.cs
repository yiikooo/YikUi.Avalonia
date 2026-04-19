using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using TioUi.Controls;

namespace TioUi.Test;

public partial class MainView : TioView
{
    TioWindowToastManager _toastManager;

    public MainView()
    {
        InitializeComponent();
    }

    private void Button_OnClick(object? sender, RoutedEventArgs e)
    {
        var topLevel = TopLevel.GetTopLevel(this);
        _toastManager = new TioWindowToastManager(topLevel);
        _toastManager.Show("hi");
    }
}