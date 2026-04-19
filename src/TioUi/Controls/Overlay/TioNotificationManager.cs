using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using Avalonia.Threading;
using Avalonia.VisualTree;
using TioUi.Common.Classes;
using INotification = TioUi.Common.Interfaces.INotification;
using INotificationManager = TioUi.Common.Interfaces.INotificationManager;

namespace TioUi.Controls;

[PseudoClasses(PC_TopLeft, PC_TopRight, PC_BottomLeft, PC_BottomRight, PC_TopCenter, PC_BottomCenter)]
public class TioNotificationManager : WindowMessageManager, INotificationManager
{
    public const string PC_TopLeft = ":topleft";
    public const string PC_TopRight = ":topright";
    public const string PC_BottomLeft = ":bottomleft";
    public const string PC_BottomRight = ":bottomright";
    public const string PC_TopCenter = ":topcenter";
    public const string PC_BottomCenter = ":bottomcenter";

    public static readonly StyledProperty<NotificationPosition> PositionProperty =
        AvaloniaProperty.Register<TioNotificationManager, NotificationPosition>(nameof(Position),
            NotificationPosition.TopRight);

    static TioNotificationManager()
    {
        HorizontalAlignmentProperty.OverrideDefaultValue<TioNotificationManager>(HorizontalAlignment.Stretch);
        VerticalAlignmentProperty.OverrideDefaultValue<TioNotificationManager>(VerticalAlignment.Stretch);
    }

    public TioNotificationManager()
    {
        UpdatePseudoClasses(Position);
    }

    public TioNotificationManager(TopLevel? host) : this()
    {
        if (host is not null)
        {
            InstallFromTopLevel(host);
        }
    }

    public TioNotificationManager(VisualLayerManager? visualLayerManager) : base(visualLayerManager)
    {
        UpdatePseudoClasses(Position);
    }

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
            OnClose = content.OnClose,
            Title = content.Title,
            Content = content.Content
        };
        Show(options);
    }

    public static bool TryGetNotificationManager(Visual? visual, out TioNotificationManager? manager)
    {
        manager = visual?.FindDescendantOfType<TioNotificationManager>();
        return manager is not null;
    }

    public override void Show(object content)
    {
        if (content is INotification notification)
        {
            Show(notification);
        }
        else
        {
            Show(new NotificationOptions()
            {
                Content = content
            });
        }
    }

    public void Show(string title, string msg, NotificationOptions? options = null)
    {
        options ??= new NotificationOptions();
        options.Title = title;
        options.Content = msg;
        Show(options);
    }

    public void Show(string msg, NotificationOptions? options = null)
    {
        options ??= new NotificationOptions();
        options.Content = msg;
        Show(options);
    }

    public async void Show(NotificationOptions options)
    {
        Dispatcher.UIThread.VerifyAccess();

        var notificationControl = new TioNotificationCard
        {
            Content = options is { Title: not null, Content: string msg }
                ? new Notification
                {
                    Content = msg,
                    Title = options.Title
                }
                : options.Content ?? options.Title,
            NotificationType = options.Type,
            ShowIcon = options.IsIconVisible,
            OperateButtons = options.OperateButtons,
            IsButtonsInline = options.IsButtonsInline,
            OnRemove = options.OnRemove,
            ShowCollapseButton = options.IsCollapseButtonVisible,
            ShowRemoveButton = options.IsCloseButtonVisible,
            [!TioNotificationCard.PositionProperty] = this[!PositionProperty]
        };

        if (options.IsColorful)
            options.Classes.Add("Colorful");

        if (options.Classes is not null)
        {
            foreach (var @class in options.Classes)
            {
                notificationControl.Classes.Add(@class);
            }
        }

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

            if (_items?.OfType<NotificationCard>().Count(i => !i.IsClosing) > MaxItems)
            {
                _items.OfType<NotificationCard>().First(i => !i.IsClosing).Close();
            }
        });

        if (options.Expiration == TimeSpan.Zero)
        {
            return;
        }

        await Task.Delay(options.Expiration);

        notificationControl.Close();
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