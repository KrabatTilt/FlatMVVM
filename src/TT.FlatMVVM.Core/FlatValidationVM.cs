using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TT.FlatMVVM.Core
{
    /// <summary>
    /// ViewModel base class with validation support.
    /// </summary>
    public abstract class FlatValidationVM : FlatVM, INotifyDataErrorInfo
    {
        #region Fields

        private readonly Dictionary<string, string[]> _validationErrors = new(StringComparer.Ordinal);

        #endregion

        #region Properties

        /// <summary>
        /// Gets value indicates entity validation errors.
        /// </summary>
        /// <returns>true if the entity currently has validation errors; otherwise, false.</returns>
        public bool HasErrors => _validationErrors.Count != 0;

        #endregion

        #region Events

        /// <summary>
        /// Call to raise ErrorsChanged event.
        /// </summary>
        /// <param name="propertyName">Name of changed property errors.</param>
        protected virtual void OnErrorsChanged(string propertyName)
        {
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs validation errors changed for property or for entire entity.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        #endregion

        #region Services

        /// <summary>
        /// Sets value target property and validates it.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property.</typeparam>
        /// <param name="store">Reference backing field property may be changed.</param>
        /// <param name="value">The new value shall be assigned property.</param>
        /// <param name="validationRule">Property validation rule.</param>
        /// <param name="propertyName">Name of changed property.</param>
        /// <returns>Return true if property value has changed; otherwise false.</returns>
        protected virtual bool SetProperty<TProperty>(ref TProperty store, TProperty value, Func<TProperty, IEnumerable<string>> validationRule, [CallerMemberName] string propertyName = "")
        {
            return SetProperty(ref store, value, null, validationRule, propertyName);
        }

        /// <summary>
        /// Sets value target property, validates it, and raises changes for additional properties.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property.</typeparam>
        /// <param name="store">Reference backing field property may be changed.</param>
        /// <param name="value">The new value shall be assigned property.</param>
        /// <param name="linkedProperties">A list additional property names PropertyChanged event should be raised.</param>
        /// <param name="validationRule">Property validation rule.</param>
        /// <param name="propertyName">Name of changed property.</param>
        /// <returns>Return true if property value has changed; otherwise false.</returns>
        protected virtual bool SetProperty<TProperty>(ref TProperty store, TProperty value, IEnumerable<string>? linkedProperties, Func<TProperty, IEnumerable<string>> validationRule, [CallerMemberName] string propertyName = "")
        {
            string[] validationResult = validationRule(value).ToArray();

            SetErrors(propertyName, validationResult);

            return SetProperty(ref store, value, linkedProperties, propertyName);
        }

        /// <summary>
        /// Gets validation errors for the specified property or entire entity.
        /// </summary>
        /// <param name="propertyName">The name of property to retrieve validation errors for; null or <see cref="string.Empty" /> retrieves all errors.</param>
        /// <returns>The validation errors for the property or entity.</returns>
        public IEnumerable GetErrors(string? propertyName)
        {
            if(string.IsNullOrEmpty(propertyName))
                propertyName = string.Empty; // Treat null or empty property name as the entire entity

            return _validationErrors.TryGetValue(propertyName, out string[]? value) ? value.ToArray() : [];
        }

        /// <summary>
        /// Sets validation errors for the specified property.
        /// </summary>
        /// <param name="propertyName">Name of property to set validation errors for.</param>
        /// <param name="errors">Validation errors to set.</param>
        protected void SetErrors(string propertyName, IEnumerable<string> errors)
        {
            string[] errorSnapshot = errors.ToArray();

            if (errorSnapshot.Length == 0)
            {
                ClearErrors(propertyName);
                return;
            }

            bool hadErrors = HasErrors;

            if (_validationErrors.TryGetValue(propertyName, out string[]? existingErrors) && existingErrors.SequenceEqual(errorSnapshot))
                return;

            _validationErrors[propertyName] = errorSnapshot;
            OnErrorsChanged(propertyName);

            if (hadErrors != HasErrors)
                OnPropertyChanged(nameof(HasErrors));
        }

        /// <summary>
        /// Clears validation errors for the specified property.
        /// </summary>
        /// <param name="propertyName">Name of property to clear validation errors for.</param>
        private void ClearErrors(string propertyName)
        {
            bool hadErrors = HasErrors;

            if (!_validationErrors.Remove(propertyName))
                return;

            OnErrorsChanged(propertyName);

            if (hadErrors != HasErrors)
                OnPropertyChanged(nameof(HasErrors));
        }

        #endregion
    }
}


