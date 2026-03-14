using Avalonia.Interactivity;

namespace YikUi.Controls;

public class CalendarYearButtonEventArgs : RoutedEventArgs
{
    /// <inheritdoc />
    internal CalendarYearButtonEventArgs(CalendarViewMode mode, CalendarContext context)
    {
        Context = context;
        Mode = mode;
    }

    internal CalendarContext Context { get; }
    internal CalendarViewMode Mode { get; }
}