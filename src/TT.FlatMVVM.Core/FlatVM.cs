using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace TT.FlatMVVM.Core
{
    /// <summary>
    /// ViewModel base class.
    /// </summary>
    public abstract class FlatVM : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Called when property changed.
        /// </summary>
        /// <param name="propertyName">Name of changed property.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Call to raise PropertyChanged event.
        /// </summary>
        /// <param name="sender">Name of changed property.</param>
        /// <param name="propertyChangedEventArgs">Property changed event arguments.</param>
        protected virtual void OnPropertyChanged(object? sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            PropertyChanged?.Invoke(sender, propertyChangedEventArgs);
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        #endregion

        #region Services

        /// <summary>
        /// Sets value target property.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property.</typeparam>
        /// <param name="store">Reference backing field property may be changed.</param>
        /// <param name="value">The new value shall be assigned property.</param>
        /// <param name="propertyName">Name of changed property.</param>
        /// <returns>Return true if property value has changed; otherwise false.</returns>
        /// <remarks>
        /// The current approach is to check whether the value that should be assigned to the property is equal the current value.
        /// If true this method returns false and no PropertyChanged event is raised. Else it assigns the new value, raises the PropertyChanged event and returns true.
        /// </remarks>
        protected virtual bool SetProperty<TProperty>(ref TProperty store, TProperty value, [CallerMemberName] string propertyName = "")
        {
            return SetProperty(ref store, value, null, propertyName);
        }

        /// <summary>
        /// Sets value target property and raises changes for additional properties.
        /// </summary>
        /// <typeparam name="TProperty">Type of the property.</typeparam>
        /// <param name="store">Reference backing field property may be changed.</param>
        /// <param name="value">The new value shall be assigned property.</param>
        /// <param name="linkedProperties">A list additional property names PropertyChanged event should be raised.</param>
        /// <param name="propertyName">Name of changed property.</param>
        /// <returns>Return true if property value has changed; otherwise false.</returns>
        /// <remarks>
        /// The current approach is to check whether the value that should be assigned to the property is equal the current value.
        /// If true this method returns false and no PropertyChanged event is raised. Else it assigns the new value, raises the PropertyChanged event and returns true.
        /// </remarks>
        protected virtual bool SetProperty<TProperty>(ref TProperty store, TProperty value, IEnumerable<string>? linkedProperties, [CallerMemberName] string propertyName = "")
        {
            if (EqualityComparer<TProperty>.Default.Equals(store, value))
                return false;

            store = value;
            OnPropertyChanged(propertyName);

            if (linkedProperties == null)
                return true;

            foreach (string linkedProperty in linkedProperties)
                OnPropertyChanged(linkedProperty);

            return true;
        }

        /// <summary>
        /// Refresh all bindings connected this ViewModel.
        /// </summary>
        public void RefreshAllBindings()
        {
            OnPropertyChanged(string.Empty);
        }

        #endregion
    }
}
