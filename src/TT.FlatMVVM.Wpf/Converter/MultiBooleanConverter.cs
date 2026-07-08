using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace TT.FlatMVVM.Wpf.Converter
{

    /// <summary>
    /// Converts multiple bool values to a single one by applying the boolean operations defined in the Operators string.
    /// Sequential evaluation (Left-to-Right).
    /// </summary>
    public class MultiBooleanConverter : AConverterBase, IMultiValueConverter
    {
        private string? _operators;
        //private Func<bool[], string, bool> _evaluate;

        /// <summary>
        /// A string representing boolean operators (A = AND, O = OR).
        /// Example: "AO" means (Value1 AND Value2) OR Value3.
        /// </summary>
        public string? Operators
        {
            get => _operators;
            set
            {
                _operators = value;
                //GenerateEvaluationFunction();
            }
        }

        /// <summary>
        /// Value used when an input is null or DependencyProperty.UnsetValue. Default is false.
        /// </summary>
        public bool NullValue { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[]? values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values is null || values.Length == 0)
                return NullValue;

            string ops = Operators ?? string.Empty;
            if (ops.Length != values.Length - 1)
                throw new InvalidOperationException($"Input mismatch: Received {values.Length} values but {ops.Length} operators. Expected {values.Length - 1} operators.");

            bool result = ParseBool(values[0]);

            for (int i = 0; i < ops.Length; i++)
            {
                bool nextValue = ParseBool(values[i + 1]);
                char op = char.ToUpperInvariant(ops[i]);

                result = op switch
                {
                    'A' => result && nextValue,
                    'O' => result || nextValue,
                    _ => throw new ArgumentException($"Unknown operator '{op}'. Only 'A' (AND) and 'O' (OR) are allowed.")
                };
            }

            return result;

            //var boolValues = new bool[values.Length];
            //for (int i = 0; i < values.Length; i++)
            //{
            //    if (values[i] == DependencyProperty.UnsetValue)
            //    {
            //        boolValues[i] = NullValue;
            //    }
            //    else if (values[i] is bool boolValue)
            //    {
            //        boolValues[i] = boolValue;
            //    }
            //    else
            //    {
            //        throw new InvalidOperationException($"Parameter provided must be of type {typeof(bool)} or {typeof(bool?)} but is {values[i].GetType().Name}");
            //    }
            //}

            //return _evaluate(boolValues, Operators);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        private bool ParseBool(object? value)
        {
            if (value is null || value == DependencyProperty.UnsetValue)
                return NullValue;

            if (value is bool b)
                return b;

            throw new InvalidOperationException($"Value must be a boolean, but was {value.GetType().Name}");
        }

        //private void GenerateEvaluationFunction()
        //{
        //    //TODO: Check if this can also be done using DynamicMethod class and IL emit to avoid loop and provide a static expression.
        //    _evaluate = (bools, operators) =>
        //    {
        //        operators = operators.ToUpper();
        //        int i = 0;
        //        bool result = bools[i++];

        //        foreach (char operation in operators)
        //        {
        //            switch (operation)
        //            {
        //                case 'A':
        //                    result = result && bools[i++];
        //                    continue;
        //                case 'O':
        //                    result = result || bools[i++];
        //                    continue;
        //            }
        //        }
        //        return result;
        //    };
        //}

    }
}
