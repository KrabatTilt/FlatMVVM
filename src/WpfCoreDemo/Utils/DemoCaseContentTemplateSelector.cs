using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfCoreDemo.Utils
{
    internal sealed class DynamicDataTemplateSelector : DataTemplateSelector
    {

        private readonly Dictionary<object, DataTemplate> _contentMappings;

        public DynamicDataTemplateSelector()
        {
            _contentMappings = new Dictionary<object, DataTemplate>();
        }

        /// <summary>
        /// Adds a mapping of a content object to its data template.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddDataTemplateMapping(object key, DataTemplate value)
        {
            _contentMappings.Add(key, value);
        }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            if (item != null && _contentMappings.TryGetValue(item, out DataTemplate template))
                return template;

            return null;
        }

    }
}
