using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace YikUi.Controls;

public partial class YikTitleBar : UserControl , INotifyPropertyChanged
{
    private readonly List<Action> _disposeActions = [];
    private Win32Properties.CustomWndProcHookCallback? _wndProcHookCallback;

    public YikTitleBar()
    {
        InitializeComponent();
        DataContext = this;
        CloseButton.Click += CloseButton_Click;
        MaximizeButton.Click += MaximizeButton_Click;
        MinimizeButton.Click += MinimizeButton_Click;
        MoveDragArea.PointerPressed += MoveDragArea_PointerPressed;

        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            AttachedToVisualTree += (s, e) =>
            {
                Debug.WriteLine("YikTitleBar: AttachedToVisualTree event fired");
                EnableWindowsSnapLayout(MaximizeButton);
            };
        }
    }
    
    public string Title
    {
        get;
        set => SetField(ref field, value);
    }

    public object LeftContent
    {
        get;
        set => SetField(ref field, value);
    }

    public bool IsCloseBtnExitApp
    {
        get;
        set => SetField(ref field, value);
    }

    public bool IsCloseBtnHideWindow
    {
        get;
        set => SetField(ref field, value);
    }

    public bool IsCloseBtnShow
    {
        get;
        set => SetField(ref field, value);
    } = true;

    public bool IsMaxBtnShow
    {
        get;
        set => SetField(ref field, value);
    } = true;

    public bool IsMinBtnShow
    {
        get;
        set => SetField(ref field, value);
    } = true;

    public DateTime? lastClickTime { get; set; }
    public Action? OnExit { get; set; }


    private void MoveDragArea_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Pointer.Type == PointerType.Mouse)
        {
            if (sender is Grid control)
            {
                var window = control.GetVisualRoot() as Window;
                window.BeginMoveDrag(e);
            }

            if (IsMaxBtnShow && lastClickTime.HasValue && (DateTime.Now - lastClickTime.Value).TotalMilliseconds < 300)
            {
                lastClickTime = null;
                if (this.GetVisualRoot() is Window window)
                    window.WindowState = window.WindowState == WindowState.Maximized
                        ? WindowState.Normal
                        : WindowState.Maximized;
            }
            else
            {
                lastClickTime = DateTime.Now;
            }

            e.Handled = true;
        }
    }

    private void MinimizeButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;
        if (button.GetVisualRoot() is Window window) window.WindowState = WindowState.Minimized;
    }

    private void MaximizeButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;
        if (button.GetVisualRoot() is not Window window) return;
        window.WindowState = window.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
    }

    private void CloseButton_Click(object? sender, RoutedEventArgs e)
    {
        if (sender is not Button button) return;
        if (IsCloseBtnExitApp)
        {
            OnExit.Invoke();
        }
        else
        {
            if (button.GetVisualRoot() is not Window window) return;
            if (IsCloseBtnHideWindow)
            {
                window.Hide();
            }
            else
            {
                CloseButton.Click -= CloseButton_Click;
                MaximizeButton.Click -= MaximizeButton_Click;
                MinimizeButton.Click -= MinimizeButton_Click;
                MoveDragArea.PointerPressed -= MoveDragArea_PointerPressed;

                // Execute disposal actions (including Win32 hook cleanup)
                foreach (var disposeAction in _disposeActions)
                {
                    try
                    {
                        disposeAction.Invoke();
                    }
                    catch
                    {
                        // Ignore disposal errors
                    }
                }

                _disposeActions.Clear();

                window.Close();
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }
        }
    }

    #region Windows Snap Layout Support

    [DllImport("user32.dll")]
    private static extern short GetAsyncKeyState(int vKey);

    private static bool IsMouseDown()
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return false;

        const int VK_LBUTTON = 1;
        return (GetAsyncKeyState(VK_LBUTTON) & 0x8000) != 0;
    }

    private void EnableWindowsSnapLayout(Button maximizeButton)
    {
        if (!RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            return;

        const int HTCLIENT = 1;
        const int HTMAXBUTTON = 9;
        const uint WM_NCHITTEST = 0x0084;

        var pointerOnButton = false;
        var pointerOverSetter = typeof(Button).GetProperty(nameof(IsPointerOver));
        if (pointerOverSetter is null)
        {
            Debug.WriteLine("YikTitleBar: IsPointerOver property not found");
            return;
        }

        var window = this.GetVisualRoot() as Window;
        if (window == null)
        {
            Debug.WriteLine("YikTitleBar: Window not found");
            return;
        }

        Debug.WriteLine($"YikTitleBar: Enabling Snap Layout for button");

        nint ProcHookCallback(nint hWnd, uint msg, nint wParam, nint lParam, ref bool handled)
        {
            if (msg == WM_NCHITTEST)
            {
                if (!maximizeButton.IsVisible)
                    return 0;

                var point = new PixelPoint(
                    (short)(ToInt32(lParam) & 0xffff),
                    (short)(ToInt32(lParam) >> 16)
                );

                var buttonSize = maximizeButton.DesiredSize;
                var buttonLeftTop = maximizeButton.PointToScreen(new Point(0, 0));

                var scaling = window.RenderScaling;
                var x = (point.X - buttonLeftTop.X) / scaling;
                var y = (point.Y - buttonLeftTop.Y) / scaling;

                var isInButton = new Rect(default, buttonSize).Contains(new Point(x, y));

                if (isInButton)
                {
                    handled = true;

                    if (!pointerOnButton)
                    {
                        pointerOnButton = true;
                        pointerOverSetter.SetValue(maximizeButton, true);
                        Debug.WriteLine("YikTitleBar: Pointer entered maximize button");
                    }

                    var result = IsMouseDown() ? HTCLIENT : HTMAXBUTTON;
                    Debug.WriteLine($"YikTitleBar: Returning {(result == HTMAXBUTTON ? "HTMAXBUTTON" : "HTCLIENT")}");
                    return result;
                }
                else
                {
                    if (pointerOnButton)
                    {
                        pointerOnButton = false;
                        pointerOverSetter.SetValue(maximizeButton, false);
                        Debug.WriteLine("YikTitleBar: Pointer left maximize button");
                    }
                }
            }

            return 0;
        }

        static int ToInt32(IntPtr ptr) =>
            IntPtr.Size == 4 ? ptr.ToInt32() : (int)(ptr.ToInt64() & 0xffffffff);

        try
        {
            _wndProcHookCallback = new Win32Properties.CustomWndProcHookCallback(ProcHookCallback);
            Win32Properties.AddWndProcHookCallback(window, _wndProcHookCallback);

            Debug.WriteLine("YikTitleBar: Win32 hook successfully registered");

            _disposeActions.Add(() =>
            {
                try
                {
                    if (_wndProcHookCallback != null)
                    {
                        Win32Properties.RemoveWndProcHookCallback(window, _wndProcHookCallback);
                        Debug.WriteLine("YikTitleBar: Win32 hook removed");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"YikTitleBar: Error removing hook: {ex.Message}");
                }
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"YikTitleBar: Failed to enable Windows Snap Layout: {ex.Message}");
            Debug.WriteLine($"YikTitleBar: Stack trace: {ex.StackTrace}");
        }
    }

    #endregion

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}