namespace TT.FlatMVVM.ControlVM
{

    /// <summary>
    /// FlatVM for TabControl entries.
    /// </summary>
    public class TabControlVM : FlatVM
    {

        private object _header;
        private FlatVM _content;

        /// <summary>
        /// Header object of tab control view model
        /// </summary>
        public object Header
        {
            get => _header;
            set
            {
                _header = value; 
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Content view model of tab control view model.
        /// </summary>
        public FlatVM Content
        {
            get => _content;
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header">String header </param>
        /// <param name="content">Content used as tabitem content view model</param>
        public TabControlVM(string header, FlatVM content)
        {
            Header = header;
            Content = content;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="content">A content view model used in header and content.</param>
        public TabControlVM(FlatVM content)
        {
            Header = content;
            Content = content;
        }

    }
}
