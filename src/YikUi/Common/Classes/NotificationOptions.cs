using System.Collections.ObjectModel;
using Avalonia.Controls.Notifications;

namespace YikUi.Common.Classes;

public class NotificationOptions
{
    public NotificationOptions(Action? onClose = null)
    {
        OnClose = onClose;
    }

    public NotificationType Type { get; set; } = NotificationType.Information;
    public TimeSpan Expiration { get; init; } = TimeSpan.FromSeconds(3);
    public Action? OnClick { get; init; }
    public Action? OnClose { get; set; }
    public Action? OnRemove { get; set; }
    public ObservableCollection<OperateButtonEntry>? OperateButtons { get; init; }
    public bool IsButtonsInline { get; init; } = true;
    public bool IsTouchClose { get; init; } = false;
    public bool IsIconVisible { get; init; } = true;
    public bool IsCloseButtonVisible { get; init; } = true;
    public bool IsCollapseButtonVisible { get; init; } = false;
    public Avalonia.Controls.Classes Classes { get; } = ["Light"];
}