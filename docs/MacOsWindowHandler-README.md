# macOS 窗口按钮位置调整功能

## 概述

这是一个用于调整 macOS 窗口标题栏按钮（关闭、最小化、最大化）位置的功能模块。通过 Objective-C 运行时调用 macOS 原生 API，实现对窗口按钮位置的精确控制。

## 功能特性

- ✅ 调整窗口标题栏按钮的 X、Y 坐标
- ✅ 自定义按钮之间的间距
- ✅ 隐藏最大化（缩放）按钮
- ✅ 仅在 macOS 系统上生效，其他系统自动跳过
- ✅ 提供完整的 Demo 页面和测试窗口
- ✅ 支持运行时动态调整

## 快速开始

### 1. 在 Demo 中体验

运行 YikUi.Demo 项目，在左侧导航栏找到 "Platform" → "macOS Window"，即可进入演示页面。

详见：[Demo 使用指南](MacOsWindowHandler-Demo.md)

### 2. 在项目中使用

```csharp
using YikUi.Common;
using YikUi.Common.Helpers;

public class MyWindow : YikWindow
{
    public MyWindow()
    {
        InitializeComponent();

        if (Data.DesktopType == DesktopType.MacOs)
        {
            PropertyChanged += OnWindowPropertyChanged;
        }
    }

    private void OnWindowPropertyChanged(object? sender, AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Property.Name != "IsVisible") return;
        if (GetValue(e.Property) is not true) return;

        var platform = TryGetPlatformHandle();
        if (platform is null) return;
        var nsWindow = platform.Handle;
        if (nsWindow == IntPtr.Zero) return;

        MacOsWindowHandler.RefreshTitleBarButtonPosition(nsWindow, 20, -4, 20);
    }
}
```

详见：[快速开始指南](MacOsWindowHandler-QuickStart.md)

## 文档导航

- **[Demo 使用指南](MacOsWindowHandler-Demo.md)** - 如何在 Demo 中使用和测试
- **[快速开始](MacOsWindowHandler-QuickStart.md)** - 快速集成到你的项目
- **[详细使用文档](MacOsWindowHandler-Usage.md)** - 完整的 API 文档和高级用法

## 项目文件

### 核心功能

- `src/YikUi/Common/Helpers/MacOsWindowHandler.cs` - 核心实现

### Demo 页面

- `src/YikUi.Demo/Pages/MacOsWindowPage.axaml` - Demo 页面 XAML
- `src/YikUi.Demo/Pages/MacOsWindowPage.axaml.cs` - Demo 页面代码
- `src/YikUi.Demo/Navs/Platform.cs` - 导航配置

### 测试窗口

- `src/YikUi/Controls/Window/MacOsTestWindow.axaml` - 测试窗口 XAML
- `src/YikUi/Controls/Window/MacOsTestWindow.axaml.cs` - 测试窗口代码

### 文档

- `docs/MacOsWindowHandler-README.md` - 本文档
- `docs/MacOsWindowHandler-Demo.md` - Demo 使用指南
- `docs/MacOsWindowHandler-QuickStart.md` - 快速开始
- `docs/MacOsWindowHandler-Usage.md` - 详细使用文档

## API 概览

### MacOsWindowHandler

```csharp
// 调整按钮位置
public static void RefreshTitleBarButtonPosition(
    IntPtr nsWindow,
    double x = 20,      // X 坐标
    double y = -4,      // Y 坐标
    double spacing = 20 // 按钮间距
)

// 隐藏最大化按钮
public static void HideZoomButton(IntPtr nsWindow)
```

## 参数说明

| 参数    | 说明                       | 推荐范围  | 默认值 |
| ------- | -------------------------- | --------- | ------ |
| x       | 按钮距离窗口左边缘的距离   | 10 - 50   | 20     |
| y       | 按钮的垂直偏移（负值向上） | -20 到 20 | -4     |
| spacing | 三个按钮之间的水平间距     | 15 - 30   | 20     |

## 预设参数

| 名称     | X   | Y   | 间距 | 说明       |
| -------- | --- | --- | ---- | ---------- |
| 标准布局 | 20  | -4  | 20   | 默认推荐   |
| 紧凑布局 | 15  | -4  | 15   | 更紧凑     |
| 宽松布局 | 25  | -4  | 25   | 更宽松     |
| 向下偏移 | 20  | 0   | 20   | 不向上偏移 |

## 系统要求

- macOS 系统（功能仅在 macOS 上生效）
- Avalonia UI 框架
- .NET 6.0 或更高版本

## 注意事项

1. 此功能仅在 macOS 系统上生效
2. 必须在窗口句柄可用后调用
3. 建议先使用 Demo 页面测试合适的参数
4. 参数设置不当可能导致按钮超出窗口边界

## 许可证

遵循 YikUi 项目的许可证。

## 贡献

欢迎提交 Issue 和 Pull Request。

## 更新日志

### v1.0.0 (2024)

- ✨ 初始版本
- ✨ 支持调整按钮位置
- ✨ 支持隐藏最大化按钮
- ✨ 提供完整的 Demo 页面
- ✨ 提供测试窗口
- 📝 完整的文档
