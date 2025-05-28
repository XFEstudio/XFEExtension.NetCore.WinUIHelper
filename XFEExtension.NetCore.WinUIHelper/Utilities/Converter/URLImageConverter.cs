using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using XFEExtension.NetCore.StringExtension;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

/// <summary>
/// Converts a URL string into a <see cref="BitmapImage"/> for use in UI elements.
/// </summary>
/// <remarks>This converter takes a string representing a URL and attempts to create a <see cref="BitmapImage"/> 
/// from it. If the URL is null, empty, or consists only of whitespace, the method returns <see
/// langword="null"/>.</remarks>
public partial class URLImageConverter : IValueConverter
{
    /// <summary>
    /// Converts a string URL into a <see cref="BitmapImage"/> instance, or returns <see langword="null"/> if the URL is
    /// invalid or empty.
    /// </summary>
    /// <param name="value">The input value to convert. Must be a string representing a URL.</param>
    /// <param name="targetType">The type to convert to. This parameter is not used in the conversion process.</param>
    /// <param name="parameter">An optional parameter for the conversion. This parameter is not used in the conversion process.</param>
    /// <param name="language">The language information for the conversion. This parameter is not used in the conversion process.</param>
    /// <returns>A <see cref="BitmapImage"/> created from the provided URL if the input is a non-empty string; otherwise, <see
    /// langword="null"/>.</returns>
    public object? Convert(object value, Type targetType, object parameter, string language) => value is string url ? !url.IsNullOrWhiteSpace() ? new BitmapImage(new(url)) : null : null;

    /// <summary>
    /// Converts a value back to its original type or representation.
    /// </summary>
    /// <param name="value">The value to be converted back. This is typically the result of a previous conversion.</param>
    /// <param name="targetType">The type to which the value should be converted. This parameter specifies the desired target type.</param>
    /// <param name="parameter">An optional parameter to use during the conversion. This can be used to provide additional context or
    /// configuration.</param>
    /// <param name="language">The language or culture information to use during the conversion. This parameter may influence localization or
    /// formatting.</param>
    /// <returns>An object representing the converted value. The default implementation returns an empty string.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, string language) => string.Empty;
}
