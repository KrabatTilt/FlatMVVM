using System.Windows.Media;
using FlatMVVM;

namespace FlatMVVM_WPFDemo.ViewModels
{
    public class ColoredItemVM : FlatVM
    {
        private Brush _brush;

        public Brush Brush
        {
            get { return _brush; }
            set
            {
                _brush = value;
                OnPropertyChanged();
            }
        }
    }
}
