using Microsoft.UI.Xaml.Data;

namespace XFEExtension.NetCore.WinUIHelper.Utilities.Converter;

/// <summary>
/// Provides conversion between <see cref="DateTime"/> and <see cref="DateTimeOffset"/> objects.
/// </summary>
/// <remarks>This converter is typically used in scenarios where a <see cref="DateTime"/> value needs to be
/// transformed into a <see cref="DateTimeOffset"/> or vice versa, such as in data binding or serialization
/// contexts.</remarks>
public partial class DateTimeOffsetConverter : IValueConverter
{
    /// <summary>
    /// Converts a <see cref="DateTime"/> value to a <see cref="DateTimeOffset"/> instance.
    /// </summary>
    /// <param name="value">The value to convert. Must be a <see cref="DateTime"/> instance.</param>
    /// <param name="targetType">The target type for the conversion. This parameter is not used in this implementation.</param>
    /// <param name="parameter">An optional parameter for the conversion. This parameter is not used in this implementation.</param>
    /// <param name="language">The language information for the conversion. This parameter is not used in this implementation.</param>
    /// <returns>A <see cref="DateTimeOffset"/> instance representing the input <see cref="DateTime"/> value,  or <see
    /// langword="null"/> if the input value is not a <see cref="DateTime"/>.</returns>
    public object? Convert(object value, Type targetType, object parameter, string language) => value is DateTime dateTime ? new DateTimeOffset(dateTime) : default;

    /// <summary>
    /// Converts a <see cref="DateTimeOffset"/> value to a <see cref="DateTime"/>.  Returns <see langword="null"/> if
    /// the input value is not a <see cref="DateTimeOffset"/>.
    /// </summary>
    /// <param name="value">The value to convert, expected to be of type <see cref="DateTimeOffset"/>.</param>
    /// <param name="targetType">The target type of the conversion. This parameter is not used in this implementation.</param>
    /// <param name="parameter">An optional parameter for the conversion. This parameter is not used in this implementation.</param>
    /// <param name="language">The language information for the conversion. This parameter is not used in this implementation.</param>
    /// <returns>A <see cref="DateTime"/> representation of the input <see cref="DateTimeOffset"/> value,  or <see
    /// langword="null"/> if the input value is not a <see cref="DateTimeOffset"/>.</returns>
    public object? ConvertBack(object value, Type targetType, object parameter, string language) => value is DateTimeOffset dateTimeOffset ? dateTimeOffset.DateTime : default;
}
