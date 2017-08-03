using System;
using System.Windows;
using System.Windows.Controls;
using FlatMVVM_WPFDemo.ViewModels;

namespace FlatMVVM_WPFDemo.Utils
{
    public class ListViewItemDataTemplateSelector : DataTemplateSelector
    {

        public DataTemplate ColoredItemTemplate { get; set; }
        public DataTemplate TextItemTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item is ColoredItemVM)
                return ColoredItemTemplate;

            if (item is TextItemVM)
                return TextItemTemplate;

            throw new InvalidOperationException();
        }
    }
}
