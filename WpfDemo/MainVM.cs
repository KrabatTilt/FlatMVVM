using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using TT.FlatMVVM.Wpf;
using TT.FlatMVVM.Wpf.Utils;
using WpfDemo.Part1;

namespace WpfDemo
{
    internal class MainVM : FlatVM
    {

        private readonly Dictionary<string, Window> _exampleWindows = new Dictionary<string, Window>();

        private ICommand _openExamppleCommand;

        public ICommand OpenExample => _openExamppleCommand ?? (_openExamppleCommand = new DelegateCommand<string, object>(ExecuteOpenExample));

        private void ExecuteOpenExample(string id)
        {
            if (_exampleWindows.TryGetValue(id, out Window exampleWindow))
            {
                exampleWindow.WindowState = WindowState.Normal;
                exampleWindow.Visibility = Visibility.Visible;
            }
            else
            {
                switch (id)
                {
                    case "1":
                        exampleWindow = new Part1Window { Owner = UI.MainWindow, ShowInTaskbar = false };
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(id), "");
                }

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
