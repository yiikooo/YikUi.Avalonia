using Android.App;
using Android.Runtime;
using Avalonia.Android;

namespace TioUi.Demo.Android;

[Application]
public class AndroidApp : AvaloniaAndroidApplication<TioUi.Demo.App>
{
    protected AndroidApp(nint javaReference, JniHandleOwnership transfer)
        : base(javaReference, transfer)
    {
    }
}