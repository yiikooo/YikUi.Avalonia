using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace YikUi.Common.Converter;

/// <summary>
/// 颜色变暗转换器，支持 15%、30%、45% 三个级别
/// </summary>
public class ColorDarkenConverter : IValueConverter
{
    public static readonly ColorDarkenConverter Darken15 = new() { Percentage = 0.15 };
    public static readonly ColorDarkenConverter Darken30 = new() { Percentage = 0.30 };
    public static readonly ColorDarkenConverter Darken45 = new() { Percentage = 0.45 };

    public double Percentage { get; set; } = 0.15;

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Color color)
        {
            return DarkenColor(color, Percentage);
        }

        if (value is SolidColorBrush brush)
        {
            return new SolidColorBrush(DarkenColor(brush.Color, Percentage));
        }

        return value;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }

    private static Color DarkenColor(Color color, double percentage)
    {
        var r = (byte)(color.R * (1 - percentage));
        var g = (byte)(color.G * (1 - percentage));
        var b = (byte)(color.B * (1 - percentage));
        return Color.FromArgb(color.A, r, g, b);
    }
}