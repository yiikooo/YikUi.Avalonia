using System.Collections.ObjectModel;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;

namespace TioUi.Demo.Pages;

public partial class TagInputPage : UserControl
{
    public TagInputPage()
    {
        InitializeComponent();
        DataContext = new TagInputDemoViewModel();
    }
}

public class TagInputDemoViewModel : ObservableObject
{
    private ObservableCollection<string> _distinctTags = new();
    private ObservableCollection<string> _tags = new();

    public ObservableCollection<string> Tags
    {
        get => _tags;
        set => SetProperty(ref _tags, value);
    }

    public ObservableCollection<string> DistinctTags
    {
        get => _distinctTags;
        set => SetProperty(ref _distinctTags, value);
    }
}