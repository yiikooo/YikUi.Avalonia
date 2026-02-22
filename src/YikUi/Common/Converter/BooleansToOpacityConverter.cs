using System.Globalization;

namespace YikUi.Common.Converter;

public class BooleansToOpacityConverter : MarkupValueConverter
{
    public override object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is bool b)
        {
            return b ? 1.0 : 0.0;
        }

        return 1;
    }
}