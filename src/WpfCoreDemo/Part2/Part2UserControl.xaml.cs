using System;
using System.Windows;
using System.Windows.Input;

namespace WpfCoreDemo.Part2
{
    /// <summary>
    /// Interaction logic for Part2UserControl.xaml
    /// </summary>
    public partial class Part2UserControl
    {

        public event EventHandler<CustomEvent1Args> CustomEvent1;


        public Part2UserControl()
        {
            InitializeComponent();
            ControlCanvas.PreviewMouseLeftButtonUp += ControlCanvas_PreviewMouseLeftButtonUp;
        }

        private void ControlCanvas_PreviewMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            CustomEvent1?.Invoke(sender, new CustomEvent1Args(new Point(13,37)));
        }

    }

    public class CustomEvent1Args : EventArgs
    {
        public Point Position { get; }

        public CustomEvent1Args(Point position)
        {
            Position = position;
        }

    }

}
