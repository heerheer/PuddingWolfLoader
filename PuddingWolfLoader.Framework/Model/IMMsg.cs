using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PuddingWolfLoader.Framework.Model
{
    public class IMMsg
    {
        public IMMsg(MsgType type = default)
        {
            Type = type;
        }
        public string From { get; set; }
        public string Sender { get; set; }
        public MsgType Type { get; set; }
        public string Content { get; set; }
    }
    public enum MsgType
    {
        TextMessage,
        Image,
        Event,
        
    }
}
