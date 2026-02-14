using Avalonia.Controls.Notifications;
using YikUi.Controls;

namespace YikUi.Demo;

public partial class MainWindow : YikWindow
{
    private readonly YikWindowNotificationManager notification;
    private readonly YikWindowToastManager toast;

    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainWindowModel();
        toast = new YikWindowToastManager(GetTopLevel(this));
        notification = new YikWindowNotificationManager(GetTopLevel(this))
        {
            Position = NotificationPosition.TopRight
        };
    }
}