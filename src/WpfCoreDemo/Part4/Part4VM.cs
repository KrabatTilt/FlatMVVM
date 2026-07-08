using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TT.FlatMVVM.Core;

namespace WpfCoreDemo.Part4
{
    internal sealed class Part4VM : FlatVM, IDemoCase
    {

        private readonly ICommandFactory _commandFactory;

        public string Name { get; }

        public ResourceDictionary Templates { get; }

        public bool IsChecked
        {
            get;
            set => SetProperty(ref field, value);
        }

        public string? ParamText
        {
            get;
            set => SetProperty(ref field, value);
        }


        public Part4VM(ICommandFactory commandFactory)
        {
            _commandFactory = commandFactory;

            Name = "Commands";
            Templates = new ResourceDictionary { Source = new Uri("/WpfCoreDemo;component/Part4/Part4DataTemplates.xaml", UriKind.RelativeOrAbsolute) };

            ParamText = "Change me";
        }

        
        public ICommand SimpleClick => field ??= _commandFactory.Create(ExecuteSimpleClickCommand);

        private void ExecuteSimpleClickCommand()
        {
            Debug.WriteLine("Simple Click Command");
        }

        
        public ICommand CanExecuteClick => field ??= _commandFactory.Create(ExecuteCanExecuteClick, CanExecuteCanExecuteClick);

        private bool CanExecuteCanExecuteClick()
        {
            return IsChecked;
        }

        private void ExecuteCanExecuteClick()
        {
            Debug.WriteLine("Can Execute Click Command");
        }

        
        public ICommand ClickWithParameter => field ??= _commandFactory.Create<string>(ExecuteClickWithParameter, CanExecuteClickWithParameter);

        private static bool CanExecuteClickWithParameter(string? value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        private static void ExecuteClickWithParameter(string? value)
        {
            Debug.WriteLine($"Can Execute Click Command with parameter: {value}");
        }

        public ICommand WindowLoaded => field ??= _commandFactory.Create<RoutedEventArgs>(ExecuteWindowLoaded);

        private static void ExecuteWindowLoaded(RoutedEventArgs? args)
        {
            if(args == null)
                return;

            Debug.WriteLine($"{((UserControl)args.OriginalSource).Name} loaded.");
        }
      
    }
}
