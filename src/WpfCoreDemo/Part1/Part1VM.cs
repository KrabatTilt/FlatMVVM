using System.Windows;
using TT.FlatMVVM.Core;
using Timer = System.Timers.Timer;

namespace WpfCoreDemo.Part1
{

    /// <summary>
    /// Simple ViewModel showing static and dynamic binding
    /// </summary>
    internal sealed class Part1VM : FlatVM, IDemoCase
    {
        public string StaticText { get; }

        public string[] StaticTextArray { get; }


        public string? DynamicText1
        {
            get;
            set => SetProperty(ref field, value);
        }

        public string DynamicText2
        {
            get;
            set => SetProperty(ref field, value);
        }


        public string FistName
        {
            get;
            set => SetProperty(ref field, value, [nameof(FullName), nameof(Email) /*add more properties here*/]);
        }

        public string LastName
        {
            get;
            set => SetProperty(ref field, value, [nameof(FullName), nameof(Email) /*add more properties here*/]);
        }

        public string FullName => $"{FistName} {LastName}";

        public string Email => $"{FistName.ToLower()}.{LastName.ToLower()}@flatmvvm.com";

        public Part1VM()
        {
            Name = "Bindings";
            Templates = new ResourceDictionary { Source = new Uri("/WpfCoreDemo;component/Part1/Part1DataTemplates.xaml", UriKind.RelativeOrAbsolute) };


            StaticText = "Hello";

            StaticTextArray = new string[5];
            for (int i = 0; i < 5; i++)
                StaticTextArray[i] = (5 - i).ToString();

            var timer = new Timer(1000);
            timer.Elapsed += (_, _) =>
            {
                DynamicText1 = DynamicText1 == null ? "Hello" : null;
            };
            timer.Start();

            DynamicText2 = "Change me";

            FistName = "Change";
            LastName = "Text";
        }

        public string Name { get; }

        public ResourceDictionary Templates { get; }
    }
}
