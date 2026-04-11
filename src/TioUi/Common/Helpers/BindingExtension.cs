using Avalonia;
using Avalonia.Data;
using TioUi.Common.Classes;

namespace TioUi.Common.Helpers;

public static class BindingExtension
{
    public static ResultDisposable TryBind(this AvaloniaObject obj, AvaloniaProperty property, BindingBase? binding)
    {
        if (binding is null) return new ResultDisposable(new EmptyDisposable(), false);
        return new ResultDisposable(obj.Bind(property, binding), true);
    }
}