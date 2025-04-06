using Microsoft.UI.Xaml.Data;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

public partial class ObjectNullConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, string language) => value is null ? false : true;

    public object ConvertBack(object value, Type targetType, object parameter, string language) => value;
}
