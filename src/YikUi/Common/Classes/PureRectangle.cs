using Avalonia;
using Avalonia.Controls;
using Avalonia.Media;

namespace YikUi.Common.Classes;

/// <summary>
///     Provide the most simplified shape implementation. This is a rectangle with a background, without border and corner
///     radius.
/// </summary>
public class PureRectangle : Control
{
    public static readonly StyledProperty<IBrush?> BackgroundProperty =
        Border.BackgroundProperty.AddOwner<PureRectangle>();

    static PureRectangle()
    {
        AffectsRender<PureRectangle>(BackgroundProperty);
    }

    public IBrush? Background
    {
        get => GetValue(BackgroundProperty);
        set => SetValue(BackgroundProperty, value);
    }

    public override void Render(DrawingContext context)
    {
        base.Render(context);
        context.DrawRectangle(Background, null, new Rect(Bounds.Size));
    }
}