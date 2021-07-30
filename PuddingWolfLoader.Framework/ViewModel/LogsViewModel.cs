using Microsoft.VisualBasic.Logging;
using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static PuddingWolfLoader.SDK.Model.LogModel;

namespace PuddingWolfLoader.Framework.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class LogsViewModel:INotifyPropertyChanged
    {
        [AlsoNotifyFor("SelectedLogs")]
        public ObservableCollection<PudLog> Logs { get; set; }

        public ObservableCollection<PudLog> SelectedLogs=> new(Logs.Where(x => x.Type == (PudLogType)Enum.Parse(typeof(PudLogType), SelectedType)));
        public string SelectedType { get; set; }

        public LogsViewModel()
        {
            Logs = new();
            //让SelectedLogs能够被Logs引起通知UI
            Logs.CollectionChanged += delegate (object? sender, NotifyCollectionChangedEventArgs args)
            {
                PropertyChanged.Invoke(this,new(nameof(SelectedLogs)));
            };
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void AddLog(PudLog log)
        {
            Logs.Add(log);
        }
    }
}
