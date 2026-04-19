using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using Avalonia.Threading;
using TioUi.Common.Classes;
using TioUi.Controls;
using Notification = TioUi.Controls.Notification;

namespace TioUi.Demo.Pages;

public partial class NotificationPage : UserControl
{
    private TioNotificationManager? _notificationManager;

    public NotificationPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    private TioNotificationManager GetNotificationManager()
    {
        if (_notificationManager != null) return _notificationManager;

        _notificationManager = App.RootView.NotificationManager;
        return _notificationManager!;
    }

    // 位置调整
    public void SetPositionTopLeft()
    {
        GetNotificationManager().Position = NotificationPosition.TopLeft;
        GetNotificationManager()
            .Show("通知位置", "已切换到左上角", new NotificationOptions { Type = NotificationType.Information });
    }

    public void SetPositionTopCenter()
    {
        GetNotificationManager().Position = NotificationPosition.TopCenter;
        GetNotificationManager()
            .Show("通知位置", "已切换到顶部居中", new NotificationOptions { Type = NotificationType.Information });
    }

    public void SetPositionTopRight()
    {
        GetNotificationManager().Position = NotificationPosition.TopRight;
        GetNotificationManager()
            .Show("通知位置", "已切换到右上角", new NotificationOptions { Type = NotificationType.Information });
    }

    public void SetPositionBottomLeft()
    {
        GetNotificationManager().Position = NotificationPosition.BottomLeft;
        GetNotificationManager()
            .Show("通知位置", "已切换到左下角", new NotificationOptions { Type = NotificationType.Information });
    }

    public void SetPositionBottomCenter()
    {
        GetNotificationManager().Position = NotificationPosition.BottomCenter;
        GetNotificationManager()
            .Show("通知位置", "已切换到底部居中", new NotificationOptions { Type = NotificationType.Information });
    }

    public void SetPositionBottomRight()
    {
        GetNotificationManager().Position = NotificationPosition.BottomRight;
        GetNotificationManager()
            .Show("通知位置", "已切换到右下角", new NotificationOptions { Type = NotificationType.Information });
    }

    // 基础 Notification
    public void ShowInformationNotification()
    {
        var notification = new Notification("信息通知", "这是一条信息类型的通知", NotificationType.Information);
        GetNotificationManager().Show(notification);
    }

    public void ShowSuccessNotification()
    {
        var notification = new Notification("成功通知", "操作已成功完成！", NotificationType.Success);
        GetNotificationManager().Show(notification);
    }

    public void ShowWarningNotification()
    {
        var notification = new Notification("警告通知", "请注意这个警告信息", NotificationType.Warning);
        GetNotificationManager().Show(notification);
    }

    public void ShowErrorNotification()
    {
        var notification = new Notification("错误通知", "发生了一个错误", NotificationType.Error);
        GetNotificationManager().Show(notification);
    }

    // 带标题的 Notification
    public void ShowNotificationWithTitle()
    {
        GetNotificationManager().Show("系统通知", "这是一条带标题的通知消息");
    }

    public void ShowNotificationWithLongTitle()
    {
        GetNotificationManager().Show(
            "这是一个很长很长的标题用于测试标题显示效果",
            "通知内容部分",
            new NotificationOptions { Type = NotificationType.Information }
        );
    }

    public void ShowNotificationOnlyTitle()
    {
        var options = new NotificationOptions
        {
            Title = "仅显示标题的通知",
            Type = NotificationType.Success
        };
        GetNotificationManager().Show(options);
    }

    // 不同持续时间
    public void ShowShortNotification()
    {
        var notification = new Notification(
            "短时间通知",
            "1秒后自动关闭",
            NotificationType.Information,
            TimeSpan.FromSeconds(1)
        );
        GetNotificationManager().Show(notification);
    }

    public void ShowNormalNotification()
    {
        var notification = new Notification(
            "正常时间通知",
            "3秒后自动关闭",
            NotificationType.Success,
            TimeSpan.FromSeconds(3)
        );
        GetNotificationManager().Show(notification);
    }

    public void ShowLongNotification()
    {
        var notification = new Notification(
            "长时间通知",
            "5秒后自动关闭",
            NotificationType.Warning,
            TimeSpan.FromSeconds(5)
        );
        GetNotificationManager().Show(notification);
    }

    public void ShowPersistentNotification()
    {
        var notification = new Notification(
            "持久通知",
            "需要手动关闭",
            NotificationType.Information,
            TimeSpan.Zero
        );
        GetNotificationManager().Show(notification);
    }

    // 图标显示
    public void ShowNotificationWithIcon()
    {
        var options = new NotificationOptions
        {
            Title = "带图标通知",
            Content = "显示类型图标",
            Type = NotificationType.Success,
            IsIconVisible = true
        };
        GetNotificationManager().Show(options);
    }

    public void ShowNotificationWithoutIcon()
    {
        var options = new NotificationOptions
        {
            Title = "无图标通知",
            Content = "隐藏类型图标",
            Type = NotificationType.Information,
            IsIconVisible = false
        };
        GetNotificationManager().Show(options);
    }

    // 关闭按钮
    public void ShowNotificationWithCloseButton()
    {
        var notification = new Notification(
            "带关闭按钮",
            "可以手动关闭",
            NotificationType.Information,
            TimeSpan.FromSeconds(5),
            showClose: true
        );
        GetNotificationManager().Show(notification);
    }

    public void ShowNotificationWithoutCloseButton()
    {
        var notification = new Notification(
            "无关闭按钮",
            "3秒后自动关闭",
            NotificationType.Information,
            TimeSpan.FromSeconds(3),
            showClose: false
        );
        GetNotificationManager().Show(notification);
    }

    // 操作按钮
    public void ShowNotificationWithSingleButton()
    {
        var options = new NotificationOptions
        {
            Title = "单个操作按钮",
            Content = "点击按钮执行操作",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5),
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("确定",
                    _ =>
                    {
                        GetNotificationManager().Show("操作", "点击了确定按钮",
                            new NotificationOptions { Type = NotificationType.Success });
                    }, closeOnClick: true)
            }
        };
        GetNotificationManager().Show(options);
    }

    public void ShowNotificationWithMultipleButtons()
    {
        var options = new NotificationOptions
        {
            Title = "多个操作按钮",
            Content = "选择一个操作",
            Type = NotificationType.Warning,
            Expiration = TimeSpan.FromSeconds(10),
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("确认",
                    _ =>
                    {
                        GetNotificationManager().Show("操作", "已确认",
                            new NotificationOptions { Type = NotificationType.Success });
                    }, closeOnClick: true),
                new OperateButtonEntry("取消",
                    _ =>
                    {
                        GetNotificationManager().Show("操作", "已取消",
                            new NotificationOptions { Type = NotificationType.Information });
                    }, closeOnClick: true),
                new OperateButtonEntry("查看详情",
                    _ =>
                    {
                        GetNotificationManager().Show("操作", "查看详情",
                            new NotificationOptions { Type = NotificationType.Information });
                    }, closeOnClick: false)
            }
        };
        GetNotificationManager().Show(options);
    }

    public void ShowNotificationWithInlineButtons()
    {
        var options = new NotificationOptions
        {
            Title = "内联按钮",
            Content = "按钮横向排列",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5),
            IsButtonsInline = true,
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("是", _ => { }, closeOnClick: true),
                new OperateButtonEntry("否", _ => { }, closeOnClick: true)
            }
        };
        GetNotificationManager().Show(options);
    }

    public void ShowNotificationWithStackedButtons()
    {
        var options = new NotificationOptions
        {
            Title = "堆叠按钮",
            Content = "按钮纵向排列",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5),
            IsButtonsInline = false,
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("选项一", _ => { }, closeOnClick: true),
                new OperateButtonEntry("选项二", _ => { }, closeOnClick: true),
                new OperateButtonEntry("选项三", _ => { }, closeOnClick: true)
            }
        };
        GetNotificationManager().Show(options);
    }

    // 样式
    public void ShowLightNotification()
    {
        var options = new NotificationOptions
        {
            Title = "Light 样式",
            Content = "浅色主题样式",
            Type = NotificationType.Information,
            IsColorful = false
        };
        options.Classes.Clear();
        options.Classes.Add("Light");
        GetNotificationManager().Show(options);
    }

    public void ShowColorfulNotification()
    {
        var options = new NotificationOptions
        {
            Title = "Colorful 样式",
            Content = "彩色主题样式",
            Type = NotificationType.Success,
            IsColorful = true
        };
        GetNotificationManager().Show(options);
    }

    public void ShowDefaultNotification()
    {
        var options = new NotificationOptions
        {
            Title = "默认样式",
            Content = "默认主题样式",
            Type = NotificationType.Warning,
            IsColorful = false
        };
        options.Classes.Clear();
        GetNotificationManager().Show(options);
    }

    // 交互事件
    public void ShowNotificationWithClick()
    {
        var notification = new Notification(
            "点击事件",
            "点击通知试试",
            NotificationType.Information,
            TimeSpan.FromSeconds(5),
            onClick: () =>
            {
                GetNotificationManager()
                    .Show("事件", "通知被点击了！", new NotificationOptions { Type = NotificationType.Success });
            }
        );
        GetNotificationManager().Show(notification);
    }

    public void ShowNotificationWithClose()
    {
        var notification = new Notification(
            "关闭事件",
            "关闭时会触发事件",
            NotificationType.Information,
            TimeSpan.FromSeconds(3),
            onClose: () =>
            {
                GetNotificationManager().Show("事件", "通知已关闭",
                    new NotificationOptions { Type = NotificationType.Information });
            }
        );
        GetNotificationManager().Show(notification);
    }

    public void ShowNotificationWithTouchClose()
    {
        var options = new NotificationOptions
        {
            Title = "点击关闭",
            Content = "点击通知任意位置关闭",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(10),
            IsTouchClose = true
        };
        GetNotificationManager().Show(options);
    }

    // 长文本内容
    public void ShowLongContentNotification()
    {
        var options = new NotificationOptions
        {
            Title = "长文本通知",
            Content = "这是一条很长的通知内容，用于测试 Notification 组件在显示长文本时的表现效果。通知应该能够正确地显示和换行处理长文本内容，确保用户能够完整阅读所有信息。",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5)
        };
        GetNotificationManager().Show(options);
    }

    public void ShowMultiLineNotification()
    {
        var options = new NotificationOptions
        {
            Title = "多行文本通知",
            Content = "第一行内容\n第二行内容\n第三行内容\n这是多行文本的通知示例",
            Type = NotificationType.Success,
            Expiration = TimeSpan.FromSeconds(5)
        };
        GetNotificationManager().Show(options);
    }

    public void ShowLongTitleAndContent()
    {
        var options = new NotificationOptions
        {
            Title = "这是一个很长很长的标题用于测试标题在通知中的显示效果和换行处理",
            Content = "这也是一段很长的内容文本，用于测试当标题和内容都很长时，通知组件的整体显示效果和布局处理能力。",
            Type = NotificationType.Warning,
            Expiration = TimeSpan.FromSeconds(5)
        };
        GetNotificationManager().Show(options);
    }

    // 折叠按钮
    public void ShowNotificationWithCollapseButton()
    {
        var options = new NotificationOptions
        {
            Title = "折叠按钮",
            Content = "显示折叠按钮",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5),
            IsCollapseButtonVisible = true,
            IsCloseButtonVisible = false
        };
        GetNotificationManager().Show(options);
    }

    public void ShowNotificationWithBothButtons()
    {
        var options = new NotificationOptions
        {
            Title = "折叠和关闭",
            Content = "同时显示折叠和关闭按钮",
            Type = NotificationType.Success,
            Expiration = TimeSpan.FromSeconds(5),
            IsCollapseButtonVisible = true,
            IsCloseButtonVisible = true
        };
        GetNotificationManager().Show(options);
    }

    // 综合示例
    public void ShowComplexNotification()
    {
        var options = new NotificationOptions
        {
            Title = "复杂通知示例",
            Content = "这是一个功能完整的通知示例，包含标题、内容、图标、多个操作按钮和事件处理",
            Type = NotificationType.Warning,
            Expiration = TimeSpan.FromSeconds(10),
            IsIconVisible = true,
            IsCloseButtonVisible = true,
            IsCollapseButtonVisible = false,
            IsButtonsInline = true,
            IsColorful = true,
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("重试",
                    _ =>
                    {
                        GetNotificationManager().Show("操作", "正在重试...",
                            new NotificationOptions { Type = NotificationType.Information });
                    }, closeOnClick: false),
                new OperateButtonEntry("忽略", _ => { }, closeOnClick: true)
            },
            OnClick = () =>
            {
                GetNotificationManager().Show("事件", "通知被点击",
                    new NotificationOptions { Type = NotificationType.Information });
            },
            OnClose = () =>
            {
                GetNotificationManager().Show("事件", "通知已关闭",
                    new NotificationOptions { Type = NotificationType.Information });
            }
        };
        GetNotificationManager().Show(options);
    }

    public void ShowCustomNotification()
    {
        var options = new NotificationOptions
        {
            Title = "自定义通知",
            Content = "完全自定义配置的通知",
            Type = NotificationType.Error,
            Expiration = TimeSpan.FromSeconds(8),
            IsIconVisible = true,
            IsCloseButtonVisible = true,
            IsCollapseButtonVisible = true,
            IsButtonsInline = false,
            IsColorful = false,
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("查看日志",
                    _ =>
                    {
                        GetNotificationManager().Show("操作", "打开日志查看器",
                            new NotificationOptions { Type = NotificationType.Information });
                    }, closeOnClick: false),
                new OperateButtonEntry("报告问题",
                    _ =>
                    {
                        GetNotificationManager().Show("操作", "打开问题报告",
                            new NotificationOptions { Type = NotificationType.Information });
                    }, closeOnClick: false),
                new OperateButtonEntry("关闭", _ => { }, closeOnClick: true)
            }
        };
        options.Classes.Clear();
        options.Classes.Add("Light");
        GetNotificationManager().Show(options);
    }

    public void ShowMultipleNotifications()
    {
        // 连续显示多个不同类型的通知
        GetNotificationManager().Show("第一条", "Information 通知",
            new NotificationOptions { Type = NotificationType.Information });

        Task.Delay(500).ContinueWith(_ =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                GetNotificationManager().Show("第二条", "Success 通知",
                    new NotificationOptions { Type = NotificationType.Success });
            });
        });

        Task.Delay(1000).ContinueWith(_ =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                GetNotificationManager().Show("第三条", "Warning 通知",
                    new NotificationOptions { Type = NotificationType.Warning });
            });
        });

        Task.Delay(1500).ContinueWith(_ =>
        {
            Dispatcher.UIThread.Post(() =>
            {
                GetNotificationManager().Show("第四条", "Error 通知",
                    new NotificationOptions { Type = NotificationType.Error });
            });
        });
    }
}