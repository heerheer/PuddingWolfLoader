using PuddingWolfLoader.Framework.Container;
using PuddingWolfLoader.Framework.Events;
using PuddingWolfLoader.Framework.ViewModel;
using PuddingWolfLoader.SDK;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using XQSimulator.Model;

namespace PuddingWolfLoader.Framework.Util
{
    public class PluginLoader
    {

        static LogContainer logContainer;
        static PluginLoader()
        {
            logContainer = LogContainer.CreateInstance<PluginLoader>();
            LogCore.RegLogContainer(logContainer);
        }

        public delegate int Initialize(int authCode);//dll的初始化函数
        public delegate IntPtr AppInfo();//dll的获取信息函数
        public delegate IntPtr _eventPrivateMsg(int subType, int msgId, long userId, string msg, int font);//dll的群消息函数
        public delegate IntPtr _eventGroupMsg(int subType, int msgId, long group, long userId, string fromAnonymous,
            string msg, int font);//dll的私聊消息函数_eventGroupMsg

        public static void LoadPlugins()
        {
            Process process = new Process();

            var dir = Directory.GetCurrentDirectory() + "\\plugins";
            var tempdirinfo = new DirectoryInfo(Directory.GetCurrentDirectory() + "\\plugins\\temp");

            if (tempdirinfo.Exists)
            {
                tempdirinfo.Delete(true);
            }

            tempdirinfo.Create();

            //copy
            var plugindir = new DirectoryInfo(dir);//Real Plugin Dir
            plugindir.GetFiles("*.dll").ToList().ForEach(f =>
            {
                f.CopyTo(Path.Combine(tempdirinfo.FullName, f.Name));
            });
            //copy finish

            foreach (var file in tempdirinfo.GetFiles("*.dll"))
            {
                logContainer.Info("正在加载" + file.Name);
                try
                {
                    LoadPlugin(file.FullName);
                }
                catch (Exception ex)
                {
                    logContainer.Error(ex.Message + "," + new Plugin() { PluginName = file.Name });

                }
            }
        }

        public static void ReLoadPlugins()
        {
            ModulesViewModel.Instance.Plugins.ToList().ForEach(p => Kernel32.FreeLibrary(p.ProcessPtr.ToInt32()));
            GC.Collect();
            ModulesViewModel.Instance.Plugins.Clear();//Clear Plugin List
            LoadPlugins();
        }

        public static void LoadPlugin(string path)
        {
            var dllptr = Kernel32.LoadLibraryA(path);
            if (dllptr.ToInt32() == 0)
            {
                throw new Exception("DLL句柄为0");
            }

            var address_Initialize = Kernel32.GetProcAddress(dllptr, "Initialize");
            var address_AppInfo = Kernel32.GetProcAddress(dllptr, "AppInfo");



            var InitializeMethod = Marshal.GetDelegateForFunctionPointer<Initialize>(address_Initialize);
            var AppInfoMethod = Marshal.GetDelegateForFunctionPointer<AppInfo>(address_AppInfo);

            //一个json
            /*
             {
	        "id": 0,
	        "loggerAddr": 0,
	        "getInstalledApps": 0,
	        "downLoad": 0,
	        "sdkFuncs": 0
             }
             
             */

            string init_json;

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream))
                {
                    writer.WriteStartObject();
                    writer.WriteString("id", "i.dont.down");
                    writer.WriteNumber("loggerAddr", 0);
                    writer.WriteNumber("downLoad", 0);
                    writer.WriteNumber("getInstalledApps", 0);
                    writer.WriteNumber("sdkFuncs", GetSdkFuncs.GetSdkFuncsPtr.ToInt64());
                    writer.WriteEndObject();
                }
                init_json = Encoding.UTF8.GetString(stream.ToArray());

            }



            InitializeMethod.Invoke(Marshal.StringToHGlobalAnsi(init_json).ToInt32());

            var json = Marshal.PtrToStringAnsi(AppInfoMethod.Invoke());

            var jc = JsonDocument.Parse(json);

            var plug = new Plugin()
            {
                PluginName = jc.RootElement.GetProperty("id").GetString(),
                PluginVersion = jc.RootElement.GetProperty("version").GetString(),
                PluginAuthor = jc.RootElement.GetProperty("author").GetString(),
                PluginDesc = jc.RootElement.GetProperty("description").GetString(),
                ProcessPtr = dllptr
            };

            ModulesViewModel.Instance.Plugins.Add(plug);

            logContainer.Info("成功加载");


        }

        public static void GroupMsgEvent(long group,long user,string msg)
        {

            ModulesViewModel.Instance.Plugins.ToList().ForEach(x =>
            {
                var address_GroupMsg = Kernel32.GetProcAddress(x.ProcessPtr, "_eventGroupMsg");
                var _eventGroupMsgMethod = Marshal.GetDelegateForFunctionPointer<_eventGroupMsg>(address_GroupMsg);
                _eventGroupMsgMethod.Invoke(0,0,group,user,"",msg,0);
            });
            
        }
    }

}
