using System;
using System.Globalization;
using System.Windows.Data;

namespace TT.FlatMVVM.Converter
{

    /// <summary>
    /// InverseBooleanConverter
    /// </summary>
    [ValueConversion(typeof(bool), typeof(bool))]
    public class InverseBooleanConverter : AConverterBase, IValueConverter
    {
        /// <summary>Inverts a bool value.</summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool) && targetType != typeof(bool?))
                throw new InvalidOperationException($"Target must be of type {typeof(bool)} or {typeof(bool?)} but is {targetType}.");

            if (!(value is bool))
                throw new InvalidOperationException($"Value must be of type {typeof(bool)}.");

            return !(bool)value;
        }

        /// <summary>Inverts a bool value.</summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Convert(value, targetType, parameter, culture);
        }

    }
}