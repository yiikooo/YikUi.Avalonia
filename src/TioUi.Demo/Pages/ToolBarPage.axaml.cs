using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Templates;
using Avalonia.Data;
using CommunityToolkit.Mvvm.ComponentModel;
using TioUi.Controls;

namespace TioUi.Demo.Pages;

public partial class ToolBarPage : UserControl
{
    public ToolBarPage()
    {
        InitializeComponent();
        DataContext = new ToolBarDemoViewModel();
    }
}

public class ToolBarItemTemplateSelector : IDataTemplate
{
    public static ToolBarItemTemplateSelector Instance { get; } = new();

    public Control? Build(object? param)
    {
        if (param is null) return null;
        if (param is ToolBarSeparatorViewModel)
        {
            return new ToolBarSeparator();
        }

        if (param is ToolBarButtonItemViewModel)
        {
            return new Button()
            {
                [!ContentControl.ContentProperty] = new Binding() { Path = "Content" },
                [!Button.CommandProperty] = new Binding() { Path = "Command" },
                [!ToolBar.OverflowModeProperty] = new Binding() { Path = nameof(ToolBarItemViewModel.OverflowMode) },
            };
        }

        if (param is ToolBarCheckBoxItemViewModel)
        {
            return new CheckBox()
            {
                [!ContentControl.ContentProperty] = new Binding() { Path = "Content" },
                [!ToggleButton.IsCheckedProperty] = new Binding() { Path = "IsChecked" },
                [!ToolBar.OverflowModeProperty] = new Binding() { Path = nameof(ToolBarItemViewModel.OverflowMode) },
            };
        }

        if (param is ToolBarComboBoxItemViewModel)
        {
            return new ComboBox()
            {
                [!ContentControl.ContentProperty] = new Binding() { Path = "Content" },
                [!SelectingItemsControl.SelectedItemProperty] = new Binding() { Path = "SelectedItem" },
                [!ItemsControl.ItemsSourceProperty] = new Binding() { Path = "Items" },
                [!ToolBar.OverflowModeProperty] = new Binding() { Path = nameof(ToolBarItemViewModel.OverflowMode) },
            };
        }

        return new Button() { Content = "Undefined Item" };
    }

    public bool Match(object? data)
    {
        return data is ToolBarItemViewModel;
    }
}

public partial class ToolBarDemoViewModel : ObservableObject
{
    public ToolBarDemoViewModel()
    {
        Items = new()
        {
            new ToolBarButtonItemViewModel { Content = "New", OverflowMode = OverflowMode.AsNeeded },
            new ToolBarButtonItemViewModel { Content = "Open" },
            new ToolBarButtonItemViewModel { Content = "Save1" },
            new ToolBarButtonItemViewModel { Content = "Save2" },
            new ToolBarSeparatorViewModel(),
            new ToolBarButtonItemViewModel { Content = "Save3" },
            new ToolBarButtonItemViewModel { Content = "Save4" },
            new ToolBarButtonItemViewModel { Content = "Save5" },
            new ToolBarButtonItemViewModel { Content = "Save6" },
            new ToolBarButtonItemViewModel { Content = "Save7" },
            new ToolBarSeparatorViewModel(),
            new ToolBarButtonItemViewModel { Content = "Save8" },
            new ToolBarCheckBoxItemViewModel { Content = "Bold" },
            new ToolBarCheckBoxItemViewModel { Content = "Italic", OverflowMode = OverflowMode.Never },
            new ToolBarComboBoxItemViewModel { Content = "Font Size", Items = ["10", "12", "14"] }
        };
    }

    public ObservableCollection<ToolBarItemViewModel> Items { get; set; }
}

public abstract class ToolBarItemViewModel : ObservableObject
{
    public OverflowMode OverflowMode { get; set; }
}

public class ToolBarButtonItemViewModel : ToolBarItemViewModel
{
    public string? Content { get; set; }
}

public class ToolBarCheckBoxItemViewModel : ToolBarItemViewModel
{
    public string? Content { get; set; }
    public bool IsChecked { get; set; }
}

public class ToolBarComboBoxItemViewModel : ToolBarItemViewModel
{
    private string? _selectedItem;
    public string? Content { get; set; }
    public ObservableCollection<string>? Items { get; set; }

    public string? SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }
}

public class ToolBarSeparatorViewModel : ToolBarItemViewModel
{
}