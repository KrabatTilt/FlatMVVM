using System.Windows;
using System.Windows.Controls;

namespace WpfCoreDemo.Utils
{
    internal sealed class DynamicDataTemplateSelector : DataTemplateSelector
    {

        private readonly Dictionary<object, DataTemplate> _contentMappings = new();

        /// <summary>
        /// Adds a mapping of a content object to its data template.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void AddDataTemplateMapping(object key, DataTemplate value)
        {
            _contentMappings.Add(key, value);
        }

        /// <summary>When overridden in a derived class, returns a <see cref="T:System.Windows.DataTemplate" /> based on custom logic.</summary>
        /// <param name="item">The data object for which to select the template.</param>
        /// <param name="container">The data-bound object.</param>
        /// <returns>Returns a <see cref="T:System.Windows.DataTemplate" /> or <see langword="null" />. The default value is <see langword="null" />.</returns>
        public override DataTemplate? SelectTemplate(object? item, DependencyObject container)
        {
            if (item != null && _contentMappings.TryGetValue(item, out DataTemplate? template))
                return template;

            return null;
        }

    }
}
