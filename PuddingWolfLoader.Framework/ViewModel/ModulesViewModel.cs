using PuddingWolfLoader.Framework.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XQSimulator.Model;

namespace PuddingWolfLoader.Framework.ViewModel
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class ModulesViewModel
    {
        public ModulesViewModel()
        {
            Instance = this;
        }
        public static ModulesViewModel Instance;

        public ObservableCollection<Plugin> Plugins { get; set; } = new();

        public void LoadPlugins()
        {
            PluginLoader.LoadPlugins();
        }
    }
}
