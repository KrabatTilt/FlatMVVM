using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TT.FlatMVVM;
using TT.FlatMVVM.Utils;
using WpfCoreDemo.Part1;
using WpfCoreDemo.Part2;
using WpfCoreDemo.Part3;
using WpfCoreDemo.Part4;

namespace WpfCoreDemo
{
    internal class MainVM : FlatVM
    {

        private readonly Dictionary<string, Window> _exampleWindows = new Dictionary<string, Window>();

        private ICommand _openExamppleCommand;

        public ICommand OpenExample => _openExamppleCommand ??= new DelegateCommand<string>(ExecuteOpenExample);

        private void ExecuteOpenExample(string id)
        {
            if (_exampleWindows.TryGetValue(id, out Window exampleWindow))
            {
                exampleWindow.WindowState = WindowState.Normal;
                exampleWindow.Visibility = Visibility.Visible;
            }
            else
            {
                exampleWindow = id switch
                {
                    "1" => new Part1Window { Owner = UI.MainWindow, ShowInTaskbar = false, DataContext = new Part1VM() },
                    "2" => new Part2Window { Owner = UI.MainWindow, ShowInTaskbar = false, DataContext = new Part2VM() },
                    "3" => new Part3Window { Owner = UI.MainWindow, ShowInTaskbar = false, DataContext = new Part3VM() },
                    "4" => new Part4Window(){Owner = UI.MainWindow, ShowInTaskbar = false, DataContext = new Part4VM() },
                    _ => throw new ArgumentOutOfRangeException(nameof(id), $"Example id {id} dies not exist.")
                };

                exampleWindow.Closing += (sender, args) =>
                {
                    ((Window)sender).Visibility = Visibility.Collapsed;
                    args.Cancel = true;
                };

                _exampleWindows.Add(id, exampleWindow);
                exampleWindow.Show();
            }
        }
    }
}
