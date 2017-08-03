using System.Collections.ObjectModel;
using System.Linq;
using System.Timers;
using System.Windows.Input;
using FlatMVVM;

namespace FlatMVVM_WPFDemo.ViewModels
{
    public class MainVM : FlatVM
    {

        #region Fields

        private FlatVM _selectedItem;

        private ICommand _controlTimerCommand;
        private ICommand _nextItemCommand;
        private ICommand _previousItemCommand;

        private readonly Timer _timer;
        private int _selectedIndex;

        #endregion

        #region Properties

        public ObservableCollection<FlatVM> Items { get; } = new ObservableCollection<FlatVM>();

        public FlatVM SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                _selectedIndex = value;
                OnPropertyChanged();
            }
        }

        public ICommand ControlTimer => _controlTimerCommand ?? (_controlTimerCommand = new DelegateCommand<bool, object>(ExecuteControlTimer));

        public ICommand NextItem => _nextItemCommand ?? (_nextItemCommand = new DelegateCommand(e => ExecuteNextItem(), ce => CanExecuteNextItem()));

        public ICommand PreviousItem => _previousItemCommand ?? (_previousItemCommand = new DelegateCommand(e => ExecutePreviousItem(), ce => CanExecutePreviousItem()));

        #endregion

        #region Construction

        public MainVM()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i % 2 == 0)
                {
                    Items.Add(new TextItemVM { Text = $"TextItem{i}" });
                }
                else
                {
                    Items.Add(new ColoredItemVM { Brush = Utils.Utils.GetRandomBrush() });
                }
            }

            SelectedItem = Items.First();

            // configure timer
            _timer = new Timer(1000);
            _timer.Elapsed += Timer_Elapsed;
        }

        #endregion

        #region Command delegates

        private void ExecuteControlTimer(bool newValue)
        {
            if (newValue)
            {
                _timer.Start();
            }
            else
            {
                _timer.Stop();
            }
        }

        private bool CanExecuteNextItem()
        {
            return !_timer.Enabled && SelectedIndex < Items.Count - 1;
        }

        private void ExecuteNextItem()
        {
            SelectedIndex++;
        }

        private bool CanExecutePreviousItem()
        {
            return !_timer.Enabled && SelectedIndex > 0;
        }

        private void ExecutePreviousItem()
        {
            SelectedIndex--;
        }

        #endregion

        #region Event handler

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            SelectedIndex = (SelectedIndex + 1) % Items.Count;
        }

        #endregion

    }
}
