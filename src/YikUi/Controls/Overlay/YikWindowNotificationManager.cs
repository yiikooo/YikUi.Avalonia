using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Threading;
using Avalonia.VisualTree;
using YikUi.Common.Classes;
using INotification = YikUi.Common.Interfaces.INotification;
using INotificationManager = YikUi.Common.Interfaces.INotificationManager;

namespace YikUi.Controls.Overlay;

/// <summary>
/// An <see cref="Common.Interfaces.INotificationManager"/> that displays notifications in a <see cref="Window"/>.
/// </summary>
[PseudoClasses(PC_TopLeft, PC_TopRight, PC_BottomLeft, PC_BottomRight, PC_TopCenter, PC_BottomCenter)]
public class YikWindowNotificationManager : WindowMessageManager, INotificationManager
{
    public const string PC_TopLeft = ":topleft";
    public const string PC_TopRight = ":topright";
    public const string PC_BottomLeft = ":bottomleft";
    public const string PC_BottomRight = ":bottomright";
    public const string PC_TopCenter = ":topcenter";
    public const string PC_BottomCenter = ":bottomcenter";

    /// <summary>
    /// Defines the <see cref="Position"/> property.
    /// </summary>
    public static readonly StyledProperty<NotificationPosition> PositionProperty =
        AvaloniaProperty.Register<YikWindowNotificationManager, NotificationPosition>(nameof(Position),
            NotificationPosition.TopRight);

    static YikWindowNotificationManager()
    {
        HorizontalAlignmentProperty.OverrideDefaultValue<YikWindowNotificationManager>(HorizontalAlignment.Stretch);
        VerticalAlignmentProperty.OverrideDefaultValue<YikWindowNotificationManager>(VerticalAlignment.Stretch);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="YikWindowNotificationManager"/> class.
    /// </summary>
    public YikWindowNotificationManager()
    {
        UpdatePseudoClasses(Position);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="YikWindowNotificationManager"/> class.
    /// </summary>
    /// <param name="host">The TopLevel that will host the control.</param>
    public YikWindowNotificationManager(TopLevel? host) : this()
    {
        if (host is not null)
        {
            InstallFromTopLevel(host);
        }
    }

    public YikWindowNotificationManager(VisualLayerManager? visualLayerManager) : base(visualLayerManager)
    {
        UpdatePseudoClasses(Position);
    }

    /// <summary>
    /// Defines which corner of the screen notifications can be displayed in.
    /// </summary>
    /// <seealso cref="NotificationPosition"/>
    public NotificationPosition Position
    {
        get => GetValue(PositionProperty);
        set => SetValue(PositionProperty, value);
    }

    public void Show(INotification content)
    {
        var options = new NotificationOptions
        {
            Type = content.Type,
            Expiration = content.Expiration,
            IsIconVisible = content.ShowIcon,
            IsCloseButtonVisible = content.ShowClose,
            OnClick = content.OnClick,
            OnClose = content.OnClose
        };
        Show(content, options);
    }

    public void Show(string msg, NotificationType type = NotificationType.Information)
    {
        var options = new NotificationOptions
        {
            Type = type,
        };

        Show(msg, options);
    }

    public static bool TryGetNotificationManager(Visual? visual, out YikWindowNotificationManager? manager)
    {
        manager = visual?.FindDescendantOfType<YikWindowNotificationManager>();
        return manager is not null;
    }

    public override void Show(object content)
    {
        if (content is INotification notification)
            Show(notification);
        else
            Show(content, new NotificationOptions());
    }

    /// <summary>
    ///     显示 Notification 通知
    /// </summary>
    /// <param name="content">内容</param>
    /// <param name="options">显示选项</param>
    public async void Show(object content, NotificationOptions options)
    {
        Dispatcher.UIThread.VerifyAccess();

        var notificationControl = new YikNotificationCard
        {
            Content = content,
            NotificationType = options.Type,
            ShowIcon = options.IsIconVisible,
            OperateButtons = options.OperateButtons,
            IsButtonsInline = options.IsButtonsInline,
            OnRemove = options.OnRemove,
            ShowCollapseButton = options.IsCollapseButtonVisible,
            ShowRemoveButton = options.IsCloseButtonVisible,
            [!YikNotificationCard.PositionProperty] = this[!PositionProperty]
        };

        if (options.IsColorful)
            options.Classes.Add("Colorful");

        if (options.Classes is not null)
            foreach (var @class in options.Classes)
                notificationControl.Classes.Add(@class);

        notificationControl.MessageClosed += (sender, _) =>
        {
            options.OnClose?.Invoke();
            _items?.Remove(sender);
        };

        notificationControl.PointerPressed += (_, _) =>
        {
            if (options.IsTouchClose)
                notificationControl.Close();
            options.OnClick?.Invoke();
        };

        Dispatcher.UIThread.Post(() =>
        {
            _items?.Add(notificationControl);

            if (_items?.OfType<YikNotificationCard>().Count(i => !i.IsClosing) > MaxItems)
                _items.OfType<YikNotificationCard>().First(i => !i.IsClosing).Close();
        });

        if (options.Expiration == TimeSpan.Zero) return;

        await Task.Delay(options.Expiration);

        notificationControl.CloseWithoutRemovingFromList();
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == PositionProperty)
        {
            UpdatePseudoClasses(change.GetNewValue<NotificationPosition>());
        }
    }

    private void UpdatePseudoClasses(NotificationPosition position)
    {
        PseudoClasses.Set(PC_TopLeft, position == NotificationPosition.TopLeft);
        PseudoClasses.Set(PC_TopRight, position == NotificationPosition.TopRight);
        PseudoClasses.Set(PC_BottomLeft, position == NotificationPosition.BottomLeft);
        PseudoClasses.Set(PC_BottomRight, position == NotificationPosition.BottomRight);
        PseudoClasses.Set(PC_TopCenter, position == NotificationPosition.TopCenter);
        PseudoClasses.Set(PC_BottomCenter, position == NotificationPosition.BottomCenter);
    }
}