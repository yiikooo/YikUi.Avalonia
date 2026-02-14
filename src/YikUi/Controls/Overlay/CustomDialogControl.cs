using Avalonia.Controls.Primitives;
using YikUi.Common.Helpers;
using YikUi.Common.Interfaces;

namespace YikUi.Controls;

public class CustomDialogControl : DialogControlBase
{
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var closeButtonVisible = IsCloseButtonVisible ?? DataContext is IDialogContext;
        IsHitTestVisibleProperty.SetValue(closeButtonVisible, _closeButton);
        if (!closeButtonVisible) OpacityProperty.SetValue(0, _closeButton);
    }

    public override void Close()
    {
        if (DataContext is IDialogContext context)
            context.Close();
        else
            OnElementClosing(this, null);
    }
}