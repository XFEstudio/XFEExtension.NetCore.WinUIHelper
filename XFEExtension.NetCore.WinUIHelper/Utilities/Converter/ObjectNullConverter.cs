using Microsoft.UI.Xaml.Data;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

/// <summary>
/// Provides conversion logic to determine whether an object is null.
/// </summary>
/// <remarks>This converter is typically used in data binding scenarios to convert an object to a boolean value.
/// The <see cref="Convert"/> method returns <see langword="false"/> if the input object is <see langword="null"/>; 
/// otherwise, it returns <see langword="true"/>. The <see cref="ConvertBack"/> method returns the input value
/// unchanged.</remarks>
public partial class ObjectNullConverter : IValueConverter
{
    /// <summary>
    /// Converts the specified value to a boolean indicating whether the value is not null.
    /// </summary>
    /// <param name="value">The value to be checked for null.</param>
    /// <param name="targetType">The target type of the conversion. This parameter is not used in this implementation.</param>
    /// <param name="parameter">An optional parameter for the conversion. This parameter is not used in this implementation.</param>
    /// <param name="language">The language information for the conversion. This parameter is not used in this implementation.</param>
    /// <returns><see langword="true"/> if <paramref name="value"/> is not null; otherwise, <see langword="false"/>.</returns>
    public object Convert(object value, Type targetType, object parameter, string language) => value is null ? false : true;

    /// <summary>
    /// Converts a value back to its original type or representation.
    /// </summary>
    /// <param name="value">The value to be converted back. This is typically the result of a previous conversion.</param>
    /// <param name="targetType">The type to which the value should be converted. This parameter is not used in the default implementation.</param>
    /// <param name="parameter">An optional parameter to influence the conversion logic. This parameter is not used in the default
    /// implementation.</param>
    /// <param name="language">The language or culture information to use during the conversion. This parameter is not used in the default
    /// implementation.</param>
    /// <returns>The original value passed to the method, without modification.</returns>
    public object ConvertBack(object value, Type targetType, object parameter, string language) => value;
}
