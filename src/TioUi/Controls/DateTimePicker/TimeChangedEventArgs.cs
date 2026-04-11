using Avalonia.Interactivity;

namespace TioUi.Controls;

public class TimeChangedEventArgs : RoutedEventArgs
{
    public TimeChangedEventArgs(TimeSpan? oldTime, TimeSpan? newTime)
    {
        this.OldTime = oldTime;
        this.NewTime = newTime;
    }

    public TimeSpan? OldTime { get; }

    public TimeSpan? NewTime { get; }
}