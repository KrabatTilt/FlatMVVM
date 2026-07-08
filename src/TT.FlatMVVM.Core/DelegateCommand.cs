namespace TT.FlatMVVM.Core
{

    /// <summary>
    /// Basic DelegateCommand without any parameter.
    /// </summary>
    public class DelegateCommand : CommandBase
    {

        #region Fields

        private readonly Action _execute;
        private readonly Func<bool>? _canExecute;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/>.
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Predicate to check before execution.</param>
        public DelegateCommand(ICommandEventStrategy strategy, Action execute, Func<bool>? canExecute = null)
            : base(strategy)
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
        public override bool CanExecute(object? parameter) => _canExecute == null || _canExecute();

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public override void Execute(object? parameter) => _execute();

        #endregion

    }


    /// <summary>
    /// DelegateCommand with generic 'Execute' and 'CanExecute' command parameter type.
    /// </summary>
    /// <typeparam name="TParam">Type of command parameter.</typeparam>
    public class DelegateCommand<TParam> : CommandBase
    {

        #region Fields

        private readonly Action<TParam?> _execute;
        private readonly Func<TParam?, bool>? _canExecute;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/>.
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Predicate to check before execution.</param>
        public DelegateCommand(ICommandEventStrategy strategy, Action<TParam?> execute, Func<TParam?, bool>? canExecute = null)
            : base(strategy)
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
        public override bool CanExecute(object? parameter)
        {
            return CommandParameterConverter.TryConvert(parameter, out TParam? typedParameter)
                   && (_canExecute == null || _canExecute(typedParameter));
}

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public override void Execute(object? parameter)
        {
            var typedParameter = CommandParameterConverter.ConvertOrThrow<TParam>(parameter, nameof(parameter));
            _execute(typedParameter);
        }

        #endregion

    }


    /// <summary>
    /// DelegateCommand with generic 'Execute' and 'CanExecute' event and command parameter type.
    /// </summary>
    /// <typeparam name="TParam1">Type of event parameter passed by TriggerAction.</typeparam>
    /// <typeparam name="TParam2">Type of command parameter passed by TriggerAction.</typeparam>
    public class DelegateCommand<TParam1, TParam2> : CommandBase
    {

        #region Fields

        private readonly Action<TParam1?, TParam2?> _execute;
        private readonly Func<TParam1?, TParam2?, bool>? _canExecute;

        #endregion

        #region Construction

        /// <summary>
        /// Initializes a new instance of the <see cref="DelegateCommand"/>.
        /// </summary>
        /// <param name="strategy"></param>
        /// <param name="execute">Action to execute.</param>
        /// <param name="canExecute">Predicate to check before execution.</param>
        public DelegateCommand(
            ICommandEventStrategy strategy,
            Action<TParam1?, TParam2?> execute,
            Func<TParam1?, TParam2?, bool>? canExecute = null)
            : base(strategy)
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
        public override bool CanExecute(object? parameter)
        {
            return CommandParameterConverter.TryConvertTuple(parameter, out TParam1? first, out TParam2? second)
                   && (_canExecute == null || _canExecute(first, second));
        }
        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command.  If the command does not require data to be passed, this object can be set to null.</param>
        public override void Execute(object? parameter)
        {
            (TParam1? first, TParam2? second) = CommandParameterConverter.ConvertTupleOrThrow<TParam1, TParam2>(
                parameter,
                nameof(parameter));
            _execute(first, second);
        }

        #endregion

    }

}
