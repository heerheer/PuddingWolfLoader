using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using PuddingWolfLoader.Framework.Model;
using System.Diagnostics;

namespace PuddingWolfLoader.Framework.Converter
{
    class ChatBubbleSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var u = container as FrameworkElement;

            IMMsg message = item as IMMsg;

            if (message.Type is MsgType.TextMessage)
            {
                if (message.Sender == "10000")
                {
                    //Self
                    return u.FindResource("SendBubble") as DataTemplate;
                }
                else
                {
                    //Others
                    return u.FindResource("RecvBubble") as DataTemplate;
                }

            }else if(message.Type is MsgType.Event)
            {
                return u.FindResource("EventBubble") as DataTemplate;

            }
            else
            {
                return null;
            }

        }
    }
    class GroupFriendSelector : DataTemplateSelector
    {
        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var u = container as FrameworkElement;

            if (item.GetType() == typeof(Group))
            {
                return u.FindResource("iMObjectsListDataTemplate_Group") as DataTemplate;
            }
            else
            {

                return u.FindResource("iMObjectsListDataTemplate_Friend") as DataTemplate;
            }
        }
    }
}
