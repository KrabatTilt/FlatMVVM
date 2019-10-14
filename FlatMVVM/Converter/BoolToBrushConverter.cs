using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TT.FlatMVVM.Converter
{

    [ValueConversion(typeof(bool), typeof(Brush))]
    internal class BoolToBrushConverter : AConverterBase, IValueConverter
    {

        public Brush TrueBrush { get; set; }

        public Brush FalseBrush { get; set; }


        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException($"Target must be of type {typeof(Brush)}.");

            if (!(value is bool b))
                throw new InvalidOperationException($"Value must be of type {typeof(bool)}.");

            return b ? TrueBrush : FalseBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}