using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace FlatMVVM.ControlVM
{
    public class RectangleVM : FlatVM
    {

        #region Fields

        private double _x;
        private double _y;
        private double _width;
        private double _height;
        private double _minWidth;
        private double _minHeight;
        private Visibility _visibility;
        private Brush _fill;
        private double _strokeThickness;
        private string _tooltip;
        private bool _limitToParent;
        private bool _isModifiable;
        private Rect _limits;
        private Brush _stroke;
        private bool _isSelected;

        private ICommand _selectCommand;

        #endregion

        #region Properties

        public double X
        {
            get { return _x; }
            set
            {
                _x = value;
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get { return _y; }
            set
            {
                _y = value;
                OnPropertyChanged();
            }
        }

        public double Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get { return _height; }
            set
            {
                _height = value;
                OnPropertyChanged();
            }
        }

        public double MinWidth
        {
            get { return _minWidth; }
            set
            {
                _minWidth = value;
                OnPropertyChanged();
            }
        }

        public double MinHeight
        {
            get { return _minHeight; }
            set
            {
                _minHeight = value;
                OnPropertyChanged();
            }
        }

        public double StrokeThickness
        {
            get { return _strokeThickness; }
            set
            {
                _strokeThickness = value;
                OnPropertyChanged();
            }
        }

        public Visibility Visibility
        {
            get { return _visibility; }
            set
            {
                _visibility = value;
                OnPropertyChanged();
            }
        }

        public Brush Stroke
        {
            get { return _stroke; }
            set
            {
                _stroke = value;
                OnPropertyChanged();
            }
        }

        public Brush Fill
        {
            get { return _fill; }
            set
            {
                _fill = value;
                OnPropertyChanged();
            }
        }

        public string Tooltip
        {
            get { return _tooltip; }
            set
            {
                _tooltip = value;
                OnPropertyChanged();
            }
        }

        public bool LimitToParent
        {
            get { return _limitToParent; }
            set
            {
                _limitToParent = value;
                OnPropertyChanged();
            }
        }

        public bool IsModifiable
        {
            get { return _isModifiable; }
            set
            {
                _isModifiable = value;
                OnPropertyChanged();
            }
        }

        public Rect Limits
        {
            get { return _limits; }
            set
            {
                _limits = value;
                OnPropertyChanged();
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Construction

        public RectangleVM(Rect rectangle)
            : this(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height)
        {
        }

        public RectangleVM(double x, double y, double width, double height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            StrokeThickness = 1;
            Visibility = Visibility.Visible;
            Stroke = Brushes.Black;
            Fill = null;
            LimitToParent = true;
            Limits = Rect.Empty;
        }

        #endregion

        #region Commands

        public ICommand SelectCommand => _selectCommand ?? (_selectCommand = new FlatCommand(c => ExecuteSelectCommand()));

        private void ExecuteSelectCommand()
        {
            IsSelected = true;
        }

        #endregion

    }
}
