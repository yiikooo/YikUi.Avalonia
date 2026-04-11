using System.Collections.ObjectModel;
using Avalonia.Controls;

namespace TioUi.Demo.Pages;

public partial class SplitViewPage : UserControl
{
    public SplitViewPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public ObservableCollection<string> Songs { get; set; } =
    [
        "Apple", "Banana", "Cherry", "Durian", "Elderberry", "Fig", "Grape", "Honeydew", "Ice cream", "Jackfruit",
        "Kiwi", "Lemon", "Mango", "Nectarine", "Orange", "Papaya", "Quince", "Raspberry", "Strawberry", "Tangerine",
        "Ugli fruit", "Vanilla", "Watermelon", "Yuzu", "Zucchini"
    ];

    public static ObservableCollection<SplitViewDisplayMode> DisplayModes { get; set; } =
    [
        SplitViewDisplayMode.Inline,
        SplitViewDisplayMode.CompactInline,
        SplitViewDisplayMode.Overlay,
        SplitViewDisplayMode.CompactOverlay,
    ];

    public static ObservableCollection<SplitViewPanePlacement> Placements { get; set; } =
    [
        SplitViewPanePlacement.Left,
        SplitViewPanePlacement.Right,
        SplitViewPanePlacement.Top,
        SplitViewPanePlacement.Bottom
    ];
}