using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TT.FlatMVVM;

namespace WpfCoreDemo.Part4
{
    internal class Part4VM : FlatVM
    {

        private ICommand _blockedCanExecuteClickCommand;
        private ICommand _clickWithParameterCommand;

        private bool _isChecked;
        private string _paramText;
        

        public bool IsChecked
        {
            get => _isChecked;
            set => SetProperty(ref _isChecked, value);
        }

        public string ParamText
        {
            get => _paramText;
            set => SetProperty(ref _paramText, value);
        }


        public Part4VM()
        {
            ParamText = "Change me";
        }


        private ICommand _simpleClickCommand;
        
        public ICommand SimpleClick => _simpleClickCommand ??= new DelegateCommand(ExecuteSimpleClickCommand);

        private void ExecuteSimpleClickCommand()
        {
            Debug.WriteLine("Simple Click Command");
        }

        
        public ICommand CanExecuteClick => _blockedCanExecuteClickCommand ??= new DelegateCommand(ExecuteCanExecuteClick, CanExecuteCanExecuteClick);

        private bool CanExecuteCanExecuteClick()
        {
            return IsChecked;
        }

        private void ExecuteCanExecuteClick()
        {
            Debug.WriteLine("Can Execute Click Command");
        }

        
        public ICommand ClickWithParameter => _clickWithParameterCommand ??= new DelegateCommand<string>(ExecuteClickWithParameter, CanExecuteClickWithParameter);

        private bool CanExecuteClickWithParameter(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        private void ExecuteClickWithParameter(string value)
        {
            Debug.WriteLine($"Can Execute Click Command with parameter: {value}");
        }

        
        private ICommand _windowLoadedCommand;

        public ICommand WindowLoaded => _windowLoadedCommand ??= new DelegateCommand<RoutedEventArgs>(ExecuteWindowLoaded);

        private void ExecuteWindowLoaded(RoutedEventArgs args)
        {
            Debug.WriteLine($"{((Window)args.OriginalSource).Title} loaded.");
        }


    }
}
