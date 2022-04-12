using System;
using System.Timers;
using System.Windows;
using TT.FlatMVVM;

namespace WpfCoreDemo.Part1
{

    /// <summary>
    /// Simple ViewModel showing static and dynamic binding
    /// </summary>
    internal class Part1VM : FlatVM, IDemoCase
    {
        private string _dynamicText1;
        private string _dynamicText2;

        private string _fistName;
        private string _lastName;

        public string StaticText { get; }

        public string[] StaticTextArray { get; }


        public string DynamicText1
        {
            get => _dynamicText1;
            set => SetProperty(ref _dynamicText1, value);
        }

        public string DynamicText2
        {
            get => _dynamicText2;
            set => SetProperty(ref _dynamicText2, value);
        }


        public string FistName
        {
            get => _fistName;
            set => SetProperty(ref _fistName, value, new[] { nameof(FullName), nameof(Email) /*add more properties here*/ });
        }

        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value, new[] { nameof(FullName), nameof(Email) /*add more properties here*/ });
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
            timer.Elapsed += (sender, args) =>
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
