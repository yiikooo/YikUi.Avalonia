using TioUi.Controls;

namespace TioUi.Demo.Models;

public interface IView
{
    public TioWindowNotificationManager NotificationManager { get; }
    public TioWindowToastManager ToastManager { get; }
}