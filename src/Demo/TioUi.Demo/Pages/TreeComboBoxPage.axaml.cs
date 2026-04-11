using System.Collections.Generic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class TreeComboBoxPage : UserControl
{
    public TreeComboBoxPage()
    {
        InitializeComponent();
        DataContext = new TreeComboBoxDemoViewModel();
    }
}

public partial class TreeComboBoxDemoViewModel : ObservableObject
{
    [ObservableProperty] private TreeComboBoxItemViewModel? _selectedItem;

    public TreeComboBoxDemoViewModel()
    {
        Items = new List<TreeComboBoxItemViewModel>()
        {
            new TreeComboBoxItemViewModel()
            {
                ItemName = "Item 1",
                Children = new List<TreeComboBoxItemViewModel>()
                {
                    new TreeComboBoxItemViewModel()
                    {
                        ItemName = "Item 1-1 (Not selectable)",
                        IsSelectable = false,
                        Children = new List<TreeComboBoxItemViewModel>()
                        {
                            new TreeComboBoxItemViewModel()
                            {
                                ItemName = "Item 1-1-1"
                            },
                            new TreeComboBoxItemViewModel()
                            {
                                ItemName = "Item 1-1-2"
                            }
                        }
                    },
                    new TreeComboBoxItemViewModel()
                    {
                        ItemName = "Item 1-2"
                    }
                }
            },
            new TreeComboBoxItemViewModel()
            {
                ItemName = "Item 2",
                Children = new List<TreeComboBoxItemViewModel>()
                {
                    new TreeComboBoxItemViewModel()
                    {
                        ItemName = "Item 2-1  (Not selectable)",
                        IsSelectable = false,
                    },
                    new TreeComboBoxItemViewModel()
                    {
                        ItemName = "Item 2-2"
                    }
                }
            },
            new TreeComboBoxItemViewModel()
            {
                ItemName = "Item 3"
            },
        };
    }

    public List<TreeComboBoxItemViewModel> Items { get; set; }
}

public partial class TreeComboBoxItemViewModel : ObservableObject
{
    [ObservableProperty] private bool _isSelectable = true;
    [ObservableProperty] private string? _itemName;
    public List<TreeComboBoxItemViewModel> Children { get; set; } = new();
}