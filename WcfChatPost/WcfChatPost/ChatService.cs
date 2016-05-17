using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChatPost
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single,IncludeExceptionDetailInFaults =true)]
    public class ChatService : IChatService
    {
        private ChatEngine mainEngine = new ChatEngine();
        public ChatUser ClientConnect(string userName)
        {
            return mainEngine.AddNewChatUser(new ChatUser() { UserName = userName });
        }

        public ChatMessage[] GetNewMessagess(ChatUser user)
        {
            return mainEngine.GetNewMessage(user).ToArray();
        }

        public ChatUser[] GetAllUserss()
        {
            ChatUser[] array = new ChatUser[mainEngine.ConnectedUsers.Count];
            return mainEngine.ConnectedUsers.ToArray();
        }

        public void RemoveUser(ChatUser user)
        {
            mainEngine.RemoveUser(user);
        }

        public void SendNewMessage(ChatMessage newMessage)
        {
            mainEngine.AddNewMessage(newMessage);
        }

        public bool CreateNewRoom(ChatUser ourName, string[] users, ChatRoom nameRoom)
        {
            return mainEngine.CreateNewRoom(ourName, users, nameRoom);
        }
        public ChatRoom[] GetRooms(ChatUser user)
        {
            return mainEngine.GetRooms(user).ToArray();
        }
    }
}
