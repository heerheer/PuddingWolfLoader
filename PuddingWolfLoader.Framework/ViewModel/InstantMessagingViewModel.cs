using Panuon.UI.Core;
using PuddingWolfLoader.Framework.Container;
using PuddingWolfLoader.Framework.Model;
using PuddingWolfLoader.Framework.Util;
using PuddingWolfLoader.SDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;

namespace PuddingWolfLoader.Framework.ViewModel
{
    /*
     约定

    群号 7位数
    用户 5位数
    自己 10000
        
     */

    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class InstantMessagingViewModel
    {
        public static InstantMessagingViewModel Instance;

        private LogContainer logContainer;

        public ObservableCollection<IMObject> iMObjects { get; set; }
        public Dictionary<string, ObservableCollection<IMMsg>> MessageDic { get; set; } = new();

        public ObservableCollection<IMMsg> Messages => SelectedSource == null ? new() : MessageDic[SelectedSource?.Id];
        public string EditingMessage { get; set; }
        public IMObject SelectedSource { get; set; }



        #region Events
        public delegate void AddedGroupEventHandler(Group group);
        public static event AddedGroupEventHandler OnAddedGroupEvent;

        public delegate void AddedFriendEventHandler(Friend friend);
        public static event AddedFriendEventHandler OnAddedFriendEvent;

        public delegate void MsgReceivedEventHandler(IMMsg msg);
        public static event MsgReceivedEventHandler OnMsgReceived;

        public delegate void GroupJoinedMemberEventHandler(string group, string member);
        public static event GroupJoinedMemberEventHandler OnGroupJoinedMemberEvent;
        #endregion


        public ICommand AddGroupCommand { get; set; }
        public ICommand AddFriendCommand { get; set; }
        public ICommand SendMsgCommand { get; set; }
        public ICommand GroupAddRandomMemberCommand { get; set; }

        public InstantMessagingViewModel()
        {
            Instance = this;
            //初始化日志容器
            logContainer = LogContainer.CreateInstance<InstantMessagingViewModel>();
            LogCore.RegLogContainer(logContainer);

            iMObjects = new();

            //初始化Command
            AddGroupCommand = new RelayCommand(AddGroup);
            AddFriendCommand = new RelayCommand(AddFriend);
            SendMsgCommand = new RelayCommand(SendMsg);
            GroupAddRandomMemberCommand = new RelayCommand(Group_AddRandomMemeber);

            //初始化Event
            OnAddedGroupEvent += delegate (Group group)
            {
                logContainer.Info($"IM添加了群{group.Name}:{group.Id}");
            };

            OnAddedFriendEvent += delegate (Friend friend)
             {
                 logContainer.Info($"IM添加了好友{friend.Name}:{friend.Id}");
             };
            OnMsgReceived += delegate (IMMsg msg)
            {
                logContainer.Info($"IM收到消息来自{msg.From}发送者{msg.Sender},消息内容{System.Text.RegularExpressions.Regex.Escape(msg.Content)}");
            };
            OnMsgReceived += delegate (IMMsg msg)
            {
                PluginLoader.GroupMsgEvent(long.Parse(msg.From), long.Parse(msg.Sender), msg.Content);
            };
            OnGroupJoinedMemberEvent += delegate (string group, string id)
            {
                logContainer.Info($"IM 成员{id}加入了群{group}");
            };
        }

        void AddGroup(object sender)
        {
            var randomName = new[] { "XXX测试群", "XXX交流群", "XXX讨论群", "XXX真爱粉", "XXX萌新群", "XXX的小窝" };
            AddGroup(new Group
            {
                Id = new Random().Next(10000000, 99999999).ToString(),
                Name = randomName[new Random().Next(randomName.Length)] + new Random().Next(0, 999).ToString()
            });
        }
        void AddFriend(object sender)
        {
            var randomName = new[] { "一个好友", "XXX老师", "鱼", "呜呜呜", "好哥哥好姐姐", "欧洲狗" };
            AddFriend(new Friend
            {
                Id = new Random().Next(10001, 99999).ToString(),
                Name = randomName[new Random().Next(randomName.Length)] + new Random().Next(0, 999).ToString()
            });
        }

        void SendMsg(object sender)
        {
            CreatedMsg(SelectedSource.Id, "1767407822", EditingMessage);
        }

        void Group_AddRandomMemeber(object sender)
        {
            AddGroupMember(SelectedSource.Id, new Random().Next(10001, 99999).ToString());
        }


        public void AddGroup(Group group)
        {
            iMObjects.Add(group);
            MessageDic.Add(group.Id, new());
            OnAddedGroupEvent.Invoke(group);
        }
        public void AddFriend(Friend friend)
        {
            iMObjects.Add(friend);
            MessageDic.Add(friend.Id, new());
            OnAddedFriendEvent.Invoke(friend);
        }

        /// <summary>
        /// 模拟一个消息
        /// </summary>
        public void CreatedMsg(string from, string sender, string msg)
        {
            var _msg = new IMMsg(MsgType.TextMessage) { From = from, Sender = sender, Content = msg };
            App.Current.Dispatcher.Invoke(() =>
            {
                MessageDic[from].Add(_msg);
            });
            OnMsgReceived.Invoke(_msg);
        }

        public void AddGroupMember(string group, string id)
        {
            var _msg = new IMMsg(MsgType.Event) { From = group, Sender = id, Content = $"{id}加入了群聊" };
            MessageDic[group].Add(_msg);
            (iMObjects.First(x => x.Id == group) as Group).Members.Add(id);
            OnGroupJoinedMemberEvent.Invoke(group, id);
        }
    }
}
