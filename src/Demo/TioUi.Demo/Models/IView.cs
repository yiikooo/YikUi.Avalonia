using TioUi.Controls;

namespace TioUi.Demo.Models;

public interface IView
{
    public TioNotificationManager NotificationManager { get; }
    public TioToastManager ToastManager { get; }
}