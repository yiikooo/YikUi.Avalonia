using Avalonia.Interactivity;

namespace YikUi.Controls;

public class CalendarDayButtonEventArgs(DateTime? date) : RoutedEventArgs
{
    public DateTime? Date { get; private set; } = date;
}