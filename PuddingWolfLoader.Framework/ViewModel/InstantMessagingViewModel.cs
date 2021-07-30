using Panuon.UI.Core;
using PuddingWolfLoader.Framework.Container;
using PuddingWolfLoader.Framework.Model;
using PuddingWolfLoader.SDK;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace PuddingWolfLoader.Framework.ViewModel
{
    [PropertyChanged.AddINotifyPropertyChangedInterface]
    public class InstantMessagingViewModel
    {
        private LogContainer logContainer;

        public ObservableCollection<IMObject> iMObjects { get; set; }
        public Dictionary<string, ObservableCollection<IMMsg>> MessageDic { get; set; } = new();

        public ObservableCollection<IMMsg> Messages => SelectedSource==null?new():MessageDic[SelectedSource?.Id];
        public string EditingMessage { get; set; }
        public IMObject SelectedSource { get; set; }



        #region Events
        public delegate void AddedGroupEventHandler(Group group);
        public static event AddedGroupEventHandler OnAddedGroupEvent;

        public delegate void AddedFriendEventHandler(Friend friend);
        public static event AddedFriendEventHandler OnAddedFriendEvent;

        public delegate void MsgReceivedEventHandler(IMMsg msg);
        public static event MsgReceivedEventHandler OnMsgReceived;
        #endregion


        public ICommand AddGroupCommand { get; set; }
        public ICommand AddFriendCommand { get; set; }
        public ICommand SendMsgCommand { get; set; }

        public InstantMessagingViewModel()
        {

            //初始化日志容器
            logContainer = LogContainer.CreateInstance<InstantMessagingViewModel>();
            LogCore.RegLogContainer(logContainer);

            iMObjects = new();

            //初始化Command
            AddGroupCommand = new RelayCommand(AddGroup);
            AddFriendCommand = new RelayCommand(AddFriend);
            SendMsgCommand = new RelayCommand(SendMsg);

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
                logContainer.Info($"IM收到消息来自{msg.From}发送者{msg.Sender}");
            };
        }

        void AddGroup(object sender)
        {
            var randomName = new[] { "XXX测试群", "XXX交流群", "XXX讨论群", "XXX真爱粉", "XXX萌新群", "XXX的小窝" };
            AddGroup(new Group
            {
                Id = new Random().Next(10000000, 999999999).ToString(),
                Name = randomName[new Random().Next(randomName.Length)] + new Random().Next(0, 999).ToString()
            });
        }
        void AddFriend(object sender)
        {
            var randomName = new[] { "一个好友", "XXX老师", "鱼", "呜呜呜", "好哥哥好姐姐", "欧洲狗" };
            AddFriend(new Friend
            {
                Id = new Random().Next(10000000, 999999999).ToString(),
                Name = randomName[new Random().Next(randomName.Length)] + new Random().Next(0, 999).ToString()
            });
        }
        void SendMsg(object sender)
        {
            CreatedMsg(SelectedSource.Id, "10000", EditingMessage);
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
            var _msg = new IMMsg() { From = from, Sender = sender, Content = msg };
            MessageDic[from].Add(_msg);
            OnMsgReceived.Invoke(_msg);
        }
    }
}
