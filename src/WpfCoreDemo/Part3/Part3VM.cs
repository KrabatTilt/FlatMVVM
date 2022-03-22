using System.Timers;
using TT.FlatMVVM;

namespace WpfCoreDemo.Part3
{
    internal class Part3VM : FlatVM
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
            var timer = new Timer(1000);
            timer.Elapsed += (sender, args) => { Bool3 = !Bool3; };
            timer.Start();
        }

    }
}
