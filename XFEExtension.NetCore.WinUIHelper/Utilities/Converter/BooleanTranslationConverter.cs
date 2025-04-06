using Microsoft.UI.Xaml.Data;
namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

/// <summary>
/// 布尔值转换器
/// </summary>
public partial class BooleanTranslationConverter : IValueConverter
{
    /// <summary>
    /// 将布尔值转换为字符串
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    public object Convert(object value, Type targetType, object parameter, string language)
    {
        var split = parameter.ToString()!.Split('|');
        return value is bool boolValue ? boolValue ? split[0] : split[1] : value;
    }

    /// <summary>
    /// 将字符串转换为布尔值
    /// </summary>
    /// <param name="value"></param>
    /// <param name="targetType"></param>
    /// <param name="parameter"></param>
    /// <param name="language"></param>
    /// <returns></returns>
    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        var split = parameter.ToString()!.Split('|');
        return value.ToString() == split[0];
    }
}
