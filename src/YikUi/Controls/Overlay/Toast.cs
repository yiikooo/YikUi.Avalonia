using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls.Notifications;
using Avalonia.Metadata;
using YikUi.Common.Interfaces;

namespace YikUi.Controls;

public class Toast : IToast, INotifyPropertyChanged
{
    public Toast(
        string? content,
        NotificationType type = NotificationType.Information,
        TimeSpan? expiration = null,
        bool showClose = true,
        Action? onClick = null,
        Action? onClose = null)
    {
        Content = content;
        Type = type;
        Expiration = expiration ?? TimeSpan.FromSeconds(3);
        ShowClose = showClose;
        OnClick = onClick;
        OnClose = onClose;
    }

    public Toast() : this(null)
    {
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    /// <inheritdoc />
    [Content]
    public string? Content
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            OnPropertyChanged();
        }
    }

    public NotificationType Type { get; set; }

    public bool ShowIcon { get; set; }

    public bool ShowClose { get; set; }

    public TimeSpan Expiration { get; set; }

    public Action? OnClick { get; set; }

    public Action? OnClose { get; set; }

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}