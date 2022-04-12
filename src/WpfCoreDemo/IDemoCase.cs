using System.Windows;

namespace WpfCoreDemo
{
    internal interface IDemoCase
    {

        string Name { get; }

        ResourceDictionary Templates { get; }

    }
}
