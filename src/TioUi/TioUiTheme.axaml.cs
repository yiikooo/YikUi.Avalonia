using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Metadata;
using Avalonia.Styling;
using TioUi.Common.Helpers;
using TioUi.Common.Language;
using TioUi.Shared.Animations;

[assembly: XmlnsDefinition("https://github.com/yiikooo/TioUi.Avalonia", "TioUi")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/TioUi.Avalonia", "TioUi.Controls")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/TioUi.Avalonia", "TioUi.Controls.Overlay")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/TioUi.Avalonia", "TioUi.Common")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/TioUi.Avalonia", "TioUi.Common.Helpers")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/TioUi.Avalonia", "TioUi.Common.Classes")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/TioUi.Avalonia", "TioUi.Common.Interfaces")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/TioUi.Avalonia", "TioUi.Common.Converter")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/TioUi.Avalonia", "TioUi.Shared.Animations")]
[assembly: XmlnsDefinition("https://github.com/yiikooo/TioUi.Avalonia", "TioUi.Theme.Controls.ColorPicker")]
[assembly: XmlnsPrefix("https://github.com/yiikooo/TioUi.Avalonia", "tio")]

namespace TioUi;

public class TioUiTheme : Styles
{
    public static readonly StyledProperty<Color> ThemeColorProperty =
        AvaloniaProperty.Register<TioUiTheme, Color>(nameof(ThemeColor), Color.Parse("#1890ff"));

    public static readonly StyledProperty<Languages?> LanguageProperty =
        AvaloniaProperty.Register<TioUiTheme, Languages?>(nameof(Language));

    public static readonly StyledProperty<ILang?> CustomLanguageProperty =
        AvaloniaProperty.Register<TioUiTheme, ILang?>(nameof(CustomLanguage));

    public TioUiTheme()
    {
        Resources.MergedDictionaries.Add(new NavMenuSizeAnimations());

        if (CustomLanguage != null)
            LangManager.SetLanguage(CustomLanguage);
        else if (Language != null)
            LangManager.SetLanguage(Language.Value);
        else
            LangManager.SetLanguage(Languages.zh_cn);

        ThemeManager.SetThemeColor(ThemeColor);

        // 使用 ThemeManager 统一管理主题色
        ObservableExtension.Subscribe(this.GetObservable(ThemeColorProperty), ThemeManager.SetThemeColor);

        ObservableExtension.Subscribe(this.GetObservable(LanguageProperty), lang =>
        {
            if (lang.HasValue)
                LangManager.SetLanguage(lang.Value);
        });

        ObservableExtension.Subscribe(this.GetObservable(CustomLanguageProperty), customLang =>
        {
            if (customLang != null)
                LangManager.SetLanguage(customLang);
        });

        AvaloniaXamlLoader.Load(this);
    }

    public Color ThemeColor
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