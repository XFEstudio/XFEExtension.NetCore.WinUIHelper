using Microsoft.UI.Xaml.Data;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

/// <summary>
/// 字符串格式转换器
/// </summary>
public partial class StringFormatConverter : IValueConverter
{
    /// <summary>
    /// Converts the input value into a formatted string based on the specified parameter and language.
    /// </summary>
    /// <remarks>This method assumes that both <paramref name="value"/> and <paramref name="parameter"/> can
    /// be converted to strings. If either is not a string or cannot be converted to a string, the resulting behavior
    /// may be undefined.</remarks>
    /// <param name="value">The value to be converted. Typically a string representation.</param>
    /// <param name="targetType">The target type of the conversion. This parameter is not used in the current implementation.</param>
    /// <param name="parameter">A formatting string that may contain placeholders, such as "(0)", to be replaced with the value.</param>
    /// <param name="language">The language parameter for localization purposes. This parameter is not used in the current implementation.</param>
    /// <returns>A string that combines the parameter and value. If the parameter contains the placeholder "(0)", it is replaced
    /// with the value; otherwise, the parameter and value are concatenated.</returns>
    public object? Convert(object value, Type targetType, object parameter, string language)
    {
        if (value is not null && parameter.ToString() is string parameterString && value.ToString() is string valueString)
        {
            return parameterString.Contains("(0)") ? parameterString.Replace("(0)", valueString) : $"{parameter}{value}";
        }
        return value;
    }

    /// <summary>
    /// Converts a value back to its original form by removing a specified parameter string from it.
    /// </summary>
    /// <remarks>This method is typically used in scenarios where a formatted string needs to be reverted to
    /// its original form by removing specific substrings. The behavior depends on whether the parameter contains the
    /// "(0)" placeholder.</remarks>
    /// <param name="value">The value to be converted back. Typically a string.</param>
    /// <param name="targetType">The type to convert the value to. This parameter is not used in this implementation.</param>
    /// <param name="parameter">The string parameter to be removed from the value. If the parameter contains "(0)", it will be treated as a
    /// placeholder.</param>
    /// <param name="language">The language information for the conversion. This parameter is not used in this implementation.</param>
    /// <returns>A string representing the original value with the specified parameter string removed. If the parameter contains
    /// "(0)", the placeholder is replaced before removal.</returns>
    public object? ConvertBack(object value, Type targetType, object parameter, string language)
    {
        if (parameter.ToString() is string parameterString && value.ToString() is string valueString)
        {
            return parameterString.Contains("(0)") ? valueString.Replace(parameterString.Replace("(0)", string.Empty), string.Empty) : valueString.Replace(parameterString, string.Empty);
        }
        return (value.ToString() ?? string.Empty).Replace(parameter.ToString() ?? string.Empty, string.Empty);
    }
}
