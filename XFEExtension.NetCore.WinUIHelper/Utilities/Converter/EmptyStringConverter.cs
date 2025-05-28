using Microsoft.UI.Xaml.Data;
using XFEExtension.NetCore.StringExtension;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

/// <summary>
/// Provides conversion logic for handling empty strings in data binding scenarios.
/// </summary>
/// <remarks>This converter transforms empty or null string values into a predefined placeholder string ("无")  and
/// vice versa. It is commonly used in UI applications to display a user-friendly representation  of empty
/// strings.</remarks>
public partial class EmptyStringConverter : IValueConverter
{
    /// <summary>
    /// Converts the specified value to a target type, returning a default representation if the value is null or empty.
    /// </summary>
    /// <param name="value">The value to be converted. If the value is null or an empty string, a default representation is returned.</param>
    /// <param name="targetType">The type to which the value is being converted. This parameter is not used in the current implementation.</param>
    /// <param name="parameter">An optional parameter that can be used to influence the conversion logic. This parameter is not used in the
    /// current implementation.</param>
    /// <param name="language">The language information for the conversion. This parameter is not used in the current implementation.</param>
    /// <returns>The original value if it is not null or empty; otherwise, the string "无".</returns>
    public object Convert(object value, Type targetType, object parameter, string language) => value.ToString().IsNullOrEmpty() ? "无" : value;

    /// <summary>
    /// Converts a value back to its original representation based on the specified logic.
    /// </summary>
    /// <param name="value">The value to be converted back. Typically a string representation.</param>
    /// <param name="targetType">The type to which the value is being converted. This parameter is not used in the current implementation.</param>
    /// <param name="parameter">An optional parameter for the conversion logic. This parameter is not used in the current implementation.</param>
    /// <param name="language">The language information for the conversion. This parameter is not used in the current implementation.</param>
    /// <returns>A string representing the converted value. Returns an empty string if the input value is "无"; otherwise, returns
    /// the original value.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, string language) => value.ToString() == "无" ? string.Empty : value;
}
