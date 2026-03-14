using Avalonia.Controls.Primitives;
using YikUi.Common.Interfaces;

namespace YikUi.Controls;

public class CustomDrawerControl : DrawerControlBase
{
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        if (_closeButton is not null)
        {
            _closeButton.IsVisible = IsCloseButtonVisible ?? true;
        }
    }

    public override void Close()
    {
        if (DataContext is IDialogContext context)
        {
            context.Close();
        }
        else
        {
            OnElementClosing(this, null);
        }
    }

    protected internal override void AnchorAndUpdatePositionInfo()
    {
        // throw new NotImplementedException();
    }
}