using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Controls.Primitives;
using Avalonia.Threading;
using Avalonia.VisualTree;
using YikUi.Common.Classes;
using YikUi.Common.Interfaces;

namespace YikUi.Controls.Overlay;

public class YikWindowToastManager : WindowMessageManager, IToastManager
{
    public YikWindowToastManager()
    {
    }

    public YikWindowToastManager(TopLevel? host) : this()
    {
        if (host is not null) InstallFromTopLevel(host);
    }

    public YikWindowToastManager(VisualLayerManager? visualLayerManager) : base(visualLayerManager)
    {
    }

    public void Show(IToast content)
    {
        var options = new NotificationOptions
        {
            Content = content.Content,
            Type = content.Type,
            Expiration = content.Expiration,
            IsIconVisible = content.ShowIcon,
            IsCloseButtonVisible = content.ShowClose,
            OnClick = content.OnClick,
            OnClose = content.OnClose
        };
        Show(options);
    }

    public void Show(string msg, NotificationType type = NotificationType.Information)
    {
        var toastOptions = new NotificationOptions
        {
            Content = msg,
            Type = type,
        };

        Show(toastOptions);
    }

    public static bool TryGetToastManager(Visual? visual, out YikWindowToastManager? manager)
    {
        manager = visual?.FindDescendantOfType<YikWindowToastManager>();
        return manager is not null;
    }

    public override void Show(object content)
    {
        if (content is IToast toast)
            Show(toast);
        else
            Show(new NotificationOptions()
            {
                Content = content
            });
    }

    public async void Show(NotificationOptions options)
    {
        Dispatcher.UIThread.VerifyAccess();

        var toastControl = new YikToastCard
        {
            Content = options.Content,
            NotificationType = options.Type,
            ShowIcon = options.IsIconVisible,
            OperateButtons = options.OperateButtons,
            IsButtonsInline = options.IsButtonsInline,
            OnRemove = options.OnRemove,
            ShowCollapseButton = options.IsCollapseButtonVisible,
            ShowRemoveButton = options.IsCloseButtonVisible
        };

        if (options.IsColorful)
            options.Classes.Add("Colorful");

        if (options.Classes is not null)
            foreach (var @class in options.Classes)
                toastControl.Classes.Add(@class);

        toastControl.MessageClosed += (sender, _) =>
        {
            options.OnClose?.Invoke();
            _items?.Remove(sender);
        };

        toastControl.PointerPressed += (_, _) =>
        {
            if (options.IsTouchClose)
                toastControl.Close();
            options.OnClick?.Invoke();
        };

        Dispatcher.UIThread.Post(() =>
        {
            _items?.Add(toastControl);

            if (_items?.OfType<YikToastCard>().Count(i => !i.IsClosing) > MaxItems)
                _items.OfType<YikToastCard>().First(i => !i.IsClosing).Close();
        });

        if (options.Expiration == TimeSpan.Zero) return;

        await Task.Delay(options.Expiration);

        toastControl.CloseWithoutRemovingFromList();
    }
}