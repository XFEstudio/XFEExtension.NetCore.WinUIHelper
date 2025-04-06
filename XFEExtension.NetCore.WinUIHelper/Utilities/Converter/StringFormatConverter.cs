using Microsoft.UI.Xaml.Data;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

public class StringFormatConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        if (parameter.ToString() is string parameterString && value.ToString() is string valueString)
        {
            return parameterString.Contains("(0)") ? parameterString.Replace("(0)", valueString) : $"{parameter}{value}";
        }
        return $"{parameter}{value}";
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (parameter.ToString() is string parameterString && value.ToString() is string valueString)
        {
            return parameterString.Contains("(0)") ? valueString.Replace(parameterString.Replace("(0)", string.Empty), string.Empty) : valueString.Replace(parameterString, string.Empty);
        }
        return (value.ToString() ?? string.Empty).Replace(parameter.ToString() ?? string.Empty, string.Empty);
    }
}
