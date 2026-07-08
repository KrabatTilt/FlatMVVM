using System.Windows;
using System.Windows.Controls;
using TT.FlatMVVM.Core;
using WpfCoreDemo.Utils;

namespace WpfCoreDemo
{
    internal sealed class MainVM : FlatVM
    {
        public List<IDemoCase> DemoCases { get; }

        public DataTemplateSelector DemoCaseContentTemplateSelector { get; }

        public IDemoCase ActiveView
        {
            get;
            set => SetProperty(ref field, value);
        }

        public MainVM(IEnumerable<IDemoCase> demoCases)
        {
            DemoCases = demoCases.ToList();

            var dynamicDataTemplateSelector = new DynamicDataTemplateSelector();

            foreach (IDemoCase demoCase in DemoCases)
            {
                DataTemplate? mainContentTemplate = Application.Current.FindDataTemplates("DemoCaseTemplate", demoCase.GetType()).FirstOrDefault();

                if(mainContentTemplate == null)
                    continue;

                dynamicDataTemplateSelector.AddDataTemplateMapping(demoCase, mainContentTemplate);
            }

            DemoCaseContentTemplateSelector = dynamicDataTemplateSelector;
            ActiveView = DemoCases.First();
        }

    }
}
