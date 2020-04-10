namespace TT.FlatMVVM.ControlVM
{

    /// <summary>
    /// FlatViewModel for ContextMenu entries
    /// </summary>
    public class MenuItemVM : FlatVM
    {

        #region Fields

        private string _header;

        #endregion

        #region Properties

        /// <summary>
        /// Get/Set item header.
        /// </summary>
        public string Header
        {
            get => _header;
            set
            {
                _header = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        #region Construction

        /// <summary>
        /// Create new instance of MenuItemVM with given header string.
        /// </summary>
        /// <param name="header">String header of menu item</param>
        public MenuItemVM(string header)
        {
            Header = header;
        }

        #endregion

    }
}
