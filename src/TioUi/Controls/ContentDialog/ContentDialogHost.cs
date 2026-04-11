using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Layout;

namespace TioUi.Controls;

/// <summary>
/// ContentDialog 的宿主控件。
/// 由 ContentDialog 直接管理尺寸并添加到 OverlayDialogHost（Canvas）上，
/// 负责提供遮罩背景并将对话框居中显示。
/// </summary>
internal class ContentDialogHost : ContentControl
{
    static ContentDialogHost()
    {
        HorizontalContentAlignmentProperty.OverrideDefaultValue<ContentDialogHost>(HorizontalAlignment.Center);
        VerticalContentAlignmentProperty.OverrideDefaultValue<ContentDialogHost>(VerticalAlignment.Center);
    }

    // 拦截所有指针事件，防止点击穿透到遮罩下方的控件
    protected override void OnPointerEntered(PointerEventArgs e) => e.Handled = true;
    protected override void OnPointerExited(PointerEventArgs e) => e.Handled = true;
    protected override void OnPointerPressed(PointerPressedEventArgs e) => e.Handled = true;
    protected override void OnPointerReleased(PointerReleasedEventArgs e) => e.Handled = true;
    protected override void OnPointerMoved(PointerEventArgs e) => e.Handled = true;
    protected override void OnPointerWheelChanged(PointerWheelEventArgs e) => e.Handled = true;
    protected override void OnPointerCaptureLost(PointerCaptureLostEventArgs e) => e.Handled = true;
}