using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.VisualTree;
using TioUi.Controls;

namespace TioUi.Demo.Dialogs;

public partial class CustomDemoDialog : UserControl
{
    private CustomDemoDialogViewModel? _viewModel;

    public CustomDemoDialog()
    {
        InitializeComponent();
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _viewModel = this.DataContext as CustomDemoDialogViewModel;
        var visualLayerManager = this.FindAncestorOfType<VisualLayerManager>();
        if (_viewModel == null) return;
        _viewModel.NotificationManager =
            TioWindowNotificationManager.TryGetNotificationManager(visualLayerManager, out var notificationManager)
                ? notificationManager
                : new TioWindowNotificationManager(visualLayerManager) { MaxItems = 3 };
        _viewModel.ToastManager = TioWindowToastManager.TryGetToastManager(visualLayerManager, out var toastManager)
            ? toastManager
            : new TioWindowToastManager(visualLayerManager) { MaxItems = 3 };
        Debug.Assert(TioWindowNotificationManager.TryGetNotificationManager(visualLayerManager, out _));
    }
}