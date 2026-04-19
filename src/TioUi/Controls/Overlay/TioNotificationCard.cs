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
    TioNotificationManager.PC_TopLeft,
    TioNotificationManager.PC_TopRight,
    TioNotificationManager.PC_BottomLeft,
    TioNotificationManager.PC_BottomRight,
    TioNotificationManager.PC_TopCenter,
    TioNotificationManager.PC_BottomCenter
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
        PseudoClasses.Set(TioNotificationManager.PC_TopLeft, position == NotificationPosition.TopLeft);
        PseudoClasses.Set(TioNotificationManager.PC_TopRight, position == NotificationPosition.TopRight);
        PseudoClasses.Set(TioNotificationManager.PC_BottomLeft, position == NotificationPosition.BottomLeft);
        PseudoClasses.Set(TioNotificationManager.PC_BottomRight, position == NotificationPosition.BottomRight);
        PseudoClasses.Set(TioNotificationManager.PC_TopCenter, position == NotificationPosition.TopCenter);
        PseudoClasses.Set(TioNotificationManager.PC_BottomCenter, position == NotificationPosition.BottomCenter);
    }
}