using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Styling;
using Avalonia.VisualTree;
using YikUi.Common.Classes;
using YikUi.Common.Helpers;

namespace YikUi.Controls;

public partial class OverlayDialogHost : Canvas
{
    private static readonly Animation MaskAppearAnimation;
    private static readonly Animation MaskDisappearAnimation;

    public static readonly AttachedProperty<bool> IsModalStatusScopeProperty =
        AvaloniaProperty.RegisterAttached<OverlayDialogHost, Control, bool>("IsModalStatusScope");

    public static readonly AttachedProperty<bool> IsInModalStatusProperty =
        AvaloniaProperty.RegisterAttached<OverlayDialogHost, Control, bool>(nameof(IsInModalStatus));

    public static readonly StyledProperty<bool> IsModalStatusReporterProperty =
        AvaloniaProperty.Register<OverlayDialogHost, bool>(
            nameof(IsModalStatusReporter));

    public static readonly StyledProperty<IBrush?> OverlayMaskBrushProperty =
        AvaloniaProperty.Register<OverlayDialogHost, IBrush?>(
            nameof(OverlayMaskBrush));

    private readonly List<DialogPair> _layers = new(10);

    private int _modalCount;
    private IDisposable? _modalStatusSubscription;
    private int? _toplevelHash;

    static OverlayDialogHost()
    {
        ClipToBoundsProperty.OverrideDefaultValue<OverlayDialogHost>(true);
        MaskAppearAnimation = CreateOpacityAnimation(true);
        MaskDisappearAnimation = CreateOpacityAnimation(false);
    }

    public bool IsModalStatusReporter
    {
        get => GetValue(IsModalStatusReporterProperty);
        set => SetValue(IsModalStatusReporterProperty, value);
    }

    public bool IsInModalStatus
    {
        get => GetValue(IsInModalStatusProperty);
        set => SetValue(IsInModalStatusProperty, value);
    }

    public bool IsAnimationDisabled { get; set; }
    public bool IsTopLevel { get; set; }

    public string? HostId { get; set; }

    public DataTemplates DialogDataTemplates { get; set; } = new();

    public IBrush? OverlayMaskBrush
    {
        get => GetValue(OverlayMaskBrushProperty);
        set => SetValue(OverlayMaskBrushProperty, value);
    }

    public static void SetIsModalStatusScope(Control obj, bool value)
    {
        obj.SetValue(IsModalStatusScopeProperty, value);
    }

    internal static bool GetIsModalStatusScope(Control obj)
    {
        return obj.GetValue(IsModalStatusScopeProperty);
    }

    internal static void SetIsInModalStatus(Control obj, bool value)
    {
        obj.SetValue(IsInModalStatusProperty, value);
    }

    public static bool GetIsInModalStatus(Control obj)
    {
        return obj.GetValue(IsInModalStatusProperty);
    }

    private static Animation CreateOpacityAnimation(bool appear)
    {
        var animation = new Animation
        {
            FillMode = FillMode.Forward
        };
        var keyFrame1 = new KeyFrame { Cue = new Cue(0.0) };
        keyFrame1.Setters.Add(new Setter { Property = OpacityProperty, Value = appear ? 0.0 : 1.0 });
        var keyFrame2 = new KeyFrame { Cue = new Cue(1.0) };
        keyFrame2.Setters.Add(new Setter { Property = OpacityProperty, Value = appear ? 1.0 : 0.0 });
        animation.Children.Add(keyFrame1);
        animation.Children.Add(keyFrame2);
        animation.Duration = TimeSpan.FromSeconds(0.2);
        return animation;
    }

    private PureRectangle CreateOverlayMask(bool modal, bool canCloseOnClick)
    {
        PureRectangle rec = new()
        {
            Width = Bounds.Width,
            Height = Bounds.Height,
            IsVisible = true
        };
        if (modal)
            rec[!PureRectangle.BackgroundProperty] = this[!OverlayMaskBrushProperty];
        else if (canCloseOnClick) rec.SetCurrentValue(PureRectangle.BackgroundProperty, Brushes.Transparent);

        if (canCloseOnClick)
            rec.AddHandler(PointerReleasedEvent, ClickMaskToCloseDialog);
        else if (IsTopLevel) rec.AddHandler(PointerPressedEvent, DragMaskToMoveWindow);

        return rec;
    }

    private void DragMaskToMoveWindow(object? sender, PointerPressedEventArgs e)
    {
        if (!e.GetCurrentPoint(this).Properties.IsLeftButtonPressed) return;

        if (sender is not PureRectangle mask) return;
        if (TopLevel.GetTopLevel(mask) is Window window) window.BeginMoveDrag(e);
    }

    private void ClickMaskToCloseDialog(object? sender, PointerReleasedEventArgs e)
    {
        if (sender is not PureRectangle border) return;
        var layer = _layers.FirstOrDefault(a => a.Mask == border);
        if (layer is null) return;
        border.RemoveHandler(PointerReleasedEvent, ClickMaskToCloseDialog);
        border.RemoveHandler(PointerPressedEvent, DragMaskToMoveWindow);
        layer.Element.Close();
    }

    protected sealed override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);
        _toplevelHash = TopLevel.GetTopLevel(this)?.GetHashCode();
        var modalHost = this.GetVisualAncestors().OfType<Control>().FirstOrDefault(GetIsModalStatusScope);
        if (modalHost is not null)
            _modalStatusSubscription = ObservableExtension
                .Subscribe(this.GetObservable(IsInModalStatusProperty), a =>
                {
                    if (IsModalStatusReporter) SetIsInModalStatus(modalHost, a);
                });

        OverlayDialogManager.RegisterHost(this, HostId, _toplevelHash);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        while (_layers.Count > 0) _layers[0].Element.Close();

        _modalStatusSubscription?.Dispose();
        OverlayDialogManager.UnregisterHost(HostId, _toplevelHash);
        base.OnDetachedFromVisualTree(e);
    }


    protected sealed override void OnSizeChanged(SizeChangedEventArgs e)
    {
        base.OnSizeChanged(e);
        for (var i = 0; i < _layers.Count; i++)
        {
            if (_layers[i].Mask is { } rect)
            {
                rect.Width = Bounds.Width;
                rect.Height = Bounds.Height;
            }

            if (_layers[i].Element is DialogControlBase d)
                ResetDialogPosition(d, e.NewSize);
            else if (_layers[i].Element is DrawerControlBase drawer) ResetDrawerPosition(drawer, e.NewSize);
        }
    }

    private void ResetZIndices()
    {
        var index = 0;
        for (var i = 0; i < _layers.Count; i++)
        {
            if (_layers[i].Mask is { } mask)
            {
                mask.ZIndex = index;
                index++;
            }

            if (_layers[i].Element is { } dialog)
            {
                dialog.ZIndex = index;
                index++;
            }
        }
    }

    internal IDataTemplate? GetDataTemplate(object? o)
    {
        if (o is null) return null;
        var templates = DialogDataTemplates;
        var result = templates.FirstOrDefault(a => a.Match(o));
        if (result != null) return result;
        var keys = Resources.Keys;
        foreach (var key in keys)
            if (Resources.TryGetValue(key, out var value) && value is IDataTemplate t)
            {
                result = t;
                break;
            }

        return result;
    }

    internal T? Recall<T>()
    {
        var element = _layers.LastOrDefault(a => a.Element.Content?.GetType() == typeof(T));
        return element?.Element.Content is T t ? t : default;
    }

    private class DialogPair(PureRectangle? mask, OverlayFeedbackElement element, bool modal = true)
    {
        internal readonly OverlayFeedbackElement Element = element;
        internal readonly PureRectangle? Mask = mask;
        internal readonly bool Modal = modal;
    }
}