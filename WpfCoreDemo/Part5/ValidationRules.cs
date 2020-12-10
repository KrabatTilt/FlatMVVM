using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfCoreDemo.Part5
{
    internal static class ValidationRules
    {

        public static IEnumerable<string> ValidateNoNumbers(string input)
        {
            var validationResult = new List<string>();
            if (input.Any(char.IsDigit))
                validationResult.Add("Contains numbers");

            return validationResult;
        }

        public static IEnumerable<string>

        public static IEnumerable<string> BadValidationRule()
        {
            throw new Exception("Shiat");
        }

    }
}
