using System;
using System.Globalization;
using System.Windows.Data;

namespace TT.FlatMVVM.Converter
{

    /// <summary>
    /// Converts multiple bool values to a single one by applying the boolean operations defined in the Operators string.
    /// </summary>
    public class MultiBooleanConverter : AConverterBase, IMultiValueConverter
    {
        private string _operators;
        private Func<bool[], string, bool> _evaluate;

        /// <summary>
        /// A list of boolean operators
        /// </summary>
        public string Operators
        {
            get => _operators;
            set
            {
                _operators = value;
                GenerateEvaluationFunction();
            }
        }

        /// <summary>
        /// Get or set the value that is used when null is passed into the converter. Default = false 
        /// </summary>
        public bool NullValue { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="values"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Length <= Operators.Length)
                throw new InvalidOperationException($"Number of supplied boolean operators must be {values.Length - 1} but is {Operators.Length}");

            if (targetType != typeof(bool) && targetType != typeof(bool?))
                throw new InvalidOperationException($"Target must be of type {typeof(bool)} or {typeof(bool?)} but is {targetType}.");

            var boolValues = new bool[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                switch (values[i])
                {
                    case null:
                        boolValues[i] = NullValue;
                        break;
                    case bool boolValue:
                        boolValues[i] = boolValue;
                        break;
                    default:
                        throw new InvalidOperationException($"Parameter provided must be if type {typeof(bool)} or {typeof(bool?)} but is {values[i].GetType().Name}");
                }
            }

            return _evaluate(boolValues, Operators);
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

        private void GenerateEvaluationFunction()
        {
            //TODO: Check if this can also be done using DynamicMethod class and IL emit to avoid loop and provide a static expression.
            _evaluate = (bools, operators) =>
            {
                operators = operators.ToUpper();
                int i = 0;
                bool result = bools[i++];

                foreach (char operation in operators)
                {
                    switch (operation)
                    {
                        case 'A':
                            result = result && bools[i++];
                            continue;
                        case 'O':
                            result = result || bools[i++];
                            continue;
                    }
                }
                return result;
            };
        }

    }
}
