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
            TioNotificationManager.TryGetNotificationManager(visualLayerManager, out var notificationManager)
                ? notificationManager
                : new TioNotificationManager(visualLayerManager) { MaxItems = 3 };
        _viewModel.ToastManager = TioToastManager.TryGetToastManager(visualLayerManager, out var toastManager)
            ? toastManager
            : new TioToastManager(visualLayerManager) { MaxItems = 3 };
        Debug.Assert(TioNotificationManager.TryGetNotificationManager(visualLayerManager, out _));
    }
}