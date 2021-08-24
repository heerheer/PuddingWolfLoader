using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PuddingWolfLoader.Framework.Events
{
    public class GetSdkFuncs
    {
        public delegate string GetSdkFunsDelegate();
        public static GetSdkFunsDelegate GetSdkFuns = new(_GetSdkFuns);

        public static IntPtr GetSdkFuncsPtr = Marshal.GetFunctionPointerForDelegate(GetSdkFuns);

        public static string _GetSdkFuns()
        {

            using (var stream = new MemoryStream())
            {
                using (var writer = new Utf8JsonWriter(stream))
                {
                    writer.WriteStartObject();
                    writer.WriteNumber("发送群消息", Marshal.GetFunctionPointerForDelegate(Events.MsgEventApis.SendGroupMsgApi).ToInt64());
                    writer.WriteNumber("发送私聊消息", Marshal.GetFunctionPointerForDelegate(Events.MsgEventApis.SenPriMsgApi).ToInt64());
                    writer.WriteNumber("取登录昵称", 0);
                    writer.WriteNumber("取登录帐号", 0);
                    writer.WriteNumber("撤回消息", 0);
                    writer.WriteNumber("发送赞", 0);
                    writer.WriteNumber("接收图片", 0);
                    writer.WriteNumber("接收语音", 0);
                    writer.WriteNumber("取Cookies", 0);
                    writer.WriteNumber("取CsrfToken", 0);
                    writer.WriteNumber("取好友列表", 0);
                    writer.WriteNumber("取陌生人信息", 0);
                    writer.WriteNumber("取群成员列表", 0);
                    writer.WriteNumber("取群列表", 0);
                    writer.WriteNumber("是否支持发送图片", 0);
                    writer.WriteNumber("是否支持发送语音", 0);
                    writer.WriteNumber("置好友添加请求", 0);
                    writer.WriteNumber("置匿名群员禁言", 0);
                    writer.WriteNumber("置全群禁言", 0);
                    writer.WriteNumber("置群成员名片", 0);
                    writer.WriteNumber("置群成员专属头衔", 0);
                    writer.WriteNumber("置群管理员", 0);
                    writer.WriteNumber("置群匿名设置", 0);
                    writer.WriteNumber("置群添加请求", 0);
                    writer.WriteNumber("置群退出", 0);
                    writer.WriteNumber("置群员禁言", 0);
                    writer.WriteNumber("置群员移除", 0);
                    writer.WriteNumber("取头像_地址", 0);
                    writer.WriteNumber("取头像_字节集", 0);
                    writer.WriteEndObject();
                }
                return Encoding.UTF8.GetString(stream.ToArray());

            }
        }
    }
}
