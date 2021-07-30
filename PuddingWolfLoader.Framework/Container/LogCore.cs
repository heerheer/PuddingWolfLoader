using PuddingWolfLoader.Framework.View;
using PuddingWolfLoader.SDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PuddingWolfLoader.SDK.Model.LogModel;

namespace PuddingWolfLoader.Framework.Container
{
    public class LogCore
    {
        public delegate void AddLogEventHandler(PudLog log);

        /// <summary>
        /// 当日志被添加
        /// </summary>
        public static event AddLogEventHandler OnAddedLogEvent;

        public static Dictionary<Type, LogContainer> logContainers = new();

        static LogCore()
        {
            OnAddedLogEvent += AddedLog;
        }

        //注册Log容器
        public static void RegLogContainer(LogContainer container)
        {
            //当容器中日志事件触发时，触发LogCore.OnAddedLogEvent
            container.OnLogAdded += delegate (PudLog log)
            {
                OnAddedLogEvent.Invoke(log);
            };
            logContainers.Add(container.FromType, container);
        }

        public static LogContainer GetLogContainer<T>()
        {
            return logContainers[typeof(T)];
        }

        public static void AddedLog(PudLog log)
        {
            ((LogsView)ViewContainer.GetViewInstance<LogsView>()).ViewModel.AddLog(log);
        }

    }
}
