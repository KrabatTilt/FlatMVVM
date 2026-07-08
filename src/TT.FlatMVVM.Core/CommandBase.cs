using System.Windows.Input;

namespace TT.FlatMVVM.Core
{

    /// <summary>
    /// Represents the base abstract implementation of the <see cref="ICommand"/> interface,
    /// utilizing an <see cref="ICommandEventStrategy"/> to manage <see cref="CanExecuteChanged"/> event handling.
    /// </summary>
    public abstract class CommandBase : ICommand
    {
        private readonly ICommandEventStrategy _strategy;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandBase"/> class with the specified event strategy.
        /// </summary>
        /// <param name="strategy">The strategy used to handle <see cref="CanExecuteChanged"/> event subscriptions and notifications.</param>
        protected CommandBase(ICommandEventStrategy strategy)
        {
            _strategy = strategy;
        }

        /// <summary>
        /// Occurs when changes occur that affect whether the command should execute.
        /// Handled by the configured <see cref="ICommandEventStrategy"/>.
        /// </summary>
        public event EventHandler? CanExecuteChanged
        {
            add => _strategy.AddHandler(value);
            remove => _strategy.RemoveHandler(value);
        }

        /// <summary>
        /// Raises the <see cref="CanExecuteChanged"/> event to notify subscribers that the execution status has changed.
        /// </summary>
        public void RaiseCanExecuteChanged()
        {
            _strategy.RaiseCanExecuteChanged(this);
        }

        /// <summary>
        /// Determines whether the command can execute in its current state.
        /// </summary>
        /// <param name="parameter">Data used by the command. Can be <c>null</c>.</param>
        /// <returns><c>true</c> if the command can execute; otherwise, <c>false</c>.</returns>
        public abstract bool CanExecute(object? parameter);

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. Can be <c>null</c>.</param>
        public abstract void Execute(object? parameter);
    }
}
