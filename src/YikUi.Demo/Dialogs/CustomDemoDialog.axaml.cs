using System.Diagnostics;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.VisualTree;
using YikUi.Controls;

namespace YikUi.Demo.Dialogs;

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
            YikWindowNotificationManager.TryGetNotificationManager(visualLayerManager, out var notificationManager)
                ? notificationManager
                : new YikWindowNotificationManager(visualLayerManager) { MaxItems = 3 };
        _viewModel.ToastManager = YikWindowToastManager.TryGetToastManager(visualLayerManager, out var toastManager)
            ? toastManager
            : new YikWindowToastManager(visualLayerManager) { MaxItems = 3 };
        Debug.Assert(YikWindowNotificationManager.TryGetNotificationManager(visualLayerManager, out _));
    }
}