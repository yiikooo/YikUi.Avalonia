using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace YikUi.Controls;

public class YikWindow : Window
{
    private YikTitleBar? _titleBar;
    private Border? _rootBorder;
    private Action<YikTitleBar>? _titleBarLoadedCallback;

    public YikWindow()
    {
        PropertyChanged += (_, args) =>
        {
            if (args.Property != WindowStateProperty) return;
            _rootBorder?.Margin = new Thickness(WindowState == WindowState.Maximized ? 8 : 0);
        };
    }

    protected override Type StyleKeyOverride => typeof(YikWindow);

    /// <summary>
    /// 获取 TitleBar 控件引用。此属性在模板应用后才可用。
    /// </summary>
    public YikTitleBar? TitleBar => _titleBar;

    /// <summary>
    /// 获取根 Border 控件引用。此属性在模板应用后才可用。
    /// </summary>
    public Border? RootBorder => _rootBorder;

    /// <summary>
    /// 当 TitleBar 加载完成时触发的事件
    /// </summary>
    public event EventHandler<YikTitleBar>? TitleBarLoaded;

    /// <summary>
    /// 设置 TitleBar 加载完成后的回调。如果 TitleBar 已经加载，则立即执行回调。
    /// </summary>
    /// <param name="callback">回调函数，参数为 TitleBar 实例</param>
    public void OnTitleBarLoaded(Action<YikTitleBar> callback)
    {
        if (_titleBar != null)
        {
            callback(_titleBar);
        }
        else
        {
            _titleBarLoadedCallback = callback;
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        // 从模板中获取控件引用
        _titleBar = e.NameScope.Find<YikTitleBar>("PART_TitleBar");
        _rootBorder = e.NameScope.Find<Border>("PART_Root");

        if (_titleBar != null)
        {
            TitleBarLoaded?.Invoke(this, _titleBar);
            _titleBarLoadedCallback?.Invoke(_titleBar);
            _titleBarLoadedCallback = null;
        }

        // 初始化根 Border 的 Margin
        if (_rootBorder != null)
        {
            _rootBorder.Margin = new Thickness(WindowState == WindowState.Maximized ? 10 : 0);
        }
    }

    #region Styled Properties

    /// <summary>
    /// 窗口图标，接管 Avalonia 的 Icon 属性
    /// </summary>
    public static new readonly StyledProperty<WindowIcon?> IconProperty =
        Window.IconProperty.AddOwner<YikWindow>();

    public new WindowIcon? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public static readonly StyledProperty<bool> IsCloseBtnShowProperty =
        AvaloniaProperty.Register<YikWindow, bool>(nameof(IsCloseBtnShow), defaultValue: true);

    public bool IsCloseBtnShow
    {
        get => GetValue(IsCloseBtnShowProperty);
        set => SetValue(IsCloseBtnShowProperty, value);
    }

    public static readonly StyledProperty<bool> IsMaxBtnShowProperty =
        AvaloniaProperty.Register<YikWindow, bool>(nameof(IsMaxBtnShow), defaultValue: true);

    public bool IsMaxBtnShow
    {
        get => GetValue(IsMaxBtnShowProperty);
        set => SetValue(IsMaxBtnShowProperty, value);
    }

    public static readonly StyledProperty<bool> IsMinBtnShowProperty =
        AvaloniaProperty.Register<YikWindow, bool>(nameof(IsMinBtnShow), defaultValue: true);

    public bool IsMinBtnShow
    {
        get => GetValue(IsMinBtnShowProperty);
        set => SetValue(IsMinBtnShowProperty, value);
    }

    /// <summary>
    /// 关闭按钮点击时的回调。返回 true 表示阻止默认关闭行为，返回 false 或 null 表示执行默认关闭。
    /// </summary>
    public static readonly StyledProperty<Func<bool>?> OnCloseProperty =
        AvaloniaProperty.Register<YikWindow, Func<bool>?>(nameof(OnClose));

    public Func<bool>? OnClose
    {
        get => GetValue(OnCloseProperty);
        set => SetValue(OnCloseProperty, value);
    }

    public static readonly StyledProperty<object?> TitleBarLeftContentProperty =
        AvaloniaProperty.Register<YikWindow, object?>(nameof(TitleBarLeftContent));

    public object? TitleBarLeftContent
    {
        get => GetValue(TitleBarLeftContentProperty);
        set => SetValue(TitleBarLeftContentProperty, value);
    }

    public static readonly StyledProperty<object?> TitleBarRightContentProperty =
        AvaloniaProperty.Register<YikWindow, object?>(nameof(TitleBarRightContent));

    public object? TitleBarRightContent
    {
        get => GetValue(TitleBarRightContentProperty);
        set => SetValue(TitleBarRightContentProperty, value);
    }

    public static readonly StyledProperty<Thickness> ContentMarginProperty =
        AvaloniaProperty.Register<YikWindow, Thickness>(nameof(ContentMargin), defaultValue: new Thickness(10));

    public Thickness ContentMargin
    {
        get => GetValue(ContentMarginProperty);
        set => SetValue(ContentMarginProperty, value);
    }

    #endregion
}