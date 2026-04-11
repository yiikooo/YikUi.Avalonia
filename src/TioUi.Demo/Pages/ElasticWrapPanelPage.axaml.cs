using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Layout;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class ElasticWrapPanelPage : UserControl
{
    public ElasticWrapPanelPage()
    {
        InitializeComponent();
        DataContext = new ElasticWrapPanelDemoViewModel();
    }
}

public partial class ElasticWrapPanelDemoViewModel : ObservableObject
{
    [ObservableProperty] private bool _autoHeight = true;

    [ObservableProperty] private bool _autoWidth = true;

    [ObservableProperty] private HorizontalAlignment _cmbHAlign = HorizontalAlignment.Left;
    [ObservableProperty] private VerticalAlignment _cmbVAlign = VerticalAlignment.Stretch;
    [ObservableProperty] private ScrollBarVisibility _horizontalVisibility = ScrollBarVisibility.Auto;

    [ObservableProperty] private bool _isFillHorizontal;
    [ObservableProperty] private bool _isFillVertical;
    [ObservableProperty] private double _itemHeight = 40d;
    [ObservableProperty] private double _itemSelfHeight = double.NaN;
    [ObservableProperty] private double _itemSelfWidth = double.NaN;
    [ObservableProperty] private double _itemSpacing;
    [ObservableProperty] private double _itemWidth = 40d;
    [ObservableProperty] private double _lineSpacing;
    private double _oldItemSelfHeight;

    private double _oldItemSelfWidth;
    [ObservableProperty] private Orientation _selectedOrientation = Orientation.Horizontal;
    [ObservableProperty] private ScrollBarVisibility _verticalVisibility = ScrollBarVisibility.Auto;

    partial void OnAutoWidthChanged(bool value)
    {
        if (value)
        {
            _oldItemSelfWidth = ItemSelfWidth;
            ItemSelfWidth = double.NaN;
        }
        else
        {
            ItemSelfWidth = _oldItemSelfWidth;
        }
    }

    partial void OnAutoHeightChanged(bool value)
    {
        if (value)
        {
            _oldItemSelfHeight = ItemSelfHeight;
            ItemSelfHeight = double.NaN;
        }
        else
        {
            ItemSelfHeight = _oldItemSelfHeight;
        }
    }
}