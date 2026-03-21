using Avalonia.Interactivity;
using YikUi.Common.Classes;

namespace YikUi.Controls;

public class DialogLayerChangeEventArgs : RoutedEventArgs
{
    public DialogLayerChangeEventArgs(DialogLayerChangeType type)
    {
        ChangeType = type;
    }

    public DialogLayerChangeEventArgs(RoutedEvent routedEvent, DialogLayerChangeType type) : base(routedEvent)
    {
        ChangeType = type;
    }

    public DialogLayerChangeType ChangeType { get; }
}