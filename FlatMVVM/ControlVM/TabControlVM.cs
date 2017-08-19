﻿namespace FlatMVVM.ControlVM
{

    /// <summary>
    /// FlatVM for TabControl entries.
    /// </summary>
    public class TabControlVM : FlatVM
    {

        private object _header;
        private FlatVM _content;

        public object Header
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

        public TabControlVM(FlatVM content)
        {
            Header = content;
            Content = content;
        }

    }
}
