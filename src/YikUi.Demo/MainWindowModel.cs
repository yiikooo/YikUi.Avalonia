using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Controls;
using YikUi.Demo.Pages;

namespace YikUi.Demo;

public sealed class MainWindowModel : INotifyPropertyChanged
{
    public MainWindowModel()
    {
        SelectedPage = Pages[0];
        foreach (var page in Pages)
        {
            if (page.Children is { Count: > 0 })
            {
                page.Children = page.Children.OrderBy(child => child.Title).ToList();
            }
        }
    }

    public Page SelectedPage
    {
        get;
        set => SetField(ref field, value);
    }

    public static List<Page> Pages { get; set; } =
    [
        new()
        {
            Title = "Overview",
            Content = new OverviewPage()
        },
        new()
        {
            Title = "Setting",
            Content = new SettingPage()
        },
        new()
        {
            Title = "Basic",
            Children =
            [
                new Page
                {
                    Title = "Label",
                    Content = new LabelPage(),
                },
                new Page
                {
                    Title = "TextBlock",
                    Content = new TextBoxPage(),
                },
                new Page
                {
                    Title = "SelectableTextBlock",
                    Content = new SelectableTextBlockPage(),
                },
            ]
        },
        new()
        {
            Title = "Button",
            Children =
            [
                new Page
                {
                    Title = "Button",
                    Content = new ButtonPage(),
                },
                new Page
                {
                    Title = "ToggleSwitch",
                    Content = new ToggleSwitchPage(),
                },
                new Page
                {
                    Title = "RadioButton",
                    Content = new RadioButtonPage(),
                },
                new Page
                {
                    Title = "HyperlinkButton",
                    Content = new HyperlinkButtonPage(),
                },
                new Page
                {
                    Title = "ToggleButton",
                    Content = new ToggleButtonPage(),
                },
                new Page
                {
                    Title = "CheckBox",
                    Content = new CheckBoxPage(),
                },
            ]
        },
        new()
        {
            Title = "Input",
            Children =
            [
                new Page
                {
                    Title = "ComboBox",
                    Content = new ComboBoxPage(),
                },
                new Page
                {
                    Title = "TextBox",
                    Content = new TextBoxPage(),
                },
                new Page
                {
                    Title = "AutoCompleteBox",
                    Content = new AutoCompleteBoxPage(),
                },
                new Page
                {
                    Title = "ButtonSpinnerPage",
                    Content = new ButtonSpinnerPage(),
                },
                new Page
                {
                    Title = "NumericUpDown",
                    Content = new NumericUpDownPage(),
                },
                new Page
                {
                    Title = "Slider",
                    Content = new SliderPage(),
                },
                new Page
                {
                    Title = "ManagedFileChooser",
                    Content = new ManagedFileChooserPage(),
                },
            ]
        },
        new()
        {
            Title = "Menu",
            Children =
            [
                new Page
                {
                    Title = "Menu",
                    Content = new MenuPage(),
                },
                new Page
                {
                    Title = "NavMenu",
                    Content = new NavMenuPage(),
                },
            ]
        },
        new()
        {
            Title = "Time",
            Children =
            [
                new Page
                {
                    Title = "Calendar",
                    Content = new CalendarPage(),
                },
                new Page
                {
                    Title = "CalendarDatePicker",
                    Content = new CalendarDatePickerPage(),
                },
                new Page
                {
                    Title = "DatePicker",
                    Content = new DatePickerPage(),
                },
                new Page
                {
                    Title = "TimePicker",
                    Content = new TimePickerPage(),
                },
            ]
        },
        new()
        {
            Title = "Show",
            Children =
            [
                new Page
                {
                    Title = "Flyout",
                    Content = new FlyoutPage(),
                },
                new Page
                {
                    Title = "ToolTip",
                    Content = new ToolTipPage(),
                },
                new Page
                {
                    Title = "ProgressBar",
                    Content = new ProgressBarPage(),
                },
                new Page
                {
                    Title = "Expander",
                    Content = new ExpanderPage(),
                },
                new Page
                {
                    Title = "ListBox",
                    Content = new ListBoxPage(),
                },
                new Page
                {
                    Title = "Carousel",
                    Content = new CarouselPage(),
                },
            ]
        },
        new()
        {
            Title = "Feedback",
            Children =
            [
                new Page
                {
                    Title = "DataValidationErrors",
                    Content = new DataValidationErrorsPage(),
                },
                new Page
                {
                    Title = "RefreshContainer",
                    Content = new RefreshContainerPage(),
                }
            ]
        },
        new()
        {
            Title = "Layout",
            Children =
            [
                new Page
                {
                    Title = "GroupBorder",
                    Content = new GroupBorderPage(),
                },
                new Page
                {
                    Title = "GridSplitter",
                    Content = new GridSplitterPage(),
                },
                new Page
                {
                    Title = "ScrollViewer",
                    Content = new ScrollViewerPage(),
                },
                new Page
                {
                    Title = "ThemeVariantScope",
                    Content = new ThemeVariantScopePage(),
                },
                new Page
                {
                    Title = "HeaderedContent",
                    Content = new HeaderedContentPage(),
                },
            ]
        }
    ];

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private void SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return;
        field = value;
        OnPropertyChanged(propertyName);
    }
}

public class Page
{
    public string Title { get; set; }
    public string Icon { get; set; }
    public UserControl Content { get; set; }
    public List<Page> Children { get; set; }
}