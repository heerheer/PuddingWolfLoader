using PuddingWolfLoader.Framework.View;
using PuddingWolfLoader.SDK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuddingWolfLoader.Framework.Container
{
    public class LogCore
    {
        public delegate void AddToLogsHandler(string text);

        public static Dictionary<Type,LogContainer> logContainers= new();

        public static void RegLogContainer(LogContainer container)
        {
            container.OnLogAdded += delegate (Type from,string text) {
                ((LogsView)ViewContainer.GetViewInstance<LogsView>()).ViewModel.AddLog(
                    $"[{from}]<{DateTime.Now.ToShortTimeString()}>{text}");
            };
           logContainers.Add(container.FromType,container);
        }

        public static LogContainer GetLogContainer<T>()
        {
            return logContainers[typeof(T)];
        }

    }
}
