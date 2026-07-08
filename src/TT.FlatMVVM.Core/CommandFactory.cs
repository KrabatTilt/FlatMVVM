using System.Windows.Input;

namespace TT.FlatMVVM.Core
{

    /// <summary>
    /// Factory class responsible creating instances <see cref="ICommand"/>.
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        private readonly ICommandEventStrategy _commandEventStrategy;

        /// <summary>
        /// Initializes new instance <see cref="CommandFactory"/> class specified command event strategy.
        /// </summary>
        /// <param name="commandEventStrategy">The event strategy used by created commands.</param>
        public CommandFactory(ICommandEventStrategy commandEventStrategy)
        {
            _commandEventStrategy = commandEventStrategy;
        }

        /// <inheritdoc />
        public ICommand Create(Action execute, Func<bool>? canExecute = null)
        {
            return new DelegateCommand(_commandEventStrategy, execute, canExecute);
        }


        /// <inheritdoc />
        public ICommand Create<T>(Action<T?> execute, Func<T?, bool>? canExecute = null)
        {
            return new DelegateCommand<T>(_commandEventStrategy, execute, canExecute);
        }

        /// <inheritdoc />
        public ICommand Create<T1, T2>(Action<T1?, T2?> execute, Func<T1?, T2?, bool>? canExecute = null)
        {
            return new DelegateCommand<T1, T2>(_commandEventStrategy, execute, canExecute);
        }

    }
}
