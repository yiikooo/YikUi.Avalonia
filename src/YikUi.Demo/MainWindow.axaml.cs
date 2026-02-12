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
    private YikWindowToastManager toast;

    public MainWindow()
    {
        InitializeComponent();
        toast = new YikWindowToastManager(GetTopLevel(this));
    }

    private void Notice(object? sender, RoutedEventArgs e)
    {
        var t = ((Button)sender).Tag.ToString();
        var i = new TimeSpan(1, 0, 3);
        switch (t)
        {
            case "Info":
                Overlay.Notice("Info");
                break;
            case "Success":
                Overlay.Notice("Success", NotificationType.Success, new NotificationOptions
                {
                    Expiration = i
                });
                break;
            case "Warn":
                Overlay.Notice("Warn", NotificationType.Warning);
                break;
            case "Error":
                Overlay.Notice("Error", NotificationType.Error);
                break;
            case "Long":
                Overlay.Notice(
                    "Avalonia 是一个基于 .NET 的跨平台 UI 框架，灵感来源于 WPF，可在 Windows、macOS、Linux、移动设备和 WebAssembly 上使用同一套 XAML 代码开发应用程序，适合桌面和移动端开发者探索跨平台解决方案。",
                    NotificationType.Information,
                    new NotificationOptions
                    {
                        Expiration = i
                    });
                break;
            case "Click":
                Overlay.Notice("Avalonia", NotificationType.Information, new NotificationOptions
                {
                    OnClick = () => { Console.WriteLine("OnClick!"); }
                });
                break;
        }
    }

    private void NoticeWithButtons(object? sender, RoutedEventArgs e)
    {
        var buttons = new ObservableCollection<OperateButtonEntry>
        {
            new("查看详情", _ => { Console.WriteLine("查看详情按钮被点击"); }),
            new("关闭", _ => { Console.WriteLine("关闭按钮被点击"); }, true),
            new("关闭并移除", _ => { Console.WriteLine("关闭并移除按钮被点击"); }, true,
                true)
        };

        Overlay.Notice(
            "这是一条带有操作按钮的通知",
            NotificationType.Information,
            new NotificationOptions
            {
                OperateButtons = buttons
            }
        );
    }


    private void NoticeWithButtonsInline(object? sender, RoutedEventArgs e)
    {
        var buttons = new ObservableCollection<OperateButtonEntry>
        {
            new("查看详情", _ => { Console.WriteLine("查看详情按钮被点击"); }),
            new("关闭", _ => { Console.WriteLine("关闭按钮被点击"); }, true),
            new("关闭并移除", _ => { Console.WriteLine("关闭并移除按钮被点击"); }, true,
                true)
        };

        Overlay.Notice(
            "这是一条按钮在同一行的通知",
            NotificationType.Information,
            new NotificationOptions
            {
                OperateButtons = buttons,
                IsButtonsInline = true,
                Expiration = new TimeSpan(0, 2, 0, 0, 0)
            }
        );
    }
}