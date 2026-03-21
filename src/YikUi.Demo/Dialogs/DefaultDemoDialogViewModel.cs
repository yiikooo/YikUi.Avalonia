using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace YikUi.Demo.Dialogs;

public partial class DefaultDemoDialogViewModel : ObservableObject
{
    [ObservableProperty] private string? _city;
    [ObservableProperty] private string? _department;
    [ObservableProperty] private string? _owner;
    [ObservableProperty] private string? _target;

    public DefaultDemoDialogViewModel()
    {
        Cities =
        [
            "Shanghai", "Beijing", "Hulunbuir", "Shenzhen", "Hangzhou", "Nanjing", "Chengdu", "Wuhan", "Chongqing",
            "Suzhou", "Tianjin", "Xi'an", "Qingdao", "Dalian"
        ];
    }

    public ObservableCollection<string> Cities { get; set; }
}