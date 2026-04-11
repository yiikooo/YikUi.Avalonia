using Avalonia.Interactivity;

namespace TioUi.Controls;

public class CalendarDayButtonEventArgs(DateTime? date) : RoutedEventArgs
{
    public DateTime? Date { get; private set; } = date;
}