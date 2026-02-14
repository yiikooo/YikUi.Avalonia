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
    public static readonly StyledProperty<Color?> AccentColorProperty =
        AvaloniaProperty.Register<YikUiTheme, Color?>(nameof(AccentColor), Color.Parse("#1BD76A"));

    public YikUiTheme()
    {
        AvaloniaXamlLoader.Load(this);
        Resources.MergedDictionaries.Add(new NavMenuSizeAnimations());
        this.GetObservable(AccentColorProperty).Subscribe(color =>
        {
            if (color.HasValue)
            {
                ThemeHelper.SetAccentColor(color.Value);
            }
        });
    }

    public Color? AccentColor
    {
        get => GetValue(AccentColorProperty);
        set => SetValue(AccentColorProperty, value);
    }

    public void SetAccentColor(Color color)
    {
        AccentColor = color;
    }

    public void SetAccentColor(string hexColor)
    {
        if (Color.TryParse(hexColor, out var color))
        {
            SetAccentColor(color);
        }
    }
}