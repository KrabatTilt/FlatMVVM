﻿using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace FlatMVVM.Utils
{
    /// <summary>
    /// TriggerAction which links events to commands.
    /// </summary>
    public class TriggerCommandAction : TriggerAction<DependencyObject>
    {

        #region Dependency Properties

        public static readonly DependencyProperty CommandProperty = DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(TriggerCommandAction), new FrameworkPropertyMetadata((ICommand)null));

        /// <summary>
        /// Command that should be called when event is triggered.
        /// </summary>
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public static readonly DependencyProperty CommandParameterProperty = DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(TriggerCommandAction), new FrameworkPropertyMetadata((object)null));

        /// <summary>
        /// CommandParameter which should be passed with the Command.
        /// </summary>
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        #endregion

        /// <summary>
        /// Invokes the action.
        /// </summary>
        /// <param name="parameter">The parameter to the action. If the action does not require a parameter, the parameter may be set to a null reference.</param>
        protected override void Invoke(object parameter)
        {
            ICommand command = Command;
            object commandParameter = CommandParameter;

            if (commandParameter != null)
                parameter = commandParameter;

            if (command?.CanExecute(parameter) == true)
                command.Execute(parameter);
        }

    }
}
