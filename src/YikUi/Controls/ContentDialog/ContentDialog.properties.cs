using System.Windows.Input;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Templates;

namespace YikUi.Controls;

[PseudoClasses(PC_Hidden, PC_Open)]
[PseudoClasses(PC_Primary, PC_Secondary, PC_Close)]
[PseudoClasses(PC_FullSize)]
[TemplatePart(PART_PrimaryButton, typeof(Button))]
[TemplatePart(PART_SecondaryButton, typeof(Button))]
[TemplatePart(PART_CloseButton, typeof(Button))]
public partial class ContentDialog
{
    // TemplatePart names
    public const string PART_PrimaryButton = "PART_PrimaryButton";
    public const string PART_SecondaryButton = "PART_SecondaryButton";
    public const string PART_CloseButton = "PART_CloseButton";

    // PseudoClass names
    public const string PC_Hidden = ":hidden";
    public const string PC_Open = ":open";
    public const string PC_Primary = ":primary";
    public const string PC_Secondary = ":secondary";
    public const string PC_Close = ":close";
    public const string PC_FullSize = ":fullsize";

    /// <summary>定义 <see cref="Title"/> 属性</summary>
    public static readonly StyledProperty<object?> TitleProperty =
        AvaloniaProperty.Register<ContentDialog, object?>(nameof(Title), "");

    /// <summary>定义 <see cref="TitleTemplate"/> 属性</summary>
    public static readonly StyledProperty<IDataTemplate?> TitleTemplateProperty =
        AvaloniaProperty.Register<ContentDialog, IDataTemplate?>(nameof(TitleTemplate));

    /// <summary>定义 <see cref="PrimaryButtonText"/> 属性</summary>
    public static readonly StyledProperty<string?> PrimaryButtonTextProperty =
        AvaloniaProperty.Register<ContentDialog, string?>(nameof(PrimaryButtonText));

    /// <summary>定义 <see cref="SecondaryButtonText"/> 属性</summary>
    public static readonly StyledProperty<string?> SecondaryButtonTextProperty =
        AvaloniaProperty.Register<ContentDialog, string?>(nameof(SecondaryButtonText));

    /// <summary>定义 <see cref="CloseButtonText"/> 属性</summary>
    public static readonly StyledProperty<string?> CloseButtonTextProperty =
        AvaloniaProperty.Register<ContentDialog, string?>(nameof(CloseButtonText));

    /// <summary>定义 <see cref="PrimaryButtonCommand"/> 属性</summary>
    public static readonly StyledProperty<ICommand?> PrimaryButtonCommandProperty =
        AvaloniaProperty.Register<ContentDialog, ICommand?>(nameof(PrimaryButtonCommand));

    /// <summary>定义 <see cref="PrimaryButtonCommandParameter"/> 属性</summary>
    public static readonly StyledProperty<object?> PrimaryButtonCommandParameterProperty =
        AvaloniaProperty.Register<ContentDialog, object?>(nameof(PrimaryButtonCommandParameter));

    /// <summary>定义 <see cref="SecondaryButtonCommand"/> 属性</summary>
    public static readonly StyledProperty<ICommand?> SecondaryButtonCommandProperty =
        AvaloniaProperty.Register<ContentDialog, ICommand?>(nameof(SecondaryButtonCommand));

    /// <summary>定义 <see cref="SecondaryButtonCommandParameter"/> 属性</summary>
    public static readonly StyledProperty<object?> SecondaryButtonCommandParameterProperty =
        AvaloniaProperty.Register<ContentDialog, object?>(nameof(SecondaryButtonCommandParameter));

    /// <summary>定义 <see cref="CloseButtonCommand"/> 属性</summary>
    public static readonly StyledProperty<ICommand?> CloseButtonCommandProperty =
        AvaloniaProperty.Register<ContentDialog, ICommand?>(nameof(CloseButtonCommand));

    /// <summary>定义 <see cref="CloseButtonCommandParameter"/> 属性</summary>
    public static readonly StyledProperty<object?> CloseButtonCommandParameterProperty =
        AvaloniaProperty.Register<ContentDialog, object?>(nameof(CloseButtonCommandParameter));

    /// <summary>定义 <see cref="IsPrimaryButtonEnabled"/> 属性</summary>
    public static readonly StyledProperty<bool> IsPrimaryButtonEnabledProperty =
        AvaloniaProperty.Register<ContentDialog, bool>(nameof(IsPrimaryButtonEnabled), true);

    /// <summary>定义 <see cref="IsSecondaryButtonEnabled"/> 属性</summary>
    public static readonly StyledProperty<bool> IsSecondaryButtonEnabledProperty =
        AvaloniaProperty.Register<ContentDialog, bool>(nameof(IsSecondaryButtonEnabled), true);

    /// <summary>定义 <see cref="DefaultButton"/> 属性</summary>
    public static readonly StyledProperty<ContentDialogButton> DefaultButtonProperty =
        AvaloniaProperty.Register<ContentDialog, ContentDialogButton>(nameof(DefaultButton), ContentDialogButton.None);

    /// <summary>定义 <see cref="FullSizeDesired"/> 属性</summary>
    public static readonly StyledProperty<bool> FullSizeDesiredProperty =
        AvaloniaProperty.Register<ContentDialog, bool>(nameof(FullSizeDesired));

    /// <summary>获取或设置对话框标题</summary>
    public object? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    /// <summary>获取或设置标题的数据模板</summary>
    public IDataTemplate? TitleTemplate
    {
        get => GetValue(TitleTemplateProperty);
        set => SetValue(TitleTemplateProperty, value);
    }

    /// <summary>获取或设置主要按钮的文本</summary>
    public string? PrimaryButtonText
    {
        get => GetValue(PrimaryButtonTextProperty);
        set => SetValue(PrimaryButtonTextProperty, value);
    }

    /// <summary>获取或设置次要按钮的文本</summary>
    public string? SecondaryButtonText
    {
        get => GetValue(SecondaryButtonTextProperty);
        set => SetValue(SecondaryButtonTextProperty, value);
    }

    /// <summary>获取或设置关闭按钮的文本</summary>
    public string? CloseButtonText
    {
        get => GetValue(CloseButtonTextProperty);
        set => SetValue(CloseButtonTextProperty, value);
    }

    /// <summary>获取或设置主要按钮的命令</summary>
    public ICommand? PrimaryButtonCommand
    {
        get => GetValue(PrimaryButtonCommandProperty);
        set => SetValue(PrimaryButtonCommandProperty, value);
    }

    /// <summary>获取或设置主要按钮命令的参数</summary>
    public object? PrimaryButtonCommandParameter
    {
        get => GetValue(PrimaryButtonCommandParameterProperty);
        set => SetValue(PrimaryButtonCommandParameterProperty, value);
    }

    /// <summary>获取或设置次要按钮的命令</summary>
    public ICommand? SecondaryButtonCommand
    {
        get => GetValue(SecondaryButtonCommandProperty);
        set => SetValue(SecondaryButtonCommandProperty, value);
    }

    /// <summary>获取或设置次要按钮命令的参数</summary>
    public object? SecondaryButtonCommandParameter
    {
        get => GetValue(SecondaryButtonCommandParameterProperty);
        set => SetValue(SecondaryButtonCommandParameterProperty, value);
    }

    /// <summary>获取或设置关闭按钮的命令</summary>
    public ICommand? CloseButtonCommand
    {
        get => GetValue(CloseButtonCommandProperty);
        set => SetValue(CloseButtonCommandProperty, value);
    }

    /// <summary>获取或设置关闭按钮命令的参数</summary>
    public object? CloseButtonCommandParameter
    {
        get => GetValue(CloseButtonCommandParameterProperty);
        set => SetValue(CloseButtonCommandParameterProperty, value);
    }

    /// <summary>获取或设置主要按钮是否可用</summary>
    public bool IsPrimaryButtonEnabled
    {
        get => GetValue(IsPrimaryButtonEnabledProperty);
        set => SetValue(IsPrimaryButtonEnabledProperty, value);
    }

    /// <summary>获取或设置次要按钮是否可用</summary>
    public bool IsSecondaryButtonEnabled
    {
        get => GetValue(IsSecondaryButtonEnabledProperty);
        set => SetValue(IsSecondaryButtonEnabledProperty, value);
    }

    /// <summary>获取或设置对话框的默认按钮</summary>
    public ContentDialogButton DefaultButton
    {
        get => GetValue(DefaultButtonProperty);
        set => SetValue(DefaultButtonProperty, value);
    }

    /// <summary>获取或设置是否以全尺寸显示对话框</summary>
    public bool FullSizeDesired
    {
        get => GetValue(FullSizeDesiredProperty);
        set => SetValue(FullSizeDesiredProperty, value);
    }

    /// <summary>在对话框打开之前触发</summary>
    public event EventHandler<EventArgs>? Opening;

    /// <summary>在对话框完全打开后触发</summary>
    public event EventHandler<EventArgs>? Opened;

    /// <summary>在对话框开始关闭时（关闭之前）触发</summary>
    public event EventHandler<ContentDialogClosingEventArgs>? Closing;

    /// <summary>在对话框关闭后触发</summary>
    public event EventHandler<ContentDialogClosedEventArgs>? Closed;

    /// <summary>在主要按钮被点击后触发</summary>
    public event EventHandler<ContentDialogButtonClickEventArgs>? PrimaryButtonClick;

    /// <summary>在次要按钮被点击后触发</summary>
    public event EventHandler<ContentDialogButtonClickEventArgs>? SecondaryButtonClick;

    /// <summary>在关闭按钮被点击后触发</summary>
    public event EventHandler<ContentDialogButtonClickEventArgs>? CloseButtonClick;
}