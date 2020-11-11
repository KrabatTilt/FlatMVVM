using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TT.FlatMVVM.Converter
{

    /// <summary>
    /// Use NullToVisibilityConverter to convert a null value to predefined visibility. Can be inverted.
    /// </summary>
    [ValueConversion(typeof(object), typeof(Visibility))]
    public class NullToVisibilityConverter : AConverterBase, IValueConverter
    {

        /// <summary>
        /// Choose between Collapsed or Hidden when value is false. (Default is Visibility.Collapsed)
        /// </summary>
        public Visibility InvisibleTarget { get; set; } = Visibility.Collapsed;

        /// <summary>
        /// Invert the converters behavior.
        /// </summary>
        public bool Inverse { get; set; }

        /// <summary>
        /// Convert null to visibility.
        /// </summary>
        /// <param name="value">Source value of type bool.</param>
        /// <param name="targetType">Target value of type Visibility.</param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException($"Target must be of type {typeof(Visibility)}.");

            return (Inverse ? value != null : value == null) ? InvisibleTarget : Visibility.Visible;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
