using YikUi.Common.Language.Langs;

namespace YikUi.Common.Language;

public static class LangManager
{
    public static ILang Current
    {
        get;
        set
        {
            if (field == value) return;
            field = value;
            LanguageChanged?.Invoke(null, value);
        }
    } = new LangZhCn();

    public static event EventHandler<ILang>? LanguageChanged;

    public static void SetLanguage(Languages lang)
    {
        Current = lang switch
        {
            Languages.zh_cn => new LangZhCn(),
            Languages.en_us => new LangEnUs(),
            _ => new LangZhCn()
        };
    }

    public static void SetLanguage(ILang customLang)
    {
        Current = customLang ?? throw new ArgumentNullException(nameof(customLang));
    }
}