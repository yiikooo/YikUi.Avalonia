using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Controls.Notifications;
using Avalonia.Interactivity;
using YikUi.Common.Classes;
using YikUi.Controls;
using YikUi.Controls.Overlay;

namespace YikUi.Demo;

public partial class MainWindow : YikWindow
{
    private readonly YikWindowNotificationManager notification;
    private readonly YikWindowToastManager toast;

    public MainWindow()
    {
        InitializeComponent();
        toast = new YikWindowToastManager(GetTopLevel(this));
        notification = new YikWindowNotificationManager(GetTopLevel(this))
        {
            Position = NotificationPosition.BottomRight
        };
    }

    private void Toast(object? sender, RoutedEventArgs e)
    {
        var t = ((Button)sender).Tag.ToString();
        var i = new TimeSpan(1, 0, 3);
        switch (t)
        {
            case "Info":
                toast.Show("Info");
                break;
            case "Success":
                toast.Show("Success", new NotificationOptions
                {
                    Type = NotificationType.Success,
                });
                break;
            case "Warn":
                toast.Show("Warn", new NotificationOptions
                {
                    Type = NotificationType.Warning
                });
                break;
            case "Error":
                toast.Show("Error", new NotificationOptions
                {
                    Type = NotificationType.Error
                });
                break;
            case "Long":
                toast.Show(
                    "Avalonia 是一个基于 .NET 的跨平台 UI 框架，灵感来源于 WPF，可在 Windows、macOS、Linux、移动设备和 WebAssembly 上使用同一套 XAML 代码开发应用程序，适合桌面和移动端开发者探索跨平台解决方案。",
                    new NotificationOptions
                    {
                        Type = NotificationType.Information,
                        Expiration = i
                    });
                break;
            case "Click":
                toast.Show("Avalonia", new NotificationOptions
                {
                    Type = NotificationType.Information,
                    OnClick = () => { Console.WriteLine("OnClick!"); }
                });
                break;
        }
    }

    private void ToastWithButtons(object? sender, RoutedEventArgs e)
    {
        var buttons = new ObservableCollection<OperateButtonEntry>
        {
            new("查看详情", _ => { Console.WriteLine("查看详情按钮被点击"); }),
            new("关闭", _ => { Console.WriteLine("关闭按钮被点击"); }, true),
            new("关闭并移除", _ => { Console.WriteLine("关闭并移除按钮被点击"); }, true,
                true)
        };

        toast.Show(
            "这是一条带有操作按钮的通知",
            new NotificationOptions
            {
                Type = NotificationType.Information,
                OperateButtons = buttons, IsButtonsInline = false
            }
        );
    }

    private void ToastWithButtonsInline(object? sender, RoutedEventArgs e)
    {
        var buttons = new ObservableCollection<OperateButtonEntry>
        {
            new("查看详情", _ => { Console.WriteLine("查看详情按钮被点击"); }),
            new("关闭", _ => { Console.WriteLine("关闭按钮被点击"); }, true),
            new("关闭并移除", _ => { Console.WriteLine("关闭并移除按钮被点击"); }, true,
                true)
        };

        toast.Show(
            "这是一条按钮在同一行的通知",
            new NotificationOptions
            {
                Type = NotificationType.Information,
                OperateButtons = buttons,
                IsButtonsInline = true,
                Expiration = new TimeSpan(0, 2, 0, 0, 0)
            }
        );
    }

    private void Notification(object? sender, RoutedEventArgs e)
    {
        var t = ((Button)sender).Tag.ToString();
        var i = new TimeSpan(1, 0, 3);
        switch (t)
        {
            case "Info":
                notification.Show("Info");
                break;
            case "Success":
                notification.Show("Success", "Success", new NotificationOptions
                {
                    Type = NotificationType.Success,
                });
                break;
            case "Warn":
                notification.Show("Warn", "Success", new NotificationOptions
                {
                    Type = NotificationType.Warning
                });
                break;
            case "Error":
                notification.Show("Error", new NotificationOptions
                {
                    Type = NotificationType.Error
                });
                break;
            case "Long":
                notification.Show(
                    "Avalonia 是一个基于 .NET 的跨平台 UI 框架，灵感来源于 WPF，可在 Windows、macOS、Linux、移动设备和 WebAssembly 上使用同一套 XAML 代码开发应用程序，适合桌面和移动端开发者探索跨平台解决方案。",
                    new NotificationOptions
                    {
                        Type = NotificationType.Information,
                        Expiration = i
                    });
                break;
            case "Click":
                notification.Show("Avalonia", new NotificationOptions
                {
                    Type = NotificationType.Information,
                    OnClick = () => { Console.WriteLine("OnClick!"); }
                });
                break;
        }
    }
}