using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using TioUi.Common;
using TioUi.Common.Helpers;

namespace TioUi.Controls;

public class IconDropDownButton : DropDownButton
{
    public static readonly StyledProperty<object?> IconProperty =
        IconButton.IconProperty.AddOwner<IconDropDownButton>();

    public static readonly StyledProperty<IDataTemplate?> IconTemplateProperty =
        IconButton.IconTemplateProperty.AddOwner<IconDropDownButton>();

    public static readonly StyledProperty<bool> IsLoadingProperty =
        IconButton.IsLoadingProperty.AddOwner<IconDropDownButton>();

    public static readonly StyledProperty<Position> IconPlacementProperty =
        IconButton.IconPlacementProperty.AddOwner<IconDropDownButton>();

    static IconDropDownButton()
    {
        ReversibleStackPanelUtils.EnsureBugFixed();
    }

    public object? Icon
    {
        get => GetValue(IconProperty);
        set => SetValue(IconProperty, value);
    }

    public IDataTemplate? IconTemplate
    {
        get => GetValue(IconTemplateProperty);
        set => SetValue(IconTemplateProperty, value);
    }

    public bool IsLoading
    {
        get => GetValue(IsLoadingProperty);
        set => SetValue(IsLoadingProperty, value);
    }

    public Position IconPlacement
    {
        get => GetValue(IconPlacementProperty);
        set => SetValue(IconPlacementProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        IconButton.UpdatePseudoClasses(this);
    }
}