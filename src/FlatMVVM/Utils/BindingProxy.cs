using System.Windows;

namespace TT.FlatMVVM.Utils
{

    /// <summary>
    /// BindingProxy
    /// </summary>
    public class BindingProxy : Freezable
    {
        /// <summary>
        /// Creates a new instance of the <see cref="T:System.Windows.Freezable" /> derived class.
        /// </summary>
        /// <returns>The new instance.</returns>
        protected override Freezable CreateInstanceCore()
        {
            return new BindingProxy();
        }

        /// <summary>
        /// Data object dependency property
        /// </summary>
        public static readonly DependencyProperty DataProperty = DependencyProperty.Register("Data", typeof(object), typeof(BindingProxy), new PropertyMetadata(null));

        /// <summary>
        /// Get set data object of binding proxy
        /// </summary>
        public object Data
        {
            get => GetValue(DataProperty);
            set => SetValue(DataProperty, value);
        }

    }

}
