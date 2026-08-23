using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;

namespace TioUi.Controls;

public sealed class PopupContentExpander : ContentExpander
{
    private static readonly TimeSpan DefaultDuration = TimeSpan.FromMilliseconds(180);
    private static readonly TimeSpan MenuDuration = TimeSpan.FromMilliseconds(260);
    private static readonly TimeSpan FadeDuration = TimeSpan.FromMilliseconds(75);

    private int _animationVersion;

    public PopupContentExpander()
    {
        Orientation = Avalonia.Layout.Orientation.Vertical;
        Multiplier = 0;
        Opacity = 0;
        ClipToBounds = true;
        PreserveDesiredSize = true;
    }

    protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e)
    {
        base.OnAttachedToVisualTree(e);

        var animationVersion = ++_animationVersion;
        Dispatcher.UIThread.Post(() =>
        {
            if (animationVersion != _animationVersion) return;
            var isMenu = Content is Avalonia.Controls.ContextMenu or MenuFlyoutPresenter ||
                         this.GetVisualDescendants().Any(control =>
                             control is Avalonia.Controls.ContextMenu or MenuFlyoutPresenter);
            Transitions = CreateTransitions(isMenu ? MenuDuration : DefaultDuration);
            Multiplier = 1;
            Opacity = 1;
        }, DispatcherPriority.Render);
    }

    protected override void OnDetachedFromVisualTree(VisualTreeAttachmentEventArgs e)
    {
        _animationVersion++;
        base.OnDetachedFromVisualTree(e);
    }

    private static Transitions CreateTransitions(TimeSpan expandDuration) =>
    [
        new DoubleTransition
        {
            Property = MultiplierProperty,
            Duration = expandDuration,
            Easing = new Avalonia.Animation.Easings.ExponentialEaseOut()
        },
        new DoubleTransition
        {
            Property = OpacityProperty,
            Duration = FadeDuration,
            Easing = new Avalonia.Animation.Easings.QuadraticEaseOut()
        }
    ];
}
