using Avalonia;
using Avalonia.Animation;
using Avalonia.Animation.Easings;
using Avalonia.Input;
using Avalonia.Styling;
using Avalonia.VisualTree;
using YikUi.Common;
using YikUi.Common.Classes;
using YikUi.Common.Helpers;

namespace YikUi.Controls.Overlay;

public partial class OverlayDialogHost
{
    internal async void AddDrawer(DrawerControlBase control)
    {
        PureRectangle? mask = null;
        if (control.CanLightDismiss) mask = CreateOverlayMask(false, true);

        _layers.Add(new DialogPair(mask, control));
        ResetZIndices();
        control.MaxWidth = Math.Min(control.MaxWidth, Bounds.Width);
        control.MaxHeight = Math.Min(control.MaxHeight, Bounds.Height);
        if (mask is not null) Children.Add(mask);
        Children.Add(control);
        control.Measure(Bounds.Size);
        control.Arrange(new Rect(control.DesiredSize));
        SetDrawerPosition(control);
        control.AddHandler(OverlayFeedbackElement.ClosedEvent, OnDrawerControlClosing);
        var animation = CreateAnimation(control.Bounds.Size, control.Position);
        if (IsAnimationDisabled)
        {
            ResetDrawerPosition(control, Bounds.Size);
        }
        else
        {
            if (mask is null)
                await animation.RunAsync(control);
            else
                await Task.WhenAll(animation.RunAsync(control), MaskAppearAnimation.RunAsync(mask));
        }
    }

    internal async void AddModalDrawer(DrawerControlBase control)
    {
        var mask = CreateOverlayMask(true, control.CanLightDismiss);
        control.MaxWidth = Math.Min(control.MaxWidth, Bounds.Width);
        control.MaxHeight = Math.Min(control.MaxHeight, Bounds.Height);
        _layers.Add(new DialogPair(mask, control));
        Children.Add(mask);
        Children.Add(control);
        ResetZIndices();
        control.Measure(Bounds.Size);
        control.Arrange(new Rect(control.DesiredSize));
        SetDrawerPosition(control);
        _modalCount++;
        IsInModalStatus = _modalCount > 0;
        control.AddHandler(OverlayFeedbackElement.ClosedEvent, OnDrawerControlClosing);
        var animation = CreateAnimation(control.Bounds.Size, control.Position);
        if (IsAnimationDisabled)
            ResetDrawerPosition(control, Bounds.Size);
        else
            await Task.WhenAll(animation.RunAsync(control), MaskAppearAnimation.RunAsync(mask));

        var element = control.GetVisualDescendants().OfType<InputElement>()
            .FirstOrDefault(FocusHelper.GetDialogFocusHint);
        if (element is null)
            element = control.GetVisualDescendants().OfType<InputElement>().FirstOrDefault(a => a.Focusable);

        element?.Focus();
    }

    private void SetDrawerPosition(DrawerControlBase control)
    {
        if (control.Position is Position.Left or Position.Right) control.Height = Bounds.Height;

        if (control.Position is Position.Top or Position.Bottom) control.Width = Bounds.Width;
    }

    private static void ResetDrawerPosition(DrawerControlBase control, Size newSize)
    {
        control.MaxWidth = newSize.Width;
        control.MaxHeight = newSize.Height;
        if (control.Position == Position.Right)
        {
            control.Height = newSize.Height;
            SetLeft(control, newSize.Width - control.Bounds.Width);
        }
        else if (control.Position == Position.Left)
        {
            control.Height = newSize.Height;
            SetLeft(control, 0);
        }
        else if (control.Position == Position.Top)
        {
            control.Width = newSize.Width;
            SetTop(control, 0);
        }
        else
        {
            control.Width = newSize.Width;
            SetTop(control, newSize.Height - control.Bounds.Height);
        }
    }

    private Animation CreateAnimation(Size elementBounds, Position position, bool appear = true)
    {
        // left or top.
        double source = 0;
        double target = 0;
        if (position == Position.Left)
        {
            source = appear ? -elementBounds.Width : 0;
            target = appear ? 0 : -elementBounds.Width;
        }

        if (position == Position.Right)
        {
            source = appear ? Bounds.Width : Bounds.Width - elementBounds.Width;
            target = appear ? Bounds.Width - elementBounds.Width : Bounds.Width;
        }

        if (position == Position.Top)
        {
            source = appear ? -elementBounds.Height : 0;
            target = appear ? 0 : -elementBounds.Height;
        }

        if (position == Position.Bottom)
        {
            source = appear ? Bounds.Height : Bounds.Height - elementBounds.Height;
            target = appear ? Bounds.Height - elementBounds.Height : Bounds.Height;
        }

        var targetProperty = position == Position.Left || position == Position.Right
            ? LeftProperty
            : TopProperty;
        var animation = new Animation
        {
            Easing = new CubicEaseOut(),
            FillMode = FillMode.Forward
        };
        var keyFrame1 = new KeyFrame { Cue = new Cue(0.0) };
        keyFrame1.Setters.Add(new Setter
            { Property = targetProperty, Value = source });
        var keyFrame2 = new KeyFrame { Cue = new Cue(1.0) };
        keyFrame2.Setters.Add(new Setter
            { Property = targetProperty, Value = target });
        animation.Children.Add(keyFrame1);
        animation.Children.Add(keyFrame2);
        animation.Duration = TimeSpan.FromSeconds(0.3);
        return animation;
    }

    private async void OnDrawerControlClosing(object? sender, ResultEventArgs e)
    {
        if (sender is DrawerControlBase control)
        {
            if (control.IsShowAsync is false)
                e.Handled = true;

            var layer = _layers.FirstOrDefault(a => a.Element == control);
            if (layer is null) return;
            _layers.Remove(layer);
            control.RemoveHandler(OverlayFeedbackElement.ClosedEvent, OnDialogControlClosing);
            control.RemoveHandler(DialogControlBase.LayerChangedEvent, OnDialogLayerChanged);
            if (layer.Mask is not null)
            {
                _modalCount--;
                IsInModalStatus = _modalCount > 0;
                layer.Mask.RemoveHandler(PointerPressedEvent, ClickMaskToCloseDialog);
                layer.Mask.RemoveHandler(PointerReleasedEvent, DragMaskToMoveWindow);
                if (!IsAnimationDisabled)
                {
                    var disappearAnimation = CreateAnimation(control.Bounds.Size, control.Position, false);
                    await Task.WhenAll(disappearAnimation.RunAsync(control),
                        MaskDisappearAnimation.RunAsync(layer.Mask));
                }

                Children.Remove(layer.Mask);
            }
            else
            {
                if (!IsAnimationDisabled)
                {
                    var disappearAnimation = CreateAnimation(control.Bounds.Size, control.Position, false);
                    await disappearAnimation.RunAsync(control);
                }
            }

            Children.Remove(control);
            ResetZIndices();
        }
    }
}