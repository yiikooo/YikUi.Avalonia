using Avalonia;
using Avalonia.Media;

namespace TioUi.Demo.Browser;

public static class AvaloniaAppBuilderExtensions
{
    private static string DefaultFontFamily => "avares://TioUi.Demo.Browser/Assets#Source Han Sans CN";

    public static AppBuilder WithSourceHanSansCNFont(this AppBuilder builder) =>
        builder.With(new FontManagerOptions
        {
            DefaultFamilyName = DefaultFontFamily,
            FontFallbacks = [new FontFallback { FontFamily = new FontFamily(DefaultFontFamily) }]
        });
}