using System.Collections.ObjectModel;
using Avalonia.Controls.Notifications;

namespace YikUi.Common.Classes;

public class NotificationOptions
{
    public NotificationOptions(Action? onClose = null)
    {
        OnClose = onClose;
    }

    public NotificationType Type { get; init; } = NotificationType.Information;
    public TimeSpan Expiration { get; init; } = TimeSpan.FromSeconds(3);
    public Action? OnClick { get; init; }
    public Action? OnClose { get; init; }
    public Action? OnRemove { get; init; }
    public ObservableCollection<OperateButtonEntry>? OperateButtons { get; init; }
    public bool IsButtonsInline { get; init; } = true;
    public bool IsTouchClose { get; init; } = false;
    public bool IsIconVisible { get; init; } = true;
    public bool IsCloseButtonVisible { get; init; } = true;
    public bool IsCollapseButtonVisible { get; init; } = false;
    public bool IsColorful { get; init; } = true;
    public Avalonia.Controls.Classes Classes { get; } = ["Light"];
}