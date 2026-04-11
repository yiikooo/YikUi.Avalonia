using Avalonia;
using Avalonia.Controls.Primitives;

namespace TioUi.Controls;

public class TioDateTimeScrollPanel : DateTimePickerPanel
{
    protected override Size MeasureOverride(Size availableSize)
    {
        var size = base.MeasureOverride(availableSize);
        var width = this.Children.Select(a => a.DesiredSize.Width).Max();
        width = Math.Max(width, this.MinWidth);
        return new Size(width, size.Height);
    }

    protected override Size ArrangeOverride(Size finalSize)
    {
        var width = this.Children.Select(a => a.DesiredSize.Width).Max();
        width = Math.Max(width, this.MinWidth);
        finalSize = new Size(width, finalSize.Height);
        return base.ArrangeOverride(finalSize);
    }
}