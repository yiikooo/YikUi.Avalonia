using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using YikUi.Controls;

namespace YikUi.Demo.Pages;

public partial class ContentDialogPage : UserControl
{
    public ContentDialogPage()
    {
        InitializeComponent();
        DataContext = new ContentDialogDemoViewModel();
    }
}

public partial class ContentDialogDemoViewModel : ObservableObject
{
    [ObservableProperty] private string _closeText = "取消";
    [ObservableProperty] private string _content = "您确定要执行此操作吗？此操作不可撤销。";
    [ObservableProperty] private ContentDialogButton _defaultButton = ContentDialogButton.Primary;
    [ObservableProperty] private string _deferralResult = "— 尚未显示 —";
    [ObservableProperty] private bool _fullSizeDesired;

    // ── 结果显示 ──────────────────────────────────────────────────────

    [ObservableProperty] private string _lastResult = "— 尚未显示 —";
    [ObservableProperty] private string _presetResult = "— 尚未显示 —";
    [ObservableProperty] private string _primaryText = "确认";
    [ObservableProperty] private string _secondaryText = "稍后再说";

    [ObservableProperty] private int _showCount;
    // ── 基础用法绑定属性 ──────────────────────────────────────────────

    [ObservableProperty] private string _title = "操作确认";

    // ── 基础用法命令 ──────────────────────────────────────────────────

    [RelayCommand]
    private async Task ShowBasic()
    {
        var dialog = new ContentDialog
        {
            Title = Title,
            Content = new TextBlock
            {
                Text = Content,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = 460
            },
            PrimaryButtonText = string.IsNullOrWhiteSpace(PrimaryText) ? null : PrimaryText,
            SecondaryButtonText = string.IsNullOrWhiteSpace(SecondaryText) ? null : SecondaryText,
            CloseButtonText = string.IsNullOrWhiteSpace(CloseText) ? null : CloseText,
            DefaultButton = DefaultButton,
            FullSizeDesired = FullSizeDesired
        };

        var result = await dialog.ShowAsync();
        ShowCount++;
        LastResult = result switch
        {
            ContentDialogResult.Primary => $"Primary（{PrimaryText}）",
            ContentDialogResult.Secondary => $"Secondary（{SecondaryText}）",
            ContentDialogResult.None => $"None / 关闭（{CloseText}）",
            _ => result.ToString()
        };
    }

    // ── 预设示例命令 ──────────────────────────────────────────────────

    [RelayCommand]
    private async Task ShowDeleteConfirm()
    {
        var dialog = new ContentDialog
        {
            Title = "删除文件",
            Content = new StackPanel
            {
                Spacing = 8,
                Children =
                {
                    new TextBlock
                    {
                        Text = "确定要删除「重要文档.docx」吗？",
                        TextWrapping = TextWrapping.Wrap
                    },
                    new TextBlock
                    {
                        Text = "删除后文件将移入回收站，您可以在 30 天内恢复。",
                        TextWrapping = TextWrapping.Wrap,
                        Opacity = 0.65
                    }
                }
            },
            PrimaryButtonText = "删除",
            CloseButtonText = "取消",
            DefaultButton = ContentDialogButton.Close // 默认聚焦"取消"，防止误删
        };

        var result = await dialog.ShowAsync();
        PresetResult = result == ContentDialogResult.Primary ? "已确认删除" : "已取消";
    }

    [RelayCommand]
    private async Task ShowSave()
    {
        var dialog = new ContentDialog
        {
            Title = "保存更改",
            Content = new TextBlock
            {
                Text = "您有未保存的更改，是否在退出前保存？",
                TextWrapping = TextWrapping.Wrap
            },
            PrimaryButtonText = "保存",
            SecondaryButtonText = "不保存",
            CloseButtonText = "取消",
            DefaultButton = ContentDialogButton.Primary
        };

        var result = await dialog.ShowAsync();
        PresetResult = result switch
        {
            ContentDialogResult.Primary => "已保存并退出",
            ContentDialogResult.Secondary => "已放弃更改并退出",
            _ => "已取消，留在当前页面"
        };
    }

    [RelayCommand]
    private async Task ShowAlert()
    {
        var dialog = new ContentDialog
        {
            Title = "提示",
            Content = new TextBlock
            {
                Text = "您的会话将在 5 分钟后过期，请注意保存您的工作。",
                TextWrapping = TextWrapping.Wrap
            },
            CloseButtonText = "我知道了",
            DefaultButton = ContentDialogButton.Close
        };

        await dialog.ShowAsync();
        PresetResult = "已读取提示";
    }

    [RelayCommand]
    private async Task ShowInputDialog()
    {
        var inputBox = new TextBox
        {
            PlaceholderText = "请输入新名称",
            MinWidth = 300
        };

        var dialog = new ContentDialog
        {
            Title = "重命名",
            Content = new StackPanel
            {
                Spacing = 8,
                Children =
                {
                    new TextBlock { Text = "为所选项目输入一个新名称：" },
                    inputBox
                }
            },
            PrimaryButtonText = "重命名",
            CloseButtonText = "取消",
            DefaultButton = ContentDialogButton.Primary
        };

        // 打开时聚焦输入框
        dialog.Opened += (_, _) => inputBox.Focus();

        var result = await dialog.ShowAsync();
        PresetResult = result == ContentDialogResult.Primary && !string.IsNullOrWhiteSpace(inputBox.Text)
            ? $"已重命名为：「{inputBox.Text}」"
            : "已取消重命名";
    }

    [RelayCommand]
    private async Task ShowCustomIcon()
    {
        var dialog = new ContentDialog
        {
            Title = "自定义图标示例",
            Content = new StackPanel
            {
                Spacing = 8,
                Children =
                {
                    new TextBlock
                    {
                        Text = "此对话框使用了自定义图标：",
                        TextWrapping = TextWrapping.Wrap
                    },
                    new TextBlock
                    {
                        Text = "• 主要按钮：心形图标",
                        Opacity = 0.7
                    },
                    new TextBlock
                    {
                        Text = "• 次要按钮：星形图标",
                        Opacity = 0.7
                    },
                    new TextBlock
                    {
                        Text = "• 关闭按钮：无图标",
                        Opacity = 0.7
                    }
                }
            },
            PrimaryButtonText = "喜欢",
            SecondaryButtonText = "收藏",
            CloseButtonText = "关闭",
            DefaultButton = ContentDialogButton.Primary,
            // 自定义图标
            PrimaryButtonIcon = Geometry.Parse(
                "M47.6 300.4L228.3 469.1c7.5 7 17.4 10.9 27.7 10.9s20.2-3.9 27.7-10.9L464.4 300.4c30.4-28.3 47.6-68 47.6-109.5v-5.8c0-69.9-50.5-129.5-119.4-141C347 36.5 300.6 51.4 268 84L256 96 244 84c-32.6-32.6-79-47.5-124.6-39.9C50.5 55.6 0 115.2 0 185.1v5.8c0 41.5 17.2 81.2 47.6 109.5z"),
            SecondaryButtonIcon = Geometry.Parse(
                "M316.9 18C311.6 7 300.4 0 288.1 0s-23.4 7-28.8 18L195 150.3 51.4 171.5c-12 1.8-22 10.2-25.7 21.7s-.7 24.2 7.9 32.7L137.8 329 113.2 474.7c-2 12 3 24.2 12.9 31.3s23 8 33.8 2.3l128.3-68.5 128.3 68.5c10.8 5.7 23.9 4.9 33.8-2.3s14.9-19.3 12.9-31.3L438.5 329 542.7 225.9c8.6-8.5 11.7-21.2 7.9-32.7s-13.7-19.9-25.7-21.7L381.2 150.3 316.9 18z"),
            IsCloseButtonIconVisible = false,
            IsSecondaryButtonIconVisible = true,
            IsPrimaryButtonIconVisible = true
        };

        var result = await dialog.ShowAsync();
        PresetResult = result switch
        {
            ContentDialogResult.Primary => "已点击「喜欢」❤️",
            ContentDialogResult.Secondary => "已点击「收藏」⭐",
            _ => "已关闭"
        };
    }

    // ── Deferral 演示命令 ─────────────────────────────────────────────

    [RelayCommand]
    private async Task ShowDeferral()
    {
        var cancelSwitch = new ToggleSwitch
        {
            Content = "模拟验证失败（勾选后点击确认，对话框不会关闭）",
            IsChecked = false
        };

        var dialog = new ContentDialog
        {
            Title = "异步验证演示",
            Content = new StackPanel
            {
                Spacing = 12,
                Children =
                {
                    new TextBlock
                    {
                        Text = "点击「提交」后将等待 1 秒模拟服务端校验。",
                        TextWrapping = TextWrapping.Wrap,
                        Opacity = 0.7
                    },
                    cancelSwitch
                }
            },
            PrimaryButtonText = "提交",
            CloseButtonText = "取消",
            DefaultButton = ContentDialogButton.Primary
        };

        dialog.PrimaryButtonClick += async (sender, args) =>
        {
            // 获取 Deferral，阻止对话框立即关闭
            var deferral = args.GetDeferral();
            try
            {
                // 模拟 1 秒异步校验
                await Task.Delay(1000);

                if (cancelSwitch.IsChecked == true)
                {
                    // 校验失败：取消关闭
                    args.Cancel = true;
                }
            }
            finally
            {
                deferral.Complete();
            }
        };

        var result = await dialog.ShowAsync();
        DeferralResult = result == ContentDialogResult.Primary
            ? "校验通过，已提交"
            : "已取消提交";
    }
}