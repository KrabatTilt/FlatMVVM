namespace FlatMVVM.ControlVM
{

    /// <summary>
    /// FlatVM for TabControl entries.
    /// </summary>
    public class TabControlVM : FlatVM
    {

        private string _header;
        private FlatVM _content;

        public string Header
        {
            get { return _header; }
            set
            {
                _header = value; 
                OnPropertyChanged();
            }
        }

        public FlatVM Content
        {
            get { return _content; }
            set
            {
                _content = value;
                OnPropertyChanged();
            }
        }

        public TabControlVM(string header, FlatVM content)
        {
            Header = header;
            Content = content;
        }

    }
}
