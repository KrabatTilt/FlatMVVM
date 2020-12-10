using TT.FlatMVVM;

namespace WpfCoreDemo.Part5
{
    internal class Part5VM : FlatVM
    {

        private string _stringInput1;
        private string _inputString2;


        public string StringInput1
        {
            get => _stringInput1;
            set => SetProperty(ref _stringInput1, value, ValidationRules.ValidateNoNumbers);
        }

        public string Linked1 => InputString2;

        public string Linked2 => InputString2;
        

        public string InputString2
        {
            get => _inputString2;
            set => SetProperty(ref _inputString2, value, new []{nameof(Linked1), nameof(Linked2)}, ValidationRules.ValidateNoNumbers);
        }
       
    }
}
