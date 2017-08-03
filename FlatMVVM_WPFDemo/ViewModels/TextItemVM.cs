using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlatMVVM;

namespace FlatMVVM_WPFDemo.ViewModels
{
    public class TextItemVM : FlatVM
    {
        private string _text;

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value; 
                OnPropertyChanged();
            }
        }

    }
}
