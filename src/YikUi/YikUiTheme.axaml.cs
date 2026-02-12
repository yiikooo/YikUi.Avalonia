using Avalonia.Markup.Xaml;
using Avalonia.Metadata;
using Avalonia.Styling;

[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Controls")]
[assembly: XmlnsPrefix("https://github.com/yiikooo/YikUi.Avalonia", "yik")]

namespace YikUi;

public class YikUiTheme : Styles
{
    public YikUiTheme()
    {
        AvaloniaXamlLoader.Load(this);
    }
}