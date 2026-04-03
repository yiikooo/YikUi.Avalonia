using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using YikUi.Common;
using YikUi.Common.Helpers;

namespace YikUi.Controls;

public partial class MacOsTestWindow : YikWindow
{
    public MacOsTestWindow()
    {
        AvaloniaXamlLoader.Load(this);

        // 如果是 macOS 系统，应用窗口按钮位置调整
        if (Data.DesktopType == DesktopType.MacOs)
        {
            PropertyChanged += OnWindowPropertyChanged;
        }
    }

    public double XCoordinate { get; set; } = 20;
    public double YCoordinate { get; set; } = -4;
    public double ButtonSpacing { get; set; } = 20;

    private void OnWindowPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        // 当窗口可见时应用设置
        if (e.Property.Name != "IsVisible") return;
        if (GetValue(e.Property) is not true) return;

        ApplyMacOsWindowSettings();
    }

    private void ApplyMacOsWindowSettings()
    {
        if (Data.DesktopType != DesktopType.MacOs) return;

        try
        {
            var platform = TryGetPlatformHandle();
            if (platform is null) return;

            var nsWindow = platform.Handle;
            if (nsWindow == IntPtr.Zero) return;

            MacOsWindowHandler.RefreshTitleBarButtonPosition(nsWindow, XCoordinate, YCoordinate, ButtonSpacing);

            // 更新参数显示
            UpdateParametersDisplay();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"应用 macOS 窗口设置失败: {ex.Message}");
        }
    }

    protected override void OnOpened(EventArgs e)
    {
        base.OnOpened(e);
        UpdateParametersDisplay();
    }

    private void UpdateParametersDisplay()
    {
        var parametersText = this.FindControl<TextBlock>("ParametersText");
        if (parametersText != null)
        {
            parametersText.Text = $"X 坐标: {XCoordinate}\n" +
                                  $"Y 坐标: {YCoordinate}\n" +
                                  $"按钮间距: {ButtonSpacing}\n" +
                                  $"当前系统: {Data.DesktopType}";
        }
    }

    private void CloseWindow_Click(object? sender, RoutedEventArgs e)
    {
        Close();
    }
}