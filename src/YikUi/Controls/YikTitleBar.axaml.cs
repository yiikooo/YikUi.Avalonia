using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace YikUi.Controls;

public partial class YikTitleBar : UserControl
{
    private readonly List<Action> _disposeActions = new();
    private Win32Properties.CustomWndProcHookCallback? _wndProcHookCallback;
    private DateTime? _lastClickTime;

    public YikTitleBar()
    {
        InitializeComponent();
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

    #region Styled Properties

    public static readonly StyledProperty<string?> TitleProperty =
        AvaloniaProperty.Register<YikTitleBar, string?>(nameof(Title));

    public string? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public static readonly StyledProperty<object?> LeftContentProperty =
        AvaloniaProperty.Register<YikTitleBar, object?>(nameof(LeftContent));

    public object? LeftContent
    {
        get => GetValue(LeftContentProperty);
        set => SetValue(LeftContentProperty, value);
    }

    public static readonly StyledProperty<bool> IsCloseBtnExitAppProperty =
        AvaloniaProperty.Register<YikTitleBar, bool>(nameof(IsCloseBtnExitApp));

    public bool IsCloseBtnExitApp
    {
        get => GetValue(IsCloseBtnExitAppProperty);
        set => SetValue(IsCloseBtnExitAppProperty, value);
    }

    public static readonly StyledProperty<bool> IsCloseBtnHideWindowProperty =
        AvaloniaProperty.Register<YikTitleBar, bool>(nameof(IsCloseBtnHideWindow));

    public bool IsCloseBtnHideWindow
    {
        get => GetValue(IsCloseBtnHideWindowProperty);
        set => SetValue(IsCloseBtnHideWindowProperty, value);
    }

    public static readonly StyledProperty<bool> IsCloseBtnShowProperty =
        AvaloniaProperty.Register<YikTitleBar, bool>(nameof(IsCloseBtnShow), defaultValue: true);

    public bool IsCloseBtnShow
    {
        get => GetValue(IsCloseBtnShowProperty);
        set => SetValue(IsCloseBtnShowProperty, value);
    }

    public static readonly StyledProperty<bool> IsMaxBtnShowProperty =
        AvaloniaProperty.Register<YikTitleBar, bool>(nameof(IsMaxBtnShow), defaultValue: true);

    public bool IsMaxBtnShow
    {
        get => GetValue(IsMaxBtnShowProperty);
        set => SetValue(IsMaxBtnShowProperty, value);
    }

    public static readonly StyledProperty<bool> IsMinBtnShowProperty =
        AvaloniaProperty.Register<YikTitleBar, bool>(nameof(IsMinBtnShow), defaultValue: true);

    public bool IsMinBtnShow
    {
        get => GetValue(IsMinBtnShowProperty);
        set => SetValue(IsMinBtnShowProperty, value);
    }

    public static readonly StyledProperty<Action?> OnExitProperty =
        AvaloniaProperty.Register<YikTitleBar, Action?>(nameof(OnExit));

    public Action? OnExit
    {
        get => GetValue(OnExitProperty);
        set => SetValue(OnExitProperty, value);
    }

    #endregion


    private void MoveDragArea_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Pointer.Type == PointerType.Mouse)
        {
            if (sender is Grid control)
            {
                var window = control.GetVisualRoot() as Window;
                window?.BeginMoveDrag(e);
            }

            if (IsMaxBtnShow && _lastClickTime.HasValue && (DateTime.Now - _lastClickTime.Value).TotalMilliseconds < 300)
            {
                _lastClickTime = null;
                if (this.GetVisualRoot() is Window window)
                    window.WindowState = window.WindowState == WindowState.Maximized
                        ? WindowState.Normal
                        : WindowState.Maximized;
            }
            else
            {
                _lastClickTime = DateTime.Now;
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
            OnExit?.Invoke();
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
}