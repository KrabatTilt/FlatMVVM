using System;
using System.ComponentModel;
using System.Reflection;

namespace TT.FlatMVVM.Converter
{

    /// <summary>
    /// An enum converter which converts enum values to its corresponding value description attribute
    /// </summary>
    public class EnumDescriptionTypeConverter : EnumConverter
    {

        /// <summary>
        /// Create a new instance of EnumDescriptionTypeConverter.
        /// </summary>
        /// <param name="type">Enum to convert</param>
        public EnumDescriptionTypeConverter(Type type)
            : base(type)
        {
        }

        /// <inheritdoc />
        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType != typeof(string))
                return base.ConvertTo(context, culture, value, destinationType);

            if (value == null)
                return string.Empty;

            FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
            if (fieldInfo == null)
                return string.Empty;

            var attributes = (DescriptionAttribute[]) fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return attributes.Length > 0 && attributes[0].Description != null ? attributes[0].Description : value.ToString();
        }
    }
}
