using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Notifications;
using Avalonia.LogicalTree;

namespace TioUi.Controls;

/// <summary>
/// Control that represents and displays a notification.
/// </summary>
[PseudoClasses(
    TioWindowNotificationManager.PC_TopLeft,
    TioWindowNotificationManager.PC_TopRight,
    TioWindowNotificationManager.PC_BottomLeft,
    TioWindowNotificationManager.PC_BottomRight,
    TioWindowNotificationManager.PC_TopCenter,
    TioWindowNotificationManager.PC_BottomCenter
)]
public class TioNotificationCard : TioMessageCard
{
    public static readonly DirectProperty<TioNotificationCard, NotificationPosition> PositionProperty =
        AvaloniaProperty.RegisterDirect<TioNotificationCard, NotificationPosition>(nameof(Position),
            o => o.Position, (o, v) => o.Position = v);

    public NotificationPosition Position
    {
        get;
        set => SetAndRaise(PositionProperty, ref field, value);
    }

    protected override void OnAttachedToLogicalTree(LogicalTreeAttachmentEventArgs e)
    {
        base.OnAttachedToLogicalTree(e);
        UpdatePseudoClasses(Position);
    }

    private void UpdatePseudoClasses(NotificationPosition position)
    {
        PseudoClasses.Set(TioWindowNotificationManager.PC_TopLeft, position == NotificationPosition.TopLeft);
        PseudoClasses.Set(TioWindowNotificationManager.PC_TopRight, position == NotificationPosition.TopRight);
        PseudoClasses.Set(TioWindowNotificationManager.PC_BottomLeft, position == NotificationPosition.BottomLeft);
        PseudoClasses.Set(TioWindowNotificationManager.PC_BottomRight, position == NotificationPosition.BottomRight);
        PseudoClasses.Set(TioWindowNotificationManager.PC_TopCenter, position == NotificationPosition.TopCenter);
        PseudoClasses.Set(TioWindowNotificationManager.PC_BottomCenter, position == NotificationPosition.BottomCenter);
    }
}