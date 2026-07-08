using System.Diagnostics;
using System.Windows;
using System.Windows.Input;
using TT.FlatMVVM.Core;

namespace WpfCoreDemo.Part2
{
    internal sealed class Part2VM : FlatVM, IDemoCase
    {

        private readonly ICommandFactory _commandFactory;


        public Part2VM(ICommandFactory commandFactory)
        {
            Name = "Event Bindings";
            Templates = new ResourceDictionary { Source = new Uri("/WpfCoreDemo;component/Part2/Part2DataTemplates.xaml", UriKind.RelativeOrAbsolute) };

            _commandFactory = commandFactory;
        }


        public ICommand NoCanExecuteCheckCommand => field ??= _commandFactory.Create(ExecuteWindowLoadedCommand);

        private static void ExecuteWindowLoadedCommand()
        {
            Debug.WriteLine("Window loaded");
        }

        public ICommand ParameterlessCommand => field ??= _commandFactory.Create(ExecutePrintTextCommand, CanExecutePrintTextCommand);

        private static bool CanExecutePrintTextCommand()
        {
            return true;
        }

        private static void ExecutePrintTextCommand()
        {
            Debug.WriteLine("Button clicked");
        }


        public ICommand SingleParameterCommand => field ??= _commandFactory.Create<bool>(ExecuteSingleParameterCommand, CanExecuteSingleParameterCommand);

        private static bool CanExecuteSingleParameterCommand(bool isChecked)
        {
            return true;
        }

        private static void ExecuteSingleParameterCommand(bool isChecked)
        {
            Debug.WriteLine($"CheckBox is {(isChecked ? "checked" : "not checked")}");
        }


        public ICommand MouseMoveCommand => field ??= _commandFactory.Create<MouseEventArgs, bool>(ExecuteMouseMoveCommand, CanExecuteMouseMoveCommand);

        private static bool CanExecuteMouseMoveCommand(MouseEventArgs? ma, bool isChecked)
        {
            return true;
        }

        private static void ExecuteMouseMoveCommand(MouseEventArgs? args, bool isChecked)
        {
            Debug.WriteLine($"EventType:{args?.GetType().Name}, IsChecked: {isChecked}");
        }


        public ICommand CustomEventCommand => field ??= _commandFactory.Create<CustomEvent1Args>(ExecuteCustomEventCommand, CanExecuteCustomEventCommand);

        private bool CanExecuteCustomEventCommand(CustomEvent1Args? e)
        {
            return true;
        }

        private void ExecuteCustomEventCommand(CustomEvent1Args? e)
        {
            Debug.WriteLine(e?.Position);
        }


        public string Name { get; }

        public ResourceDictionary Templates { get; }
    }
}
