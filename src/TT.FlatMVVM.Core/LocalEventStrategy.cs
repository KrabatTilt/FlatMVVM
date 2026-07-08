namespace TT.FlatMVVM.Core
{

    /// <summary>
/// Implements a local event strategy by managing event handlers internally
/// and invoking them directly when raising CanExecuteChanged.
/// </summary>
    public class LocalEventStrategy : ICommandEventStrategy
    {

        private EventHandler? _handler;

        /// <inheritdoc />
        public void AddHandler(EventHandler? handler) => _handler += handler;

        /// <inheritdoc />
        public void RemoveHandler(EventHandler? handler) => _handler -= handler;

        /// <inheritdoc />
        public void RaiseCanExecuteChanged(object? sender) => _handler?.Invoke(sender, EventArgs.Empty);

    }
}
