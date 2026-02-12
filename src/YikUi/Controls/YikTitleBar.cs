using System.Diagnostics;
using System.Runtime.InteropServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.VisualTree;

namespace YikUi.Controls;

public class YikTitleBar : TemplatedControl
{
    private readonly List<Action> _disposeActions = new();
    private Win32Properties.CustomWndProcHookCallback? _wndProcHookCallback;
    private Button? _closeButton;
    private Button? _maximizeButton;
    private Button? _minimizeButton;
    private Grid? _moveDragArea;
    private DateTime? _lastClickTime;

    public static readonly StyledProperty<string?> TitleProperty =
        AvaloniaProperty.Register<YikTitleBar, string?>(nameof(Title));

    public static readonly StyledProperty<object?> LeftContentProperty =
        AvaloniaProperty.Register<YikTitleBar, object?>(nameof(LeftContent));

    public static readonly StyledProperty<bool> IsCloseBtnExitAppProperty =
        AvaloniaProperty.Register<YikTitleBar, bool>(nameof(IsCloseBtnExitApp));

    public static readonly StyledProperty<bool> IsCloseBtnHideWindowProperty =
        AvaloniaProperty.Register<YikTitleBar, bool>(nameof(IsCloseBtnHideWindow));

    public static readonly StyledProperty<bool> IsCloseBtnShowProperty =
        AvaloniaProperty.Register<YikTitleBar, bool>(nameof(IsCloseBtnShow), defaultValue: true);

    public static readonly StyledProperty<bool> IsMaxBtnShowProperty =
        AvaloniaProperty.Register<YikTitleBar, bool>(nameof(IsMaxBtnShow), defaultValue: true);

    public static readonly StyledProperty<bool> IsMinBtnShowProperty =
        AvaloniaProperty.Register<YikTitleBar, bool>(nameof(IsMinBtnShow), defaultValue: true);

    public static readonly StyledProperty<Action?> ExitActionProperty =
        AvaloniaProperty.Register<YikTitleBar, Action?>(nameof(ExitAction));

    public string? Title
    {
        get => GetValue(TitleProperty);
        set => SetValue(TitleProperty, value);
    }

    public object? LeftContent
    {
        get => GetValue(LeftContentProperty);
        set => SetValue(LeftContentProperty, value);
    }

    public bool IsCloseBtnExitApp
    {
        get => GetValue(IsCloseBtnExitAppProperty);
        set => SetValue(IsCloseBtnExitAppProperty, value);
    }

    public bool IsCloseBtnHideWindow
    {
        get => GetValue(IsCloseBtnHideWindowProperty);
        set => SetValue(IsCloseBtnHideWindowProperty, value);
    }

    public bool IsCloseBtnShow
    {
        get => GetValue(IsCloseBtnShowProperty);
        set => SetValue(IsCloseBtnShowProperty, value);
    }

    public bool IsMaxBtnShow
    {
        get => GetValue(IsMaxBtnShowProperty);
        set => SetValue(IsMaxBtnShowProperty, value);
    }

    public bool IsMinBtnShow
    {
        get => GetValue(IsMinBtnShowProperty);
        set => SetValue(IsMinBtnShowProperty, value);
    }

    public Action? ExitAction
    {
        get => GetValue(ExitActionProperty);
        set => SetValue(ExitActionProperty, value);
    }

    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        // Unsubscribe old events
        if (_closeButton != null)
            _closeButton.Click -= CloseButton_Click;
        if (_maximizeButton != null)
            _maximizeButton.Click -= MaximizeButton_Click;
        if (_minimizeButton != null)
            _minimizeButton.Click -= MinimizeButton_Click;
        if (_moveDragArea != null)
            _moveDragArea.PointerPressed -= MoveDragArea_PointerPressed;

        // Get template parts
        _closeButton = e.NameScope.Find<Button>("CloseButton");
        _maximizeButton = e.NameScope.Find<Button>("MaximizeButton");
        _minimizeButton = e.NameScope.Find<Button>("MinimizeButton");
        _moveDragArea = e.NameScope.Find<Grid>("MoveDragArea");

        // Subscribe to events
        if (_closeButton != null)
            _closeButton.Click += CloseButton_Click;
        if (_maximizeButton != null)
            _maximizeButton.Click += MaximizeButton_Click;
        if (_minimizeButton != null)
            _minimizeButton.Click += MinimizeButton_Click;
        if (_moveDragArea != null)
            _moveDragArea.PointerPressed += MoveDragArea_PointerPressed;

        // Enable Windows Snap Layout for maximize button
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) && _maximizeButton != null)
        {
            EnableWindowsSnapLayout(_maximizeButton);
        }
    }

    private void MoveDragArea_PointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (e.Pointer.Type == PointerType.Mouse)
        {
            if (sender is Grid control)
            {
                var window = control.GetVisualRoot() as Window;
                window.BeginMoveDrag(e);
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
            ExitAction?.Invoke();
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
                // Cleanup event handlers
                if (_closeButton != null)
                    _closeButton.Click -= CloseButton_Click;
                if (_maximizeButton != null)
                    _maximizeButton.Click -= MaximizeButton_Click;
                if (_minimizeButton != null)
                    _minimizeButton.Click -= MinimizeButton_Click;
                if (_moveDragArea != null)
                    _moveDragArea.PointerPressed -= MoveDragArea_PointerPressed;

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