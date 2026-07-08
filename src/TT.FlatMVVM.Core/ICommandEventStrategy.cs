namespace TT.FlatMVVM.Core
{

    /// <summary>
    /// Defines a strategy for managing CanExecuteChanged event handlers
    /// and raising the event for commands.
    /// </summary>
    public interface ICommandEventStrategy
    {

        /// <summary>
        /// Adds an event handler to the CanExecuteChanged event invocation list.
        /// </summary>
        /// <param name="handler">The event handler to add. Can be <c>null</c>.</param>
        void AddHandler(EventHandler? handler);

        /// <summary>
        /// Removes an event handler from the CanExecuteChanged event invocation list.
        /// </summary>
        /// <param name="handler">The event handler to remove. Can be <c>null</c>.</param>
        void RemoveHandler(EventHandler? handler);

        /// <summary>
        /// Raises the CanExecuteChanged event, notifying subscribers of a change.
        /// </summary>
        /// <param name="sender">The source of the event. Can be <c>null</c>.</param>
        void RaiseCanExecuteChanged(object? sender);
    }
}
