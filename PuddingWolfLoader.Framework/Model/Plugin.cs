using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;
using static PuddingWolfLoader.Framework.Util.PluginLoader;

namespace XQSimulator.Model
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class Plugin
    {
        public string PluginName { get; set; } = "";
        public string PluginVersion { get; set; } = "";
        public string PluginAuthor { get; set; } = "";
        public string PluginDesc { get; set; } = "";

        internal IntPtr ProcessPtr;

        public bool Enable { get; set; } = false;

    }

}
