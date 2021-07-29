using PropertyChanged;
using PuddingWolfLoader.Framework.Container;
using PuddingWolfLoader.Framework.View;
using PuddingWolfLoader.SDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuddingWolfLoader.Framework.ViewModel
{

    public record ViewTab(string text, string icon,Type type);


    [AddINotifyPropertyChangedInterface]
    public class MainWindowViewModel
    {
        public ObservableCollection<ViewTab> ViewTabs { get; set; }

        private LogContainer LogContainer = LogContainer.CreateInstance<MainWindowViewModel>();

        public MainWindowViewModel()
        {
            ViewTabs = new ObservableCollection<ViewTab>();

            Init();
        }

        void Init()
        {
            //载入模块
            //TODO Load Modules

            //注册Log组件
            LogCore.RegLogContainer(LogContainer);//注册MainVM的日志组件
            LogContainer.Push("注册日志组件完成");

            //载入视图
            //Find ModuleViews And Add to Tabs
            RegViewAndAddToTav<DashboardView>("Dashboard", "\xe8cd");
            RegViewAndAddToTav<LogsView>("Logs", "\xe8c5");
            RegViewAndAddToTav<InstantMessagingView>("iMessgage", "\xe8c3");
            RegViewAndAddToTav<ModulesView>("Modules", "\xe89b");
            LogContainer.Push("注册视图与Tabs完成");


        }



        void RegViewAndAddToTav<T>(string text, string icon)
        {
            RegViewAndAddToTav(typeof(T), text, icon);
        }

        void RegViewAndAddToTav(Type type,string text, string icon)
        {
            ViewContainer.RegisterViewInstance(type);
            ViewTabs.Add(new ViewTab(text, icon,type));
        }
    }
}
