using PropertyChanged;
using PuddingWolfLoader.Framework.Container;
using PuddingWolfLoader.Framework.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuddingWolfLoader.Framework.ViewModel
{

    public record ViewTab(string text, string icon);


    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel
    {
        public ObservableCollection<ViewTab> ViewTabs { get; set; }

        public MainWindowViewModel()
        {
            ViewTabs = new ObservableCollection<ViewTab>();

            Init();
        }

        void Init()
        {
            //载入模块
            //TODO Load Modules

            //载入视图
            //Find ModuleViews And Add to Tabs
            RegViewAndAddToTav<DashboardView>("Dashboard", "\xe8cd");
            RegViewAndAddToTav<LogsView>("Logs", "\xe8c5");


            
        }



        void RegViewAndAddToTav<T>(string text, string icon)
        {
            RegViewAndAddToTav(typeof(T), text, icon);
        }
        void RegViewAndAddToTav(Type type,string text, string icon)
        {
            ViewContainer.RegisterViewInstance(type);
            ViewTabs.Add(new ViewTab(text, icon));
        }
    }
}
