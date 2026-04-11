# TioUi 多语言使用指南

TioUi 提供了灵活的多语言系统，支持运行时动态切换语言，UI 自动更新。

## 快速开始

### 使用内置语言

TioUi 内置了以下语言：

- `Languages.zh_cn` - 简体中文
- `Languages.en_us` - 英语（美国）

在你的应用启动时设置语言：

```csharp
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using TioUi.Common.Language;

public class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        // 设置默认语言为中文
        LangManager.SetLanguage(Languages.zh_cn);

        // 或设置为英语
        // LangManager.SetLanguage(Languages.en_us);

        base.OnFrameworkInitializationCompleted();
    }
}
```

### 运行时切换语言

```csharp
using TioUi.Common.Language;

// 切换到英语
LangManager.SetLanguage(Languages.en_us);

// 切换到中文
LangManager.SetLanguage(Languages.zh_cn);

// UI 会自动更新，无需重启应用
```

## 自定义语言

如果内置语言不满足需求，你可以在自己的项目中创建自定义语言。

### 步骤 1：创建语言类

在你的项目中创建一个新类，实现 `ILang` 接口：

```csharp
using TioUi.Common.Language;

namespace YourApp.Languages;

public class LangJaJp : ILang
{
    public string Name => "名前";
    public string FileName => "ファイル名";
    public string UpdateAt => "更新日時";
    public string Type => "種類";
    public string Size => "サイズ";
    public string Cancel => "キャンセル";
    public string Confirm => "確認";
    public string ShowHiddenFiles => "隠しファイルを表示";
    public string FileAlreadyExists => "ファイルは既に存在します";
    public string Day => "日";
    public string Month => "月";
    public string Year => "年";
}
```

### 步骤 2：使用自定义语言

```csharp
using TioUi.Common.Language;
using YourApp.Languages;

// 在应用启动时设置
public class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        // 使用自定义语言
        LangManager.SetLanguage(new LangJaJp());

        base.OnFrameworkInitializationCompleted();
    }
}

// 或在运行时切换
public void SwitchToJapanese()
{
    LangManager.SetLanguage(new LangJaJp());
}
```

## ILang 接口说明

`ILang` 接口定义了 TioUi 组件使用的所有语言文本：

```csharp
public interface ILang
{
    string Name { get; }              // 文件管理器：名称列标题
    string FileName { get; }          // 文件管理器：文件名输入框水印
    string UpdateAt { get; }          // 文件管理器：修改日期列标题
    string Type { get; }              // 文件管理器：类型列标题
    string Size { get; }              // 文件管理器：大小列标题
    string Cancel { get; }            // 通用：取消按钮
    string Confirm { get; }           // 通用：确认按钮
    string ShowHiddenFiles { get; }   // 文件管理器：显示隐藏文件选项
    string FileAlreadyExists { get; } // 文件管理器：文件已存在提示
    string Day { get; }               // 日期选择器：日
    string Month { get; }             // 日期选择器：月
    string Year { get; }              // 日期选择器：年
}
```

## 完整示例：多语言切换

### 创建多个语言类

```csharp
// Languages/LangFrFr.cs - 法语
using TioUi.Common.Language;

namespace YourApp.Languages;

public class LangFrFr : ILang
{
    public string Name => "Nom";
    public string FileName => "Nom du fichier";
    public string UpdateAt => "Date de modification";
    public string Type => "Type";
    public string Size => "Taille";
    public string Cancel => "Annuler";
    public string Confirm => "Confirmer";
    public string ShowHiddenFiles => "Afficher les fichiers cachés";
    public string FileAlreadyExists => "Le fichier existe déjà";
    public string Day => "jour";
    public string Month => "mois";
    public string Year => "année";
}

// Languages/LangDeDE.cs - 德语
public class LangDeDe : ILang
{
    public string Name => "Name";
    public string FileName => "Dateiname";
    public string UpdateAt => "Änderungsdatum";
    public string Type => "Typ";
    public string Size => "Größe";
    public string Cancel => "Abbrechen";
    public string Confirm => "Bestätigen";
    public string ShowHiddenFiles => "Versteckte Dateien anzeigen";
    public string FileAlreadyExists => "Datei existiert bereits";
    public string Day => "Tag";
    public string Month => "Monat";
    public string Year => "Jahr";
}
```

### 创建语言切换器

```csharp
using Avalonia.Controls;
using TioUi.Common.Language;
using YourApp.Languages;

namespace YourApp.ViewModels;

public class LanguageSelectorViewModel
{
    public class LanguageOption
    {
        public string DisplayName { get; set; } = "";
        public ILang Language { get; set; } = null!;
    }

    public List<LanguageOption> AvailableLanguages { get; } = new()
    {
        new() { DisplayName = "简体中文", Language = new LangZhCn() },
        new() { DisplayName = "English", Language = new LangEnUs() },
        new() { DisplayName = "日本語", Language = new LangJaJp() },
        new() { DisplayName = "Français", Language = new LangFrFr() },
        new() { DisplayName = "Deutsch", Language = new LangDeDe() }
    };

    public void ChangeLanguage(LanguageOption option)
    {
        LangManager.SetLanguage(option.Language);
    }
}
```

### XAML 界面

```xml
<UserControl xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="YourApp.Views.SettingsView">
    <StackPanel Spacing="15" Margin="20">
        <TextBlock Text="Language / 语言" FontSize="16" FontWeight="Bold" />

        <ComboBox Name="LanguageComboBox"
                  ItemsSource="{Binding AvailableLanguages}"
                  DisplayMemberPath="DisplayName"
                  SelectedIndex="0"
                  SelectionChanged="OnLanguageChanged"
                  MinWidth="200" />

        <!-- 这些文本会随语言切换自动更新 -->
        <Border BorderBrush="Gray" BorderThickness="1" Padding="10" Margin="0,20,0,0">
            <StackPanel Spacing="10">
                <TextBlock Text="预览 / Preview:" FontWeight="Bold" />
                <Button Content="{DynamicResource Lang.Confirm}" />
                <Button Content="{DynamicResource Lang.Cancel}" />
                <TextBox Watermark="{DynamicResource Lang.FileName}" />
            </StackPanel>
        </Border>
    </StackPanel>
</UserControl>
```

### 代码后台

```csharp
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace YourApp.Views;

public partial class SettingsView : UserControl
{
    public SettingsView()
    {
        InitializeComponent();
    }

    private void OnLanguageChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (LanguageComboBox.SelectedItem is LanguageSelectorViewModel.LanguageOption option)
        {
            option.ChangeLanguage(option);
        }
    }
}
```

## 根据系统语言自动选择

```csharp
using System.Globalization;
using TioUi.Common.Language;
using YourApp.Languages;

public class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        // 根据系统语言自动选择
        var culture = CultureInfo.CurrentUICulture;

        if (culture.Name.StartsWith("zh"))
        {
            LangManager.SetLanguage(Languages.zh_cn);
        }
        else if (culture.Name.StartsWith("ja"))
        {
            LangManager.SetLanguage(new LangJaJp());
        }
        else if (culture.Name.StartsWith("fr"))
        {
            LangManager.SetLanguage(new LangFrFr());
        }
        else if (culture.Name.StartsWith("de"))
        {
            LangManager.SetLanguage(new LangDeDe());
        }
        else
        {
            // 默认使用英语
            LangManager.SetLanguage(Languages.en_us);
        }

        base.OnFrameworkInitializationCompleted();
    }
}
```

## 持久化语言设置

```csharp
using System.IO;
using System.Text.Json;
using TioUi.Common.Language;

public class LanguageSettings
{
    private const string SettingsFile = "language.json";

    public class Settings
    {
        public string LanguageCode { get; set; } = "zh_cn";
    }

    // 保存语言设置
    public static void SaveLanguage(string languageCode)
    {
        var settings = new Settings { LanguageCode = languageCode };
        var json = JsonSerializer.Serialize(settings);
        File.WriteAllText(SettingsFile, json);
    }

    // 加载语言设置
    public static string LoadLanguage()
    {
        if (!File.Exists(SettingsFile))
            return "zh_cn";

        var json = File.ReadAllText(SettingsFile);
        var settings = JsonSerializer.Deserialize<Settings>(json);
        return settings?.LanguageCode ?? "zh_cn";
    }

    // 应用保存的语言
    public static void ApplySavedLanguage()
    {
        var code = LoadLanguage();

        switch (code)
        {
            case "zh_cn":
                LangManager.SetLanguage(Languages.zh_cn);
                break;
            case "en_us":
                LangManager.SetLanguage(Languages.en_us);
                break;
            case "ja_jp":
                LangManager.SetLanguage(new LangJaJp());
                break;
            // 添加更多语言...
        }
    }
}

// 在 App.axaml.cs 中使用
public class App : Application
{
    public override void OnFrameworkInitializationCompleted()
    {
        // 加载并应用保存的语言
        LanguageSettings.ApplySavedLanguage();

        base.OnFrameworkInitializationCompleted();
    }
}

// 切换语言时保存
public void ChangeLanguage(string code)
{
    LanguageSettings.SaveLanguage(code);
    // 然后切换语言...
}
```

## 监听语言变化

如果你需要在语言切换时执行某些操作：

```csharp
using System.ComponentModel;
using TioUi.Common.Language;

public class MyViewModel
{
    public MyViewModel()
    {
        // 订阅语言变化事件
        LangManager.Instance.PropertyChanged += OnLanguageChanged;
    }

    private void OnLanguageChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(LangManager.Current))
        {
            // 语言已更改
            Console.WriteLine("Language changed!");

            // 执行你的自定义逻辑
            RefreshData();
            UpdateNotifications();
        }
    }
}
```

## 工作原理

TioUi 的多语言系统基于 Avalonia 的动态资源（DynamicResource）：

1. 当你调用 `LangManager.SetLanguage()` 时，系统会自动将 `ILang` 接口的所有属性注册为应用程序资源
2. 资源键格式为 `Lang.{PropertyName}`，例如 `Lang.Confirm`、`Lang.Cancel`
3. TioUi 的所有组件使用 `{DynamicResource Lang.xxx}` 引用这些资源
4. 当语言切换时，资源自动更新，UI 立即响应变化

## 注意事项

1. **必须实现所有属性**：自定义语言类必须实现 `ILang` 接口的所有属性
2. **在应用启动时设置**：建议在 `App.OnFrameworkInitializationCompleted()` 中设置默认语言
3. **使用 DynamicResource**：如果你在自己的 XAML 中使用语言文本，请使用 `{DynamicResource Lang.xxx}`
4. **线程安全**：`LangManager` 是单例，可以在任何线程调用，但建议在 UI 线程切换语言

## 常见问题

### Q: 如何在代码中获取当前语言的文本？

```csharp
// 方式 1：直接访问
var confirmText = LangManager.Instance.Current.Confirm;

// 方式 2：从资源获取
if (Application.Current?.Resources.TryGetResource("Lang.Confirm",
    Application.Current.ActualThemeVariant, out var value) == true)
{
    var text = value?.ToString();
}
```

### Q: 切换语言后，我的自定义控件没有更新？

确保在 XAML 中使用 `{DynamicResource Lang.xxx}` 而不是 `{StaticResource Lang.xxx}`。

### Q: 可以只覆盖部分文本吗？

不可以。你必须实现 `ILang` 接口的所有属性。如果某些文本不需要翻译，可以返回相同的值。

### Q: 如何添加 TioUi 没有的语言文本？

你可以创建自己的资源字典或使用 Avalonia 的本地化系统来管理额外的文本。TioUi 的语言系统只管理 TioUi 组件使用的文本。

---

更多信息请访问 [TioUi GitHub](https://github.com/tiouoo/TioUi.Avalonia)
