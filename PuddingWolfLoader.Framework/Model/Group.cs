using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuddingWolfLoader.Framework.Model
{
    public class Group:IMObject
    { 
        public ObservableCollection<string> Members { get; set; } = new ObservableCollection<string>();
    }
}
