using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Styling;
using YikUi.Common.Helpers;
using YikUi.Common.Language;
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
[assembly: XmlnsDefinition("https://github.com/yiikooo/YikUi.Avalonia", "YikUi.Theme.Controls.ColorPicker")]
[assembly: XmlnsPrefix("https://github.com/yiikooo/YikUi.Avalonia", "yik")]

namespace YikUi;

public class YikUiTheme : Styles
{
    public static readonly StyledProperty<Color> ThemeColorProperty =
        AvaloniaProperty.Register<YikUiTheme, Color>(nameof(ThemeColor), Color.Parse("#1890ff"));

    public static readonly StyledProperty<Languages?> LanguageProperty =
        AvaloniaProperty.Register<YikUiTheme, Languages?>(nameof(Language));

    public static readonly StyledProperty<ILang?> CustomLanguageProperty =
        AvaloniaProperty.Register<YikUiTheme, ILang?>(nameof(CustomLanguage));

    public YikUiTheme()
    {
        Resources.MergedDictionaries.Add(new NavMenuSizeAnimations());

        if (CustomLanguage != null)
            LangManager.SetLanguage(CustomLanguage);
        else if (Language != null)
            LangManager.SetLanguage(Language.Value);
        else
            LangManager.SetLanguage(Languages.zh_cn);

        this.GetObservable(ThemeColorProperty).Subscribe(ThemeHelper.SetThemeColor);

        this.GetObservable(LanguageProperty).Subscribe(lang =>
        {
            if (lang.HasValue)
                LangManager.SetLanguage(lang.Value);
        });

        this.GetObservable(CustomLanguageProperty).Subscribe(customLang =>
        {
            if (customLang != null)
                LangManager.SetLanguage(customLang);
        });

        AvaloniaXamlLoader.Load(this);
    }

    public Color? ThemeColor
    {
        get => GetValue(ThemeColorProperty);
        set => SetValue(ThemeColorProperty, value);
    }

    public Languages? Language
    {
        get => GetValue(LanguageProperty);
        set => SetValue(LanguageProperty, value);
    }

    public ILang? CustomLanguage
    {
        get => GetValue(CustomLanguageProperty);
        set => SetValue(CustomLanguageProperty, value);
    }

    public void SetThemeColor(Color color)
    {
        ThemeColor = color;
    }

    public void SetThemeColor(string hexColor)
    {
        if (Color.TryParse(hexColor, out var color))
        {
            SetThemeColor(color);
        }
    }

    public void SetLanguage(Languages lang)
    {
        Language = lang;
    }

    public void SetLanguage(ILang customLang)
    {
        CustomLanguage = customLang;
    }
}