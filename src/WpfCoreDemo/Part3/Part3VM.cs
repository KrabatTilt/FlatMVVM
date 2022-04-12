using System;
using System.Timers;
using System.Windows;
using TT.FlatMVVM;

namespace WpfCoreDemo.Part3
{
    internal class Part3VM : FlatVM , IDemoCase
    {
        private bool _bool1;
        private bool _bool2;
        private bool _bool3;
        private bool? _bool4;


        public bool Bool1
        {
            get => _bool1;
            set
            {
                if (value == _bool1)
                    return;
                _bool1 = value;
                OnPropertyChanged();
            }
        }

        public bool Bool2
        {
            get => _bool2;
            set
            {
                if (value == _bool2)
                    return;
                _bool2 = value;
                OnPropertyChanged();
            }
        }

        public bool Bool3
        {
            get => _bool3;
            set => SetProperty(ref _bool3, value);
        }

        public bool? Bool4
        {
            get => _bool4;
            set => SetProperty(ref _bool4, value);
        }

        public Part3VM()
        {
            Name = "Boolean Value Converter";
            Templates = new ResourceDictionary { Source = new Uri("/WpfCoreDemo;component/Part3/Part3DataTemplates.xaml", UriKind.RelativeOrAbsolute) };

            var timer = new Timer(1000);
            timer.Elapsed += (sender, args) => { Bool3 = !Bool3; };
            timer.Start();
        }

        public string Name { get; }

        public ResourceDictionary Templates { get; }
    }
}
