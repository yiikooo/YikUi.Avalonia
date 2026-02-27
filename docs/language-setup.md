# YikUi 语言设置指南

YikUi 提供了灵活的语言设置系统，支持使用内置语言或创建自定义语言。

## 使用内置语言

YikUi 内置了以下语言：

- `Langs.zh_cn` - 简体中文
- `Langs.en_us` - 英语（美国）

### 方法 1：在 XAML 中设置

```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:yik="https://github.com/yiikooo/YikUi.Avalonia"
             x:Class="YourApp.App">
    <Application.Styles>
        <yik:YikUiTheme ThemeColor="#1890ff"
                        Language="zh_cn" />
    </Application.Styles>
</Application>
```

### 方法 2：在代码中设置

```csharp
using YikUi;
using YikUi.Common.Language;

// 方式 1：通过 YikUiTheme 实例
var theme = new YikUiTheme();
theme.SetLanguage(Langs.en_us);

// 方式 2：直接使用 LangManager（推荐）
LangManager.SetLanguage(Langs.zh_cn);
```

## 创建自定义语言

### 步骤 1：实现 ILang 接口

创建一个新的类并实现 `ILang` 接口：

```csharp
using YikUi.Common.Language;

namespace YourApp.Languages;

public class LangJaJp : ILang
{
    public string Desktop => "デスクトップ";
    public string Documents => "ドキュメント";
    public string Music => "ミュージック";
    public string Pictures => "ピクチャ";
    public string Videos => "ビデオ";
    public string Name => "名前";
    public string FileName => "ファイル名";
    public string UpdateAt => "更新日時";
    public string Type => "種類";
    public string Size => "サイズ";
    public string Cancel => "キャンセル";
    public string Confirm => "確認";
    public string ShowHiddenFiles => "隠しファイルを表示";
}
```

### 步骤 2：应用自定义语言

#### 在 XAML 中使用

```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             xmlns:yik="https://github.com/yiikooo/YikUi.Avalonia"
             xmlns:lang="clr-namespace:YourApp.Languages"
             x:Class="YourApp.App">
    <Application.Styles>
        <yik:YikUiTheme ThemeColor="#1890ff">
            <yik:YikUiTheme.CustomLanguage>
                <lang:LangJaJp />
            </yik:YikUiTheme.CustomLanguage>
        </yik:YikUiTheme>
    </Application.Styles>
</Application>
```

#### 在代码中使用

```csharp
using YikUi;
using YikUi.Common.Language;
using YourApp.Languages;

// 方式 1：通过 YikUiTheme 实例
var theme = new YikUiTheme();
theme.SetCustomLanguage(new LangJaJp());

// 方式 2：直接使用 LangManager（推荐）
LangManager.SetLanguage(new LangJaJp());
```

## 在组件中使用语言

在你的控件或视图模型中访问当前语言：

```csharp
using YikUi.Common.Language;

public class MyViewModel
{
    public void ShowMessage()
    {
        var currentLang = LangManager.Current;
        Console.WriteLine(currentLang.Confirm); // 输出：确认 / Confirm / 確認
    }
}
```

## 监听语言变化

如果需要在语言切换时更新 UI：

```csharp
using YikUi.Common.Language;

public class MyControl
{
    public MyControl()
    {
        LangManager.LanguageChanged += OnLanguageChanged;
    }

    private void OnLanguageChanged(object? sender, ILang newLang)
    {
        // 更新 UI 文本
        UpdateUITexts();
    }

    private void UpdateUITexts()
    {
        var lang = LangManager.Current;
        ConfirmButton.Content = lang.Confirm;
        CancelButton.Content = lang.Cancel;
    }
}
```

## 动态切换语言

```csharp
using YikUi.Common.Language;

public class LanguageSwitcher
{
    public void SwitchToEnglish()
    {
        LangManager.SetLanguage(Langs.en_us);
    }

    public void SwitchToChinese()
    {
        LangManager.SetLanguage(Langs.zh_cn);
    }

    public void SwitchToCustom()
    {
        LangManager.SetLanguage(new LangJaJp());
    }
}
```

## 扩展语言字段

如果内置的 `ILang` 接口字段不够用，可以创建扩展接口：

```csharp
using YikUi.Common.Language;

namespace YourApp.Languages;

public interface IExtendedLang : ILang
{
    string Settings { get; }
    string About { get; }
    string Help { get; }
}

public class ExtendedLangZhCn : IExtendedLang
{
    // 实现 ILang 的所有属性
    public string Desktop => "桌面";
    public string Documents => "文档";
    public string Music => "音乐";
    public string Pictures => "图片";
    public string Videos => "视频";
    public string Name => "名称";
    public string FileName => "文件名";
    public string UpdateAt => "修改日期";
    public string Type => "类型";
    public string Size => "大小";
    public string Cancel => "取消";
    public string Confirm => "确认";
    public string ShowHiddenFiles => "显示隐藏文件";

    // 扩展属性
    public string Settings => "设置";
    public string About => "关于";
    public string Help => "帮助";
}
```

使用扩展语言：

```csharp
// 设置语言
LangManager.SetLanguage(new ExtendedLangZhCn());

// 使用时需要类型转换
if (LangManager.Current is IExtendedLang extLang)
{
    Console.WriteLine(extLang.Settings);
}
```

## 最佳实践

1. **优先使用 LangManager**：直接使用 `LangManager.SetLanguage()` 而不是通过 `YikUiTheme`，这样更灵活。

2. **在应用启动时设置语言**：在 `App.axaml.cs` 的构造函数中设置默认语言。

3. **持久化语言设置**：将用户选择的语言保存到配置文件，下次启动时恢复。

4. **订阅语言变化事件**：对于需要动态更新的 UI，订阅 `LangManager.LanguageChanged` 事件。

5. **提供语言切换 UI**：在设置页面提供语言选择下拉框，方便用户切换。

## 示例：完整的语言切换功能

```csharp
using System;
using Avalonia.Controls;
using YikUi.Common.Language;

namespace YourApp.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();

        // 初始化语言选择器
        LanguageComboBox.Items = new[] { "简体中文", "English" };
        LanguageComboBox.SelectedIndex = 0;

        LanguageComboBox.SelectionChanged += OnLanguageSelectionChanged;
    }

    private void OnLanguageSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var selectedIndex = LanguageComboBox.SelectedIndex;

        switch (selectedIndex)
        {
            case 0:
                LangManager.SetLanguage(Langs.zh_cn);
                break;
            case 1:
                LangManager.SetLanguage(Langs.en_us);
                break;
        }
    }
}
```

---

更多信息请访问 [YikUi GitHub](https://github.com/yiikooo/YikUi.Avalonia)
