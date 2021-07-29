using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuddingWolfLoader.Framework.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class DashboardViewModel
    {
        public int ModuleCount {  get; set; }
        public string SystemStatus { get; set; } = "运行中";
        public string IMStatus { get; set; } = "IM运行中";
    }
}
