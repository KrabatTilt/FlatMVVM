﻿using System;
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

        /// <summary>
        /// Convert bool to visibility.
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

            if (!(value is bool))
                throw new InvalidOperationException($"Value must be of type {typeof(bool)}.");

            return (Inverse ? !(bool)value : (bool)value) ? Visibility.Visible : InvisibleTarget;
        }

        /// <summary>
        /// Convert Visibility to bool
        /// </summary>
        /// <param name="value">Source value of type Visibility.</param>
        /// <param name="targetType">Target value of type bool.</param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
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