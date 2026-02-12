using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using Avalonia.Styling;

[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Controls")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Controls.Overlay")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Common")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Common.Helpers")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Common.Classes")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Common.Interfaces")]
[assembly: XmlnsPrefix("https://github.com/yiikooo/YikUi.Avalonia", "yik")]

namespace YikUi;

public class YikUiTheme : Styles
{
    public YikUiTheme()
    {
        AvaloniaXamlLoader.Load(this);
    }
}