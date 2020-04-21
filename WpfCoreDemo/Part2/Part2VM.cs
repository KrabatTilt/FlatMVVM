using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using TT.FlatMVVM;

namespace WpfCoreDemo.Part2
{
    internal class Part2VM : FlatVM
    {

        private ICommand _noCanExecuteCheckCommand;
        private ICommand _printTextCommandCommand;
        private ICommand _singleParameterCommandCommand;
        private ICommand _mouseMoveCommandCommand;


        public ICommand NoCanExecuteCheckCommand => _noCanExecuteCheckCommand ??= new DelegateCommand(ExecuteWindowLoadedCommand);

        private static void ExecuteWindowLoadedCommand()
        {
            Debug.WriteLine("Window loaded");
        }


        public ICommand ParameterlessCommand => _printTextCommandCommand ??= new DelegateCommand(ExecutePrintTextCommand, CanExecutePrintTextCommand);

        private static bool CanExecutePrintTextCommand()
        {
            return true;
        }

        private static void ExecutePrintTextCommand()
        {
            Debug.WriteLine("Button clicked");
        }


        public ICommand SingleParameterCommand => _singleParameterCommandCommand ??= new DelegateCommand<bool>(ExecuteSingleParameterCommand, CanExecuteSingleParameterCommand);

        private static bool CanExecuteSingleParameterCommand(bool isChecked)
        {
            return true;
        }

        private static void ExecuteSingleParameterCommand(bool isChecked)
        {
            Debug.WriteLine($"CheckBox is {(isChecked ? "checked" : "not checked")}");
        }


        public ICommand MouseMoveCommand => _mouseMoveCommandCommand ??= new DelegateCommand<MouseEventArgs, bool>(ExecuteMouseMoveCommand, CanExecuteMouseMoveCommand);

        private static bool CanExecuteMouseMoveCommand((MouseEventArgs ma, bool ischecked) values)
        {
            return true;    
        }

        private static void ExecuteMouseMoveCommand((MouseEventArgs args, bool ischecked) values)
        {
            Debug.WriteLine($"EventType:{values.args.GetType().Name}, IsChecked: {values.ischecked}");
        }


    }
}
