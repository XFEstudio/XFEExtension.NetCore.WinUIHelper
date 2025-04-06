using Microsoft.UI.Xaml.Data;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

public partial class DateTimeOffsetConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, string language) => value is DateTime dateTime ? new DateTimeOffset(dateTime) : default;

    public object? ConvertBack(object value, Type targetType, object parameter, string language) => value is DateTimeOffset dateTimeOffset ? dateTimeOffset.DateTime : default;
}
