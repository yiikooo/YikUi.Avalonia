using Avalonia.Interactivity;

namespace TioUi.Controls;

public class PinCodeCompleteEventArgs(IList<string> code, RoutedEvent? @event) : RoutedEventArgs(@event)
{
    public IList<string> Code { get; } = code;
}