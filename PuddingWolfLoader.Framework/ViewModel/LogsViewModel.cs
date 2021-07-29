using PropertyChanged;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuddingWolfLoader.Framework.ViewModel
{
    [AddINotifyPropertyChangedInterface]
    public class LogsViewModel
    {
        private StringBuilder _logsBuilder;
        public string LogsText
        {
            get
            {

                return _logsBuilder.ToString();
            }
        }

        public LogsViewModel()
        {
            _logsBuilder = new();
        }

        public void AddLog(string text)
        {
            _logsBuilder.AppendLine(text);
        }
    }
}
