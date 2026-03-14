using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace YikUi.Test;

public partial class Model : ObservableObject
{
    [ObservableProperty] private string? _selectedItem;

    public Model()
    {
        Items = new ObservableCollection<string>()
        {
            "Ding", "Otter", "Husky", "Mr. 17", "Cass"
        };
        SelectedItem = Items[0];
    }

    public ObservableCollection<string> Items { get; set; }

    public void Clear()
    {
        SelectedItem = null;
    }
}