﻿namespace WpfDemo.Part1
{
    /// <summary>
    /// Interaction logic for Part1Window.xaml
    /// </summary>
    public partial class Part1Window
    {
        public Part1Window()
        {
            InitializeComponent();
            DataContext = new SimpleVM();
        }
    }
}
