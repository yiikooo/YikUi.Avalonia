using Avalonia.Controls.Notifications;

namespace YikUi.Common.Classes;

/// <summary>
///     Toast 显示选项
/// </summary>
public class ToastOptions
{
    /// <summary>
    ///     通知类型
    /// </summary>
    public NotificationType Type { get; init; } = NotificationType.Information;

    /// <summary>
    ///     关联的通知条目
    /// </summary>
    public NotificationEntry? NotificationEntry { get; init; }

    /// <summary>
    ///     显示时长，null 使用默认值 10 秒
    /// </summary>
    public TimeSpan? Expiration { get; init; }

    /// <summary>
    ///     是否显示图标
    /// </summary>
    public bool ShowIcon { get; init; } = true;

    /// <summary>
    ///     是否显示关闭按钮
    /// </summary>
    public bool ShowClose { get; init; } = true;

    /// <summary>
    ///     点击是否关闭
    /// </summary>
    public bool TouchClose { get; init; }

    /// <summary>
    ///     点击回调
    /// </summary>
    public Action? OnClick { get; init; }

    /// <summary>
    ///     关闭回调
    /// </summary>
    public Action? OnClose { get; init; }

    /// <summary>
    ///     样式类
    /// </summary>
    public string[]? Classes { get; init; }

    /// <summary>
    ///     操作按钮
    /// </summary>
    public IList<OperateButtonEntry>? OperateButtons { get; init; }

    /// <summary>
    ///     按钮是否内联显示
    /// </summary>
    public bool IsButtonsInline { get; init; }
}