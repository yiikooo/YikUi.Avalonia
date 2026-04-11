# 主题设置指南

TioUi 提供了简单易用的主题管理器 `ThemeManager`，让你可以轻松设置和切换主题色。

## 快速开始

### 在 XAML 中设置

最简单的方式是在 `App.axaml` 中直接设置：

```xaml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:tio="https://github.com/yiikooo/TioUi.Avalonia"
             x:Class="YourApp.App">
    <Application.Styles>
        <tio:TioUiTheme ThemeColor="#1890ff" />
    </Application.Styles>
</Application>
```

### 在代码中设置

使用 `ThemeManager` 可以在代码中轻松设置主题色：

```csharp
using TioUi.Common.Helpers;

public class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        // 使用预定义颜色
        ThemeManager.SetThemeColor(Colors.CornflowerBlue);

        // 或使用十六进制颜色值
        ThemeManager.SetThemeColor("#1890ff");

        base.OnFrameworkInitializationCompleted();
    }
}
```

## 运行时切换主题色

你可以在应用运行时动态切换主题色，UI 会自动更新：

```csharp
using TioUi.Common.Helpers;

// 切换到蓝色
ThemeManager.SetThemeColor("#1890ff");

// 切换到绿色
ThemeManager.SetThemeColor("#52c41a");

// 切换到红色
ThemeManager.SetThemeColor("#f5222d");

// 使用 Avalonia 预定义颜色
ThemeManager.SetThemeColor(Colors.Purple);
```

## 获取当前主题色

```csharp
using TioUi.Common.Helpers;

Color currentColor = ThemeManager.GetCurrentColor();
```

## 常用主题色

以下是一些常用的主题色供参考：

```csharp
// 蓝色系
ThemeManager.SetThemeColor("#1890ff"); // 默认蓝
ThemeManager.SetThemeColor("#096dd9"); // 深蓝
ThemeManager.SetThemeColor("#40a9ff"); // 浅蓝

// 绿色系
ThemeManager.SetThemeColor("#52c41a"); // 绿色
ThemeManager.SetThemeColor("#389e0d"); // 深绿
ThemeManager.SetThemeColor("#73d13d"); // 浅绿

// 红色系
ThemeManager.SetThemeColor("#f5222d"); // 红色
ThemeManager.SetThemeColor("#cf1322"); // 深红
ThemeManager.SetThemeColor("#ff4d4f"); // 浅红

// 橙色系
ThemeManager.SetThemeColor("#fa8c16"); // 橙色
ThemeManager.SetThemeColor("#d46b08"); // 深橙
ThemeManager.SetThemeColor("#ffa940"); // 浅橙

// 紫色系
ThemeManager.SetThemeColor("#722ed1"); // 紫色
ThemeManager.SetThemeColor("#531dab"); // 深紫
ThemeManager.SetThemeColor("#9254de"); // 浅紫
```

## 在 ViewModel 中使用

```csharp
using System.ComponentModel;
using Avalonia.Media;
using TioUi.Common.Helpers;

public class SettingsViewModel : INotifyPropertyChanged
{
    private Color _themeColor = Color.Parse("#1890ff");

    public Color ThemeColor
    {
        get => _themeColor;
        set
        {
            if (_themeColor == value) return;
            _themeColor = value;
            ThemeManager.SetThemeColor(value);
            OnPropertyChanged(nameof(ThemeColor));
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
```

## 主题色变化通知

`ThemeManager` 实现了 `INotifyPropertyChanged` 接口，你可以订阅主题色变化事件：

```csharp
using TioUi.Common.Helpers;

ThemeManager.Instance.PropertyChanged += (sender, e) =>
{
    if (e.PropertyName == nameof(ThemeManager.CurrentColor))
    {
        var newColor = ThemeManager.GetCurrentColor();
        // 处理主题色变化
    }
};
```

## 与语言设置配合使用

`ThemeManager` 的设计与 `LangManager` 保持一致，可以方便地同时管理主题和语言：

```csharp
using TioUi.Common.Helpers;
using TioUi.Common.Language;

public class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        // 设置语言
        LangManager.SetLanguage(Languages.zh_cn);

        // 设置主题色
        ThemeManager.SetThemeColor("#1890ff");

        base.OnFrameworkInitializationCompleted();
    }
}
```

## 注意事项

1. 主题色会自动生成多个色阶（浅色和深色变体），用于不同的 UI 状态
2. 主题色变化会立即应用到所有使用主题色的控件
3. 建议在应用启动时设置默认主题色，避免使用默认值
4. 主题色支持任何有效的颜色值，包括十六进制、RGB、命名颜色等
