using PuddingWolfLoader.Framework.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuddingWolfLoader.Framework.Events
{
    public class MsgEventApis
    {
        public delegate int SendGroupMsgDelegate(long group, string msg, string appid);
        public delegate int SendPriMsgDelegate(long qq, string msg, string appid);

        public static SendGroupMsgDelegate SendGroupMsgApi = (long group, string msg, string appid) =>
        {
            InstantMessagingViewModel.Instance.CreatedMsg(group.ToString(),"10001",msg);
            return 0;
        };

        public static SendPriMsgDelegate SenPriMsgApi = (long qq, string msg, string appid) =>
        {
            InstantMessagingViewModel.Instance.CreatedMsg("qq", "10001", msg);
            return 0;
        };
    }
}
