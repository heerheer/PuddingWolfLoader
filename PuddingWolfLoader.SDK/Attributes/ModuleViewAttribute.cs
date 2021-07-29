using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuddingWolfLoader.SDK.Attributes
{
    public class ModuleViewAttribute : Attribute
    {
        public ModuleViewAttribute(string name, string icon)
        {
            Name = name;
            Icon = icon;
        }

        public string Name { get; set; }
        public string Icon { get; set; }

    }
}
