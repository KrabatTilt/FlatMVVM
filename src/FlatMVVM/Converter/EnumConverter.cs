using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Data;

namespace TT.FlatMVVM.Converter
{

    /// <summary>
    /// BooleanToVisibilityConverter
    /// </summary>
    [ValueConversion(typeof(Enum), typeof(Array))]
    public class EnumSourceConverter : AConverterBase, IValueConverter
    {

        /// <summary>
        /// Get/Set whether to display enum value description attribute or not.
        /// </summary>
        public bool UseDescription { get; set; }

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value produced by the binding source.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is Type enumtype))
                throw new InvalidOperationException("Value cannot be cast to Type.");

            if(!enumtype.IsEnum)
                throw new InvalidOperationException("Value must be an enum.");

            if (UseDescription)
                TypeDescriptor.AddAttributes(enumtype, new TypeConverterAttribute(typeof(EnumDescriptionTypeConverter)));

            return Enum.GetValues(enumtype);
        }

        /// <summary>Converts a value. </summary>
        /// <param name="value">The value that is produced by the binding target.</param>
        /// <param name="targetType">The type to convert to.</param>
        /// <param name="parameter">The converter parameter to use.</param>
        /// <param name="culture">The culture to use in the converter.</param>
        /// <returns>A converted value. If the method returns <see langword="null" />, the valid null value is used.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
