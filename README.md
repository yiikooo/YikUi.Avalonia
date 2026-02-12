# YikUi.Avalonia

一个基于 Avalonia 的 UI 控件库

## 安装

```bash
dotnet add package YikUi
```

## 使用方法

在你的 `App.axaml` 中引入主题：

```xml
<Application xmlns="https://github.com/avaloniaui"
             xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
             x:Class="YourApp.App">
    <Application.Styles>
        <yikui:YikUiTheme xmlns:yikui="https://github.com/yiikooo/YikUi.Avalonia" />
    </Application.Styles>
</Application>
```

或者在窗口中使用：

```xml
<Window xmlns="https://github.com/avaloniaui"
        xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
        xmlns:yikui="https://github.com/yiikooo/YikUi.Avalonia"
        x:Class="YourApp.MainWindow">

    <!-- 你的控件 -->

</Window>
```

## 开发

项目结构：

- `YikUi/` - 主项目
  - `Controls/` - 自定义控件
  - `Themes/` - 主题样式文件
  - `YikUiTheme.cs` - 主题入口类

## License

MIT
