using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using XFEExtension.NetCore.StringExtension;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

public class URLImageConverter : IValueConverter
{
    public object? Convert(object value, Type targetType, object parameter, string language) => value is string url ? !url.IsNullOrWhiteSpace() ? new BitmapImage(new(url)) : null : null;

    public object ConvertBack(object value, Type targetType, object parameter, string language) => string.Empty;
}
