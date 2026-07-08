using System.Windows;
using TT.FlatMVVM.Core;

namespace WpfCoreDemo.Part5
{
    internal sealed class Part5VM : FlatValidationVM , IDemoCase
    {
        public string? StringInput1
        {
            get;
            set => SetProperty(ref field, value, ValidationRules.ValidateNoNumbers);
        }

        public string? Linked1 => StringInput2;

        public string? Linked2 => StringInput2;


        public string? StringInput2
        {
            get;
            set => SetProperty(ref field, value, [nameof(Linked1), nameof(Linked2)], ValidationRules.ValidateNoNumbers);
        }

        public string? StringInput3
        {
            get;
            set => SetProperty(ref field, value, ValidationRules.BadValidationRule());
        }

        public Part5VM()
        {
            Name = "Input Validation";
            Templates = new ResourceDictionary { Source = new Uri("/WpfCoreDemo;component/Part5/Part5DataTemplates.xaml", UriKind.RelativeOrAbsolute) };

        }

        public string Name { get; }

        public ResourceDictionary Templates { get; }
    }
}
