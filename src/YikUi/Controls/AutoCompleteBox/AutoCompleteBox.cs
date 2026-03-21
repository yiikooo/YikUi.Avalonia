using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;
using YikUi.Common.Contracts;
using YikUi.Common.Helpers;

namespace YikUi.Controls;

public class AutoCompleteBox : Avalonia.Controls.AutoCompleteBox, IClearControl
{
    // ReSharper disable once InconsistentNaming
    private const string PART_TextBox = "PART_TextBox";
    private bool _closeBySelectionFlag;

    private TextBox? _textbox;

    static AutoCompleteBox()
    {
        MinimumPrefixLengthProperty.OverrideDefaultValue<AutoCompleteBox>(0);
    }

    public AutoCompleteBox()
    {
        AddHandler(PointerReleasedEvent, OnCurrentPointerReleased!, RoutingStrategies.Tunnel);
    }

    public void Clear()
    {
        // Note: this method only resets Text to null. 
        // By default, AutoCompleteBox will clear the SelectedItem when Text is set to null.
        // But user can use custom Predicate to control the behavior when Text is set to null.
        SetCurrentValue(TextProperty, null);
    }

    private void OnCurrentPointerReleased(object sender, PointerReleasedEventArgs e)
    {
        var source = (e.Source as Control).FindAncestorOfType<ListBoxItem>();
        if (source is not null)
        {
            _closeBySelectionFlag = true;
        }
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);
        _textbox?.RemoveHandler(PointerPressedEvent, OnBoxPointerPressed);
        _textbox = e.NameScope.Find<TextBox>(PART_TextBox);
        _textbox?.AddHandler(PointerPressedEvent, OnBoxPointerPressed, handledEventsToo: true);
        PseudoClasses.Set(PseudoClassName.PC_Empty, string.IsNullOrEmpty(Text));
    }

    private void OnBoxPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (Equals(sender, _textbox)
            && e.GetCurrentPoint(this).Properties.IsLeftButtonPressed
            && IsDropDownOpen == false)
        {
            SetCurrentValue(IsDropDownOpenProperty, true);
        }
    }

    protected void OnGotFocus(RoutedEventArgs e)
    {
        // If the focus is set by keyboard navigation, open the dropdown.
        if (!_closeBySelectionFlag && IsDropDownOpen == false)
        {
            SetCurrentValue(IsDropDownOpenProperty, true);
        }
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);
        if (change.Property == TextProperty)
        {
            var value = change.GetNewValue<string?>();
            PseudoClasses.Set(PseudoClassName.PC_Empty, string.IsNullOrEmpty(value));
        }
    }
}