using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace YikUi.Controls;

public class YikWindow : Window
{
    public YikTitleBar TitleBar => this.FindControl<YikTitleBar>("TitleBar");
    
    #region Styled Properties

    // TitleBar properties
    public static readonly StyledProperty<bool> IsCloseBtnExitAppProperty =
        AvaloniaProperty.Register<YikWindow, bool>(nameof(IsCloseBtnExitApp));

    public bool IsCloseBtnExitApp
    {
        get => GetValue(IsCloseBtnExitAppProperty);
        set => SetValue(IsCloseBtnExitAppProperty, value);
    }

    public static readonly StyledProperty<bool> IsCloseBtnHideWindowProperty =
        AvaloniaProperty.Register<YikWindow, bool>(nameof(IsCloseBtnHideWindow));

    public bool IsCloseBtnHideWindow
    {
        get => GetValue(IsCloseBtnHideWindowProperty);
        set => SetValue(IsCloseBtnHideWindowProperty, value);
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

    public static readonly StyledProperty<object?> TitleBarLeftContentProperty =
        AvaloniaProperty.Register<YikWindow, object?>(nameof(TitleBarLeftContent));

    public object? TitleBarLeftContent
    {
        get => GetValue(TitleBarLeftContentProperty);
        set => SetValue(TitleBarLeftContentProperty, value);
    }

    public static readonly StyledProperty<Action?> TitleBarOnExitProperty =
        AvaloniaProperty.Register<YikWindow, Action?>(nameof(TitleBarOnExit));

    public Action? TitleBarOnExit
    {
        get => GetValue(TitleBarOnExitProperty);
        set => SetValue(TitleBarOnExitProperty, value);
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
