using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace FlatMVVM
{

    public abstract class FlatVM : INotifyPropertyChanged, INotifyDataErrorInfo
    {

        #region Fields

        private readonly ConcurrentDictionary<string, ICollection<string>> _validationErrors = new ConcurrentDictionary<string, ICollection<string>>();

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value that indicates whether the entity has validation errors.
        /// </summary>
        /// <returns>true if the entity currently has validation errors; otherwise, false.</returns>
        public bool HasErrors => _validationErrors.Count > 0;

        #endregion

        #region Events

        /// <summary>
        /// Occurs when the initialization is completed.
        /// </summary>
        public event AsyncCompletedEventHandler InitializationCompleted;

        /// <summary>
        /// Called when a property has changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnPropertyChanged(Func<ICollection<string>> propertyValidation, [CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            ValidatePropertyAsync(propertyName, propertyValidation);
        }

        /// <summary>
        /// Called when a property has changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="propertyChangedEventArgs"></param>
        protected virtual void OnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            PropertyChanged?.Invoke(sender, propertyChangedEventArgs);
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion

        #region Construction/Initialization

        /// <summary>
        /// Initializes a new instance of the <see cref="FlatVM"/> class.
        /// </summary>
        protected FlatVM()
        {
            // Initialization is done async to preserve UI thread.
            Task.Factory.StartNew(Initialize).ContinueWith(InitializationCompletedCallback, TaskContinuationOptions.ExecuteSynchronously);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        protected virtual void Initialize()
        {
        }

        /// <summary>
        /// Callback method for the async initialization.
        /// </summary>
        /// <param name="result">The result.</param>
        private void InitializationCompletedCallback(IAsyncResult result)
        {
            InitializationCompleted?.Invoke(this, new AsyncCompletedEventArgs(null, !result.IsCompleted, result.AsyncState));
        }

        #endregion

        #region Services

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire entity.
        /// </summary>
        /// <returns>The validation errors for the property or entity.</returns>
        /// <param name="propertyName">The name of the property to retrieve validation errors for; or null or <see cref="F:System.String.Empty" />, to retrieve entity-level errors.</param>
        public IEnumerable GetErrors(string propertyName)
        {
            if (string.IsNullOrEmpty(propertyName) || !_validationErrors.ContainsKey(propertyName))
                return null;

            return _validationErrors[propertyName];
        }

        /// <summary>
        /// Refresh all bindings of this ViewModel.
        /// </summary>
        public void RefreshAllBindings()
        {
            OnPropertyChanged(string.Empty);
        }

        #endregion

        #region Private

        /// <summary>
        /// Validates a property asynchronously on background thread pool.
        /// </summary>
        /// <param name="propertyName">Name of property to validate.</param>
        /// <param name="propertyValidation">Property validation function.</param>
        /// <returns>Validation task.</returns>
        private async void ValidatePropertyAsync(string propertyName, Func<ICollection<string>> propertyValidation)
        {
            ICollection<string> errors = await Task.Run(() => propertyValidation.Invoke());

            bool changed;
            if (errors.Any())
            {
                _validationErrors[propertyName] = errors;
                changed = true;
            }
            else
            {
                changed = _validationErrors.TryRemove(propertyName, out errors);
            }

            // Raise event only when there were changes
            if (changed)
                OnErrorsChanged(propertyName);
        }

        #endregion

    }
}
