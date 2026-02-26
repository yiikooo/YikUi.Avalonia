using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Styling;
using YikUi.Common.Helpers;
using YikUi.Shared.Animations;

[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Controls")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Controls.Overlay")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Common")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Common.Helpers")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Common.Classes")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Common.Interfaces")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Common.Converter")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Shared.Animations")]
[assembly: XmlnsPrefix("https://github.com/yiikooo/YikUi.Avalonia", "yik")]

namespace YikUi;

public class YikUiTheme : Styles
{
    public static readonly StyledProperty<Color?> ThemeColorProperty =
        AvaloniaProperty.Register<YikUiTheme, Color?>(nameof(ThemeColor), Color.Parse("#1890ff"));

    public YikUiTheme()
    {
        AvaloniaXamlLoader.Load(this);
        Resources.MergedDictionaries.Add(new NavMenuSizeAnimations());
        this.GetObservable(ThemeColorProperty).Subscribe(color =>
        {
            if (color.HasValue)
            {
                ThemeHelper.SetThemeColor(color.Value);
            }
        });
    }

    public Color? ThemeColor
    {
        get => GetValue(ThemeColorProperty);
        set => SetValue(ThemeColorProperty, value);
    }

    public void SetAccentColor(Color color)
    {
        ThemeColor = color;
    }

    public void SetThemeColor(string hexColor)
    {
        if (Color.TryParse(hexColor, out var color))
        {
            SetAccentColor(color);
        }
    }
}