using System;
using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Notifications;
using TioUi.Common.Classes;
using TioUi.Controls;

namespace TioUi.Demo.Pages;

public partial class ToastPage : UserControl
{
    private TioWindowToastManager? _toastManager;

    public ToastPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    private TioWindowToastManager GetToastManager()
    {
        if (_toastManager != null) return _toastManager;

        var lifetime = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;
        var mainWindow = lifetime?.MainWindow as MainWindow;
        _toastManager = mainWindow?.toast;
        return _toastManager!;
    }

    // 基础 Toast
    public void ShowInformationToast()
    {
        GetToastManager().Show("这是一条信息提示", NotificationType.Information);
    }

    public void ShowSuccessToast()
    {
        GetToastManager().Show("操作成功完成！", NotificationType.Success);
    }

    public void ShowWarningToast()
    {
        GetToastManager().Show("请注意这个警告信息", NotificationType.Warning);
    }

    public void ShowErrorToast()
    {
        GetToastManager().Show("发生了一个错误", NotificationType.Error);
    }

    // 不同持续时间
    public void ShowShortToast()
    {
        var toast = new Toast("短时间显示 (1秒)", NotificationType.Information, TimeSpan.FromSeconds(1));
        GetToastManager().Show(toast);
    }

    public void ShowNormalToast()
    {
        var toast = new Toast("正常时间显示 (3秒)", NotificationType.Success, TimeSpan.FromSeconds(3));
        GetToastManager().Show(toast);
    }

    public void ShowLongToast()
    {
        var toast = new Toast("长时间显示 (5秒)", NotificationType.Warning, TimeSpan.FromSeconds(5));
        GetToastManager().Show(toast);
    }

    public void ShowPersistentToast()
    {
        var toast = new Toast("持久显示，需要手动关闭", NotificationType.Information, TimeSpan.Zero);
        GetToastManager().Show(toast);
    }

    // 图标显示
    public void ShowToastWithIcon()
    {
        var options = new NotificationOptions
        {
            Content = "显示图标的 Toast",
            Type = NotificationType.Success,
            IsIconVisible = true
        };
        GetToastManager().Show(options);
    }

    public void ShowToastWithoutIcon()
    {
        var options = new NotificationOptions
        {
            Content = "隐藏图标的 Toast",
            Type = NotificationType.Information,
            IsIconVisible = false
        };
        GetToastManager().Show(options);
    }

    // 关闭按钮
    public void ShowToastWithCloseButton()
    {
        var toast = new Toast("带关闭按钮的 Toast", NotificationType.Information, TimeSpan.FromSeconds(5), showClose: true);
        GetToastManager().Show(toast);
    }

    public void ShowToastWithoutCloseButton()
    {
        var toast = new Toast("无关闭按钮的 Toast", NotificationType.Information, TimeSpan.FromSeconds(3), showClose: false);
        GetToastManager().Show(toast);
    }

    // 操作按钮
    public void ShowToastWithSingleButton()
    {
        var options = new NotificationOptions
        {
            Content = "带单个操作按钮的 Toast",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5),
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("确定", _ => { GetToastManager().Show("点击了确定按钮", NotificationType.Success); },
                    closeOnClick: true)
            }
        };
        GetToastManager().Show(options);
    }

    public void ShowToastWithMultipleButtons()
    {
        var options = new NotificationOptions
        {
            Content = "带多个操作按钮的 Toast",
            Type = NotificationType.Warning,
            Expiration = TimeSpan.FromSeconds(10),
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("确认", _ => { GetToastManager().Show("点击了确认", NotificationType.Success); },
                    closeOnClick: true),
                new OperateButtonEntry("取消", _ => { GetToastManager().Show("点击了取消", NotificationType.Information); },
                    closeOnClick: true),
                new OperateButtonEntry("查看详情", _ => { GetToastManager().Show("查看详情", NotificationType.Information); },
                    closeOnClick: false)
            }
        };
        GetToastManager().Show(options);
    }

    public void ShowToastWithInlineButtons()
    {
        var options = new NotificationOptions
        {
            Content = "内联按钮布局",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5),
            IsButtonsInline = true,
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("是", _ => { }, closeOnClick: true),
                new OperateButtonEntry("否", _ => { }, closeOnClick: true)
            }
        };
        GetToastManager().Show(options);
    }

    public void ShowToastWithStackedButtons()
    {
        var options = new NotificationOptions
        {
            Content = "堆叠按钮布局",
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
        GetToastManager().Show(options);
    }

    // 样式
    public void ShowLightToast()
    {
        var options = new NotificationOptions
        {
            Content = "Light 样式的 Toast",
            Type = NotificationType.Information,
            IsColorful = false
        };
        options.Classes.Clear();
        options.Classes.Add("Light");
        GetToastManager().Show(options);
    }

    public void ShowColorfulToast()
    {
        var options = new NotificationOptions
        {
            Content = "Colorful 样式的 Toast",
            Type = NotificationType.Success,
            IsColorful = true
        };
        GetToastManager().Show(options);
    }

    public void ShowDefaultToast()
    {
        var options = new NotificationOptions
        {
            Content = "默认样式的 Toast",
            Type = NotificationType.Warning,
            IsColorful = false
        };
        options.Classes.Clear();
        GetToastManager().Show(options);
    }

    // 交互事件
    public void ShowToastWithClick()
    {
        var toast = new Toast(
            "点击这个 Toast 试试",
            NotificationType.Information,
            TimeSpan.FromSeconds(5),
            onClick: () => { GetToastManager().Show("Toast 被点击了！", NotificationType.Success); }
        );
        GetToastManager().Show(toast);
    }

    public void ShowToastWithClose()
    {
        var toast = new Toast(
            "关闭时会触发事件",
            NotificationType.Information,
            TimeSpan.FromSeconds(3),
            onClose: () => { GetToastManager().Show("Toast 已关闭", NotificationType.Information); }
        );
        GetToastManager().Show(toast);
    }

    public void ShowToastWithTouchClose()
    {
        var options = new NotificationOptions
        {
            Content = "点击任意位置关闭",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(10),
            IsTouchClose = true
        };
        GetToastManager().Show(options);
    }

    // 长文本内容
    public void ShowLongContentToast()
    {
        var options = new NotificationOptions
        {
            Content = "这是一条很长的提示信息，用于测试 Toast 组件在显示长文本时的表现效果。Toast 应该能够正确地显示和换行处理长文本内容。",
            Type = NotificationType.Information,
            Expiration = TimeSpan.FromSeconds(5)
        };
        GetToastManager().Show(options);
    }

    public void ShowMultiLineToast()
    {
        var options = new NotificationOptions
        {
            Content = "第一行内容\n第二行内容\n第三行内容\n这是多行文本的 Toast 示例",
            Type = NotificationType.Success,
            Expiration = TimeSpan.FromSeconds(5)
        };
        GetToastManager().Show(options);
    }

    // 综合示例
    public void ShowComplexToast()
    {
        var options = new NotificationOptions
        {
            Content = "这是一个功能完整的 Toast 示例，包含图标、多个操作按钮和事件处理",
            Type = NotificationType.Warning,
            Expiration = TimeSpan.FromSeconds(10),
            IsIconVisible = true,
            IsCloseButtonVisible = true,
            IsButtonsInline = true,
            IsColorful = true,
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("重试", _ => { GetToastManager().Show("正在重试...", NotificationType.Information); },
                    closeOnClick: false),
                new OperateButtonEntry("忽略", _ => { }, closeOnClick: true)
            },
            OnClick = () => { GetToastManager().Show("Toast 被点击", NotificationType.Information); },
            OnClose = () => { GetToastManager().Show("Toast 已关闭", NotificationType.Information); }
        };
        GetToastManager().Show(options);
    }

    public void ShowCustomToast()
    {
        var options = new NotificationOptions
        {
            Content = "自定义配置的 Toast",
            Type = NotificationType.Error,
            Expiration = TimeSpan.FromSeconds(8),
            IsIconVisible = true,
            IsCloseButtonVisible = true,
            IsCollapseButtonVisible = false,
            IsButtonsInline = false,
            IsColorful = false,
            OperateButtons = new ObservableCollection<OperateButtonEntry>
            {
                new OperateButtonEntry("查看日志",
                    _ => { GetToastManager().Show("打开日志查看器", NotificationType.Information); }, closeOnClick: false),
                new OperateButtonEntry("报告问题", _ => { GetToastManager().Show("打开问题报告", NotificationType.Information); },
                    closeOnClick: false),
                new OperateButtonEntry("关闭", _ => { }, closeOnClick: true)
            }
        };
        options.Classes.Clear();
        options.Classes.Add("Light");
        GetToastManager().Show(options);
    }
}