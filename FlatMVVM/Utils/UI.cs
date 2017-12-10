using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace FlatMVVM.Utils
{
    /// <summary>
    /// Wraps some UI and application functionality.
    /// </summary>
    public static class UI
    {

        #region Dispatcher Operations

        /// <summary>
        /// Run action asynchronously on UI thread.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <returns></returns>
        public static DispatcherOperation RunAsync(Action action)
        {
            return Application.Current.Dispatcher.BeginInvoke(action);
        }

        /// <summary>
        /// Run action asynchronously on UI thread with specified priority.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <param name="priority">Execution priority.</param>
        /// <returns></returns>
        public static DispatcherOperation RunAsync(Action action, DispatcherPriority priority)
        {
            return Application.Current.Dispatcher.BeginInvoke(action, priority);
        }

        /// <summary>
        /// Run action synchronously on UI thread.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        public static void RunSync(Action action)
        {
            Application.Current.Dispatcher.Invoke(action);
        }

        /// <summary>
        /// Run action synchronously on UI thread with specified priority.
        /// </summary>
        /// <param name="action">Action to execute.</param>
        /// <param name="priority">Execution priority.</param>
        public static void RunSync(Action action, DispatcherPriority priority)
        {
            Application.Current.Dispatcher.Invoke(action, priority);
        }

        #endregion

        #region Commands

        /// <summary>
        /// Reevaluates all commands asynchronously.
        /// </summary>
        public static void RefreshCommands()
        {
            RunAsync(CommandManager.InvalidateRequerySuggested);
        }

        #endregion

        #region MainWindow

        /// <summary>
        /// Current Application MainWindow.
        /// </summary>
        public static Window MainWindow => Application.Current.MainWindow;

        /// <summary>
        /// Enable current application MainWindow.
        /// </summary>
        public static void EnableMainWindow()
        {
            MainWindow.IsEnabled = true;
        }

        /// <summary>
        /// Disable current application MainWindow.
        /// </summary>
        public static void DisableMainWindow()
        {
            MainWindow.IsEnabled = false;
        }

        #endregion

        #region DesignTime

        /// <summary>
        /// Check whether code is executed in design mode or not.
        /// </summary>
        /// <remarks>
        /// This method is evaluated only once on application startup.
        /// </remarks>
        /// <returns>True if in design mode; else false.</returns>
        public static bool IsInDesignMode { get; } = DesignerProperties.GetIsInDesignMode(new DependencyObject());

        #endregion

        #region Cursor

        /// <summary>
        /// Set desired mouse cursor.
        /// </summary>
        /// <param name="cursor">New mouse cursor.</param>
        public static void SetMouseCursor(Cursor cursor)
        {
            RunAsync(() => Mouse.OverrideCursor = cursor);
        }

        #endregion

    }
}
