using Microsoft.UI.Xaml.Data;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

public partial class BooleanTranslationConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var split = parameter.ToString()!.Split('|');
        return value is bool boolValue ? boolValue ? split[0] : split[1] : value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        var split = parameter.ToString()!.Split('|');
        return value.ToString() == split[0];
    }
}
