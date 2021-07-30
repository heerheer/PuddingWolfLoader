using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuddingWolfLoader.SDK.Model
{
    public class LogModel
    {

    public record PudLog(PudLogType Type,string From,DateTime Time,string Content);

    public enum PudLogType {
            Debug,
            Info,
            Warning,
            Error,
            Fatal
        }

    }
}
