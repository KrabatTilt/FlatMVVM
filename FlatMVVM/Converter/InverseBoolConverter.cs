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

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool))
                throw new InvalidOperationException($"Target must be of type {typeof(bool)}.");

            if (!(value is bool))
                throw new InvalidOperationException($"Value must be of type {typeof(bool)}.");

            return !(bool)value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }
}