using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;

namespace YikUi.Controls;

/// <summary>
/// ContentDialog 的宿主控件，负责填满 OverlayLayer 并提供遮罩背景
/// </summary>
internal class ContentDialogHost : ContentControl
{
    private IDisposable? _rootBoundsWatcher;

    static ContentDialogHost()
    {
        HorizontalAlignmentProperty.OverrideDefaultValue<ContentDialogHost>(HorizontalAlignment.Left);
        VerticalAlignmentProperty.OverrideDefaultValue<ContentDialogHost>(VerticalAlignment.Top);
        HorizontalContentAlignmentProperty.OverrideDefaultValue<ContentDialogHost>(HorizontalAlignment.Center);
        VerticalContentAlignmentProperty.OverrideDefaultValue<ContentDialogHost>(VerticalAlignment.Center);
    }

    protected override Size MeasureOverride(Size availableSize)
    {
        base.MeasureOverride(availableSize);

        if (VisualRoot is TopLevel tl)
            return tl.ClientSize;
        if (VisualRoot is Control c)
            return c.Bounds.Size;

        return default;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        if (e.RootVisual is Control root)
            _rootBoundsWatcher = root.GetObservable(BoundsProperty).Subscribe(_ => InvalidateMeasure());
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnDetachedFromVisualTree(e);
        _rootBoundsWatcher?.Dispose();
        _rootBoundsWatcher = null;
    }

    protected override void OnPointerEntered(PointerEventArgs e) => e.Handled = true;
    protected override void OnPointerExited(PointerEventArgs e) => e.Handled = true;
    protected override void OnPointerPressed(PointerPressedEventArgs e) => e.Handled = true;
    protected override void OnPointerReleased(PointerReleasedEventArgs e) => e.Handled = true;
    protected override void OnPointerMoved(PointerEventArgs e) => e.Handled = true;
    protected override void OnPointerWheelChanged(PointerWheelEventArgs e) => e.Handled = true;
    protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e) => e.Handled = true;
}