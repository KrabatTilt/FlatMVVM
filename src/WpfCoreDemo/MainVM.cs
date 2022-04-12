using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TT.FlatMVVM;
using WpfCoreDemo.Utils;

namespace WpfCoreDemo
{
    internal class MainVM : FlatVM
    {

        private IDemoCase _activeView;
        
        public List<IDemoCase> DemoCases { get; }

        public DataTemplateSelector DemoCaseContentTemplateSelector { get; }

        public IDemoCase ActiveView
        {
            get => _activeView;
            set => SetProperty(ref _activeView, value);
        }

        public MainVM()
        {
            
        }

        public MainVM(IEnumerable<IDemoCase> demoCases)
        {
            DemoCases = demoCases.ToList();

            var dynamicDataTemplateSelector = new DynamicDataTemplateSelector();

            foreach (IDemoCase demoCase in DemoCases)
            {
                DataTemplate mainContentTemplate = Application.Current.FindDataTemplates("DemoCaseTemplate", demoCase.GetType()).FirstOrDefault();
                dynamicDataTemplateSelector.AddDataTemplateMapping(demoCase, mainContentTemplate);
            }

            DemoCaseContentTemplateSelector = dynamicDataTemplateSelector;
            ActiveView = DemoCases.FirstOrDefault();
        }

    }
}
