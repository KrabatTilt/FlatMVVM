using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace FlatMVVM.Utils
{
    /// <summary>
    /// Wraps UI functionality.
    /// </summary>
    public static class UIWrapper
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public static DispatcherOperation DispatchAsync(Action action)
        {
           return Application.Current.Dispatcher.BeginInvoke(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="priority"></param>
        /// <returns></returns>
        public static DispatcherOperation DispatchAsync(Action action, DispatcherPriority priority)
        {
            return Application.Current.Dispatcher.BeginInvoke(action, priority);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        public static void DispatchSync(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="priority"></param>
        public static void DispatchSync(Action action, DispatcherPriority priority)
        {
            Application.Current.Dispatcher.Invoke(action, priority);
        }

        /// <summary>
        /// Reevaluates all commands asynchronously.
        /// </summary>
        public static void RefreshCommands()
        {
            DispatchAsync(CommandManager.InvalidateRequerySuggested);
        }

        /// <summary>
        /// Activate current applications MainWindow.
        /// </summary>
        public static void EnableMainWindow()
        {
            Application.Current.MainWindow.IsEnabled = true;
        }

        /// <summary>
        /// Deactivate current applications MainWindow.
        /// </summary>
        public static void DisableMainWindow()
        {
            Application.Current.MainWindow.IsEnabled = false;
        }

        /// <summary>
        /// Check whether code is executed in design mode or not.
        /// </summary>
        /// <returns>True if in design mode; else false.</returns>
        public static bool IsInDesignMode => DesignerProperties.GetIsInDesignMode(new DependencyObject());
    }
}
