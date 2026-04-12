using Avalonia.Controls;
using Avalonia.Controls.Primitives;

namespace TioUi.Controls;

public class TioView : ContentControl
{
    public const string PART_DialogHost = "PART_DialogHost";
    protected override Type StyleKeyOverride => typeof(TioView);

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        var host = e.NameScope.Find<OverlayDialogHost>(PART_DialogHost);
        if (host is not null) LogicalChildren.Add(host);
    }
}