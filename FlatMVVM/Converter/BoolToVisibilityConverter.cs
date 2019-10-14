using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TT.FlatMVVM.Converter
{

    /// <summary>
    /// BooleanToVisibilityConverter
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Visibility))]
    public class BooleanToVisibilityConverter : AConverterBase, IValueConverter
    {
        /// <summary>
        /// Choose between Collapsed or Hidden when value is false. (Default is Visibility.Collapsed)
        /// </summary>
        public Visibility InvisibleTarget { get; set; } = Visibility.Collapsed;

        /// <summary>
        /// Invert the converters behavior.
        /// </summary>
        public bool Inverse { get; set; }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                throw new InvalidOperationException($"Target must be of type {typeof(Visibility)}.");

            if (!(value is bool))
                throw new InvalidOperationException($"Value must be of type {typeof(bool)}.");

            return (Inverse ? !(bool)value : (bool)value) ? Visibility.Visible : InvisibleTarget;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException($"Target must be of type {typeof(bool)}.");

            if (!(value is Visibility))
                throw new InvalidOperationException($"Value must be of type {typeof(Visibility)}.");

            return (Visibility)value == Visibility.Visible;
        }
    }
}