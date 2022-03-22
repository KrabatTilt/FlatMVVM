using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace TT.FlatMVVM.Converter
{

    /// <summary>
    /// Use BoolToBrushConverter to convert a boolean value to predefined brushes for true and false condition.
    /// </summary>
    [ValueConversion(typeof(bool), typeof(Brush))]
    public class BoolToBrushConverter : AConverterBase, IValueConverter
    {

        /// <summary>
        /// Brush used when bound property is true.
        /// </summary>
        public Brush TrueBrush { get; set; }

        /// <summary>
        /// Brush used when bound property is false.
        /// </summary>
        public Brush FalseBrush { get; set; }

        /// <summary>
        /// Convert bool to brush.
        /// </summary>
        /// <param name="value">Source value of type bool.</param>
        /// <param name="targetType">Target value of type Visibility.</param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush))
                throw new InvalidOperationException($"Target must be of type {typeof(Brush)}.");

            if (!(value is bool b))
                throw new InvalidOperationException($"Value must be of type {typeof(bool)}.");

            return b ? TrueBrush : FalseBrush;
        }

        /// <inheritdoc />
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}