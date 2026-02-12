using System.Collections.ObjectModel;

namespace YikUi.Common.Classes;

internal class ReadonlyDisposableCollection(IList<IDisposable?> list)
    : ReadOnlyCollection<IDisposable?>(list), IDisposable
{
    private readonly IList<IDisposable?> _list = list;

    public void Dispose()
    {
        foreach (var item in _list) item?.Dispose();
    }
}