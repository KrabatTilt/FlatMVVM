using System.Windows.Input;
using TT.FlatMVVM.Core;

namespace TT.FlatMVVM.Wpf
{

    /// <summary>
    /// Implements an event strategy for WPF by using the <see cref="System.Windows.Input.CommandManager"/>
    /// to manage CanExecuteChanged event subscriptions and notifications.
    /// </summary>
    public class WpfEventStrategy : ICommandEventStrategy
    {
        /// <inheritdoc />
        public void AddHandler(EventHandler? handler) => CommandManager.RequerySuggested += handler;

        /// <inheritdoc />
        public void RemoveHandler(EventHandler? handler) => CommandManager.RequerySuggested -= handler;

        /// <inheritdoc />
        public void RaiseCanExecuteChanged(object? sender) => CommandManager.InvalidateRequerySuggested();
    }
}
