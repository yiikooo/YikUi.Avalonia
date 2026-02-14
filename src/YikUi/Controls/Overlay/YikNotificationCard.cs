using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Notifications;
using Avalonia.LogicalTree;

namespace YikUi.Controls;

/// <summary>
/// Control that represents and displays a notification.
/// </summary>
[PseudoClasses(
    YikWindowNotificationManager.PC_TopLeft,
    YikWindowNotificationManager.PC_TopRight,
    YikWindowNotificationManager.PC_BottomLeft,
    YikWindowNotificationManager.PC_BottomRight,
    YikWindowNotificationManager.PC_TopCenter,
    YikWindowNotificationManager.PC_BottomCenter
)]
public class YikNotificationCard : YikMessageCard
{
    public static readonly DirectProperty<YikNotificationCard, NotificationPosition> PositionProperty =
        AvaloniaProperty.RegisterDirect<YikNotificationCard, NotificationPosition>(nameof(Position),
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
        PseudoClasses.Set(YikWindowNotificationManager.PC_TopLeft, position == NotificationPosition.TopLeft);
        PseudoClasses.Set(YikWindowNotificationManager.PC_TopRight, position == NotificationPosition.TopRight);
        PseudoClasses.Set(YikWindowNotificationManager.PC_BottomLeft, position == NotificationPosition.BottomLeft);
        PseudoClasses.Set(YikWindowNotificationManager.PC_BottomRight, position == NotificationPosition.BottomRight);
        PseudoClasses.Set(YikWindowNotificationManager.PC_TopCenter, position == NotificationPosition.TopCenter);
        PseudoClasses.Set(YikWindowNotificationManager.PC_BottomCenter, position == NotificationPosition.BottomCenter);
    }
}