using Avalonia.Reactive;

namespace YikUi.Common.Helpers;

public static class ObservableExtension
{
    public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> action)
    {
        return observable.Subscribe(new AnonymousObserver<T>(action));
    }
}