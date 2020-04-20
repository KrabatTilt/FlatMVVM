using System;
using System.Windows.Input;

namespace TT.FlatMVVM
{

    /// <summary>
    /// 
    /// </summary>
    public abstract class CommandBase : ICommand
    {

        /// <summary>
        /// Occurs when changes occur that affect whether or not the command should execute.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }

        /// <summary>
        /// Defines the conditions that determine whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public abstract bool CanExecute(object parameter);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public abstract void Execute(object parameter);

    }


    /// <summary>
    /// Basic RelayCommand
    /// </summary>
    public class DelegateCommand : CommandBase
    {

        #region Fields

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/>.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Predicate to check before execution.</param>
        public DelegateCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), "Cannot be null.");
            _canExecute = canExecute;
        }
        #endregion

        #region ICommand Members

        /// <summary>
        /// Defines the conditions that determine whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object parameter) => _canExecute == null || _canExecute();

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public override void Execute(object parameter) => _execute();

        #endregion

    }

    /// <summary>
    /// RelayCommand with generic 'Execute' and 'CanExecute' command parameter types.
    /// </summary>
    /// <typeparam name="TParam">Type of execute command parameter.</typeparam>
    /// <typeparam name="TCExParam">Type of can execute command parameter.</typeparam>
    public class DelegateCommand<TParam> : CommandBase
    {

        #region Fields

        private readonly Action<TParam> _execute;
        private readonly Func<TParam, bool> _canExecute;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/>.
        /// </summary>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Predicate to check before execution.</param>
        public DelegateCommand(Action<TParam> execute, Func<TParam, bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute), "Cannot be null.");
            _canExecute = canExecute;
        }

        #endregion

        #region ICommand Members

        /// <summary>
        /// Defines the conditions that determine whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public override bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute((TParam)parameter);
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public override void Execute(object parameter)
        {
            _execute((TParam)parameter);
        }

        #endregion

    }

}
