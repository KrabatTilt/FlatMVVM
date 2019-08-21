using System.Timers;
using TT.FlatMVVM.Wpf;

namespace WpfDemo.Part1
{

    /// <summary>
    /// Simple ViewModel showing static and dynamic binding
    /// </summary>
    internal class SimpleVM : FlatVM
    {

        private readonly Timer _timer;
        private string _dynamicText1;
        private string _dynamicText2;


        public string StaticText { get; }

        public string[] StaticTextArray { get; }


        public string DynamicText1
        {
            get => _dynamicText1;
            set
            {
                _dynamicText1 = value; 
                OnPropertyChanged();
            }
        }

        public string DynamicText2
        {
            get => _dynamicText2;
            set
            {
                _dynamicText2 = value; 
                OnPropertyChanged();
            }
        }

        public SimpleVM()
        {
            StaticText = "Hello";

            StaticTextArray = new string[5];
            for (int i = 0; i < 5; i++)
                StaticTextArray[i] = (5 - i).ToString();

            _timer = new Timer(1000);
            _timer.Elapsed += (sender, args) =>
            {
                DynamicText1 = DynamicText1 == null ? "Hello" : null;
            };
            _timer.Start();

            DynamicText2 = "Change me";

        }

    }
}
