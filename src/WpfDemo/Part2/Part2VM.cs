using TT.FlatMVVM;

namespace WpfDemo.Part2
{
    internal class Part2VM : FlatVM
    {

        private Part2Enum _superEnum;
        private bool _useEnumValueDescription;

        public bool UseEnumValueDescription
        {
            get { return _useEnumValueDescription; }
            set
            {
                if (value == _useEnumValueDescription)
                    return;
                _useEnumValueDescription = value;
                OnPropertyChanged();
            }
        }

        public Part2Enum SuperEnum
        {
            get => _superEnum;
            set
            {
                if (value == _superEnum)
                    return;

                _superEnum = value;
                OnPropertyChanged();
            }
        }

        public Part2VM()
        {
            SuperEnum = Part2Enum.SUPER;
        }

    }
}
