using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace TioUi.Controls;

public class TioWindow : Window
{
    private Action<TioTitleBar>? _titleBarLoadedCallback;

    /*public TioWindow()
    {
        PropertyChanged += (_, args) =>
        {
            if (args.Property != WindowStateProperty) return;
            RootBorder?.Margin = new Thickness(WindowState == WindowState.Maximized ? 8 : 0);
        };
    }*/

    protected override Type StyleKeyOverride => typeof(TioWindow);

    /// <summary>
    ///     获取 TitleBar 控件引用。此属性在模板应用后才可用。
    /// </summary>
    public TioTitleBar? TitleBar { get; private set; }

    /// <summary>
    ///     获取根 Border 控件引用。此属性在模板应用后才可用。
    /// </summary>
    public Border? RootBorder { get; private set; }

    /// <summary>
    ///     当 TitleBar 加载完成时触发的事件
    /// </summary>
    public event EventHandler<TioTitleBar>? TitleBarLoaded;

    /// <summary>
    ///     设置 TitleBar 加载完成后的回调。如果 TitleBar 已经加载，则立即执行回调。
    /// </summary>
    /// <param name="callback">回调函数，参数为 TitleBar 实例</param>
    public void OnTitleBarLoaded(Action<TioTitleBar> callback)
    {
        if (TitleBar != null)
            callback(TitleBar);
        else
            _titleBarLoadedCallback = callback;
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        // 从模板中获取控件引用
        TitleBar = e.NameScope.Find<TioTitleBar>("PART_TitleBar");
        RootBorder = e.NameScope.Find<Border>("PART_Root");

        if (TitleBar != null)
        {
            TitleBarLoaded?.Invoke(this, TitleBar);
            _titleBarLoadedCallback?.Invoke(TitleBar);
            _titleBarLoadedCallback = null;
        }

        // 初始化根 Border 的 Margin
        if (RootBorder != null) RootBorder.Margin = new Thickness(WindowState == WindowState.Maximized ? 10 : 0);
    }

    #region Styled Properties

    public static readonly StyledProperty<bool> IsCloseBtnShowProperty =
        AvaloniaProperty.Register<TioWindow, bool>(nameof(IsCloseBtnShow), true);

    public bool IsCloseBtnShow
    {
        get => GetValue(IsCloseBtnShowProperty);
        set => SetValue(IsCloseBtnShowProperty, value);
    }

    public static readonly StyledProperty<bool> IsMaxBtnShowProperty =
        AvaloniaProperty.Register<TioWindow, bool>(nameof(IsMaxBtnShow), true);

    public bool IsMaxBtnShow
    {
        get => GetValue(IsMaxBtnShowProperty);
        set => SetValue(IsMaxBtnShowProperty, value);
    }

    public static readonly StyledProperty<bool> IsMinBtnShowProperty =
        AvaloniaProperty.Register<TioWindow, bool>(nameof(IsMinBtnShow), true);

    public bool IsMinBtnShow
    {
        get => GetValue(IsMinBtnShowProperty);
        set => SetValue(IsMinBtnShowProperty, value);
    }

    public virtual bool OnClose()
    {
        return false;
    }

    public static readonly StyledProperty<object?> TitleBarLeftContentProperty =
        AvaloniaProperty.Register<TioWindow, object?>(nameof(TitleBarLeftContent));

    public object? TitleBarLeftContent
    {
        get => GetValue(TitleBarLeftContentProperty);
        set => SetValue(TitleBarLeftContentProperty, value);
    }

    public static readonly StyledProperty<object?> TitleBarRightContentProperty =
        AvaloniaProperty.Register<TioWindow, object?>(nameof(TitleBarRightContent));

    public object? TitleBarRightContent
    {
        get => GetValue(TitleBarRightContentProperty);
        set => SetValue(TitleBarRightContentProperty, value);
    }

    public static readonly StyledProperty<Thickness> ContentMarginProperty =
        AvaloniaProperty.Register<TioWindow, Thickness>(nameof(ContentMargin), new Thickness(10));

    public Thickness ContentMargin
    {
        get => GetValue(ContentMarginProperty);
        set => SetValue(ContentMarginProperty, value);
    }

    #endregion
}