using System.Windows.Input;

namespace TT.FlatMVVM.Core
{

    /// <summary>
    /// Defines a factory interface for creating <see cref="ICommand"/> instances with various signatures.
    /// </summary>
    public interface ICommandFactory
    {

        /// <summary>
        /// Creates a command that executes the specified action and optionally evaluates whether it can execute.
        /// </summary>
        /// <param name="execute">The action to execute when the command is invoked.</param>
        /// <param name="canExecute">An optional function to determine whether the command can execute.</param>
        /// <returns>A new <see cref="ICommand"/> instance.</returns>
        ICommand Create(Action execute, Func<bool>? canExecute = null);

        /// <summary>
        /// Creates a generic command that executes the specified action with a parameter and optionally evaluates whether it can execute.
        /// </summary>
        /// <typeparam name="T">The type of the parameter passed to the execute action.</typeparam>
        /// <param name="execute">The action to execute when the command is invoked.</param>
        /// <param name="canExecute">An optional function to determine whether the command can execute with the parameter.</param>
        /// <returns>A new <see cref="ICommand"/> instance.</returns>
        ICommand Create<T>(Action<T?> execute, Func<T?, bool>? canExecute = null);

        /// <summary>
        /// Creates a command with two parameters that executes the specified action and evaluates whether it can execute.
        /// </summary>
        /// <typeparam name="T1">The type of the first parameter passed to the execute action.</typeparam>
        /// <typeparam name="T2">The type of the second parameter passed to the execute action.</typeparam>
        /// <param name="execute">The action to execute when the command is invoked.</param>
        /// <param name="canExecute">A function to determine whether the command can execute with the parameters.</param>
        /// <returns>A new <see cref="ICommand"/> instance.</returns>
        ICommand Create<T1, T2>(Action<T1?, T2?> execute, Func<T1?, T2?, bool>? canExecute = null);

    }
}
