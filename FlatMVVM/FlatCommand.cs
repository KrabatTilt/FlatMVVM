using System;
using System.Diagnostics;
using System.Windows.Input;

namespace FlatMVVM
{

    /// <summary>
    /// Basic RelayCommand
    /// </summary>
    public class FlatCommand : ICommand
    {

        #region Fields

        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public FlatCommand(Action<object> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatCommand"/>.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Predicate to check before execution.</param>
        public FlatCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException(nameof(execute), "Cannot be null");

            _execute = execute;
            _canExecute = canExecute;
        }
        #endregion

        #region ICommand Members

        /// <summary>
        /// Defines the conditions that determine whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            _execute(parameter);
        }

        #endregion

    }

    /// <summary>
    /// RelayCommand with generic 'Execute' and 'CanExecute' command parameter types.
    /// </summary>
    /// <typeparam name="TExParam">Type of execue command parameter.</typeparam>
    /// <typeparam name="TCExParam">Type of can execute command parameter.</typeparam>
    public class FlatCommand<TExParam, TCExParam> : ICommand
    {

        #region Fields

        private readonly Action<TExParam> _execute;
        private readonly Predicate<TCExParam> _canExecute;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatCommand"/> class.
        /// </summary>
        /// <param name="execute">The execute.</param>
        public FlatCommand(Action<TExParam> execute)
            : this(execute, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatCommand"/>.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Predicate to check before execution.</param>
        public FlatCommand(Action<TExParam> execute, Predicate<TCExParam> canExecute)
        {
            if (execute == null)
                throw new ArgumentNullException("execute", "Cannot be null");

            _execute = execute;
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        /// <summary>
        /// Defines the conditions that determine whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        [DebuggerStepThrough]
        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((TCExParam)parameter);
        }

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public void Execute(object parameter)
        {
            _execute((TExParam)parameter);
        }

        #endregion

    }

}
