using System.Windows;
using TT.FlatMVVM.Core;
using Timer = System.Timers.Timer;

namespace WpfCoreDemo.Part3
{
    internal sealed class Part3VM : FlatVM , IDemoCase
    {
        public bool Bool1
        {
            get;
            set
            {
                if (value == field)
                    return;
                field = value;
                OnPropertyChanged();
            }
        }

        public bool Bool2
        {
            get;
            set
            {
                if (value == field)
                    return;
                field = value;
                OnPropertyChanged();
            }
        }

        public bool Bool3
        {
            get;
            set => SetProperty(ref field, value);
        }

        public bool? Bool4
        {
            get;
            set => SetProperty(ref field, value);
        }

        public Part3VM()
        {
            Name = "Boolean Value Converter";
            Templates = new ResourceDictionary { Source = new Uri("/WpfCoreDemo;component/Part3/Part3DataTemplates.xaml", UriKind.RelativeOrAbsolute) };

            var timer = new Timer(1000);
            timer.Elapsed += (_, _) => { Bool3 = !Bool3; };
            timer.Start();
        }

        public string Name { get; }

        public ResourceDictionary Templates { get; }
    }
}
