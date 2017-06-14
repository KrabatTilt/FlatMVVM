namespace FlatMVVM.ControlVM
{
    public class MenuItemVM : FlatVM
    {

        #region Fields

        private string _header;

        #endregion

        #region Properties

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value; 
                OnPropertyChanged();
            }
        }

        #endregion

        #region Construction

        public MenuItemVM(string header)
        {
            Header = header;
        }

        #endregion

    }
}
