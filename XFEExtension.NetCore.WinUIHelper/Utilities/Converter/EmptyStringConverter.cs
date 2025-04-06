using Microsoft.UI.Xaml.Data;
using XFEExtension.NetCore.StringExtension;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

public partial class EmptyStringConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) => value.ToString().IsNullOrEmpty() ? "无" : value;

    public object ConvertBack(object value, Type targetType, object parameter, string language) => value.ToString() == "无" ? string.Empty : value;
}
