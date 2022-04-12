using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;

namespace WpfCoreDemo.Utils
{
    internal static class ApplicationExtensions
    {

        public static void AddDictionaries(this Application application, IEnumerable<ResourceDictionary> dictionaries)
        {
            foreach (ResourceDictionary resourceDictionary in dictionaries)
                application.Resources.MergedDictionaries.Add(resourceDictionary);
        }

        public static void AddDictionary(this Application application, ResourceDictionary dictionary)
        {
            application.Resources.MergedDictionaries.Add(dictionary);
        }

        public static IEnumerable<DataTemplate> FindDataTemplates(this Application application, string keyPattern, Type dataType, Uri sourcePattern = null)
        {
            var hits = new List<DataTemplate>();
            foreach (ResourceDictionary applicationResource in application.Resources.MergedDictionaries)
            {
                if (sourcePattern != null && applicationResource.Source != sourcePattern)
                    continue;

                foreach (DictionaryEntry dictionaryEntry in applicationResource)
                {
                    if (dictionaryEntry.Key is not string keyValue)
                        continue;

                    if (keyValue.Contains(keyPattern) && dictionaryEntry.Value is DataTemplate hit)
                    {
                        var dataTypeValue = hit.DataType.ToString();
                        if (string.IsNullOrWhiteSpace(dataTypeValue))
                            continue;

                        if (dataTypeValue.Contains(dataType.Name))
                            hits.Add(hit);
                    }
                }
            }

            return hits;
        }

    }
}
