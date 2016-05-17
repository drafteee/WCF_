using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChatPost
{
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.Single,
        ConcurrencyMode = ConcurrencyMode.Reentrant,
        UseSynchronizationContext = false,
        IncludeExceptionDetailInFaults =true)]
    public class ChatService : IChatService
    {
        private ChatEngine mainEngine = new ChatEngine();
        public ChatUser ClientConnect(string userName)
        {
            return mainEngine.AddNewChatUser(new ChatUser() { UserName = userName });
        }
        public ChatUser[] GetAllUserss()
        {

            return mainEngine.GetConnectUsers().ToArray();
        }

        public void RemoveUser(ChatUser user)
        {
            mainEngine.RemoveUser(user);
        }

        public void SendNewMessage(ChatMessage newMessage,ChatRoom room)
        {
            mainEngine.AddNewMessage(newMessage,room);
        }

        public bool CreateNewRoom(ChatUser ourName, string[] users, ChatRoom nameRoom)
        {
            return mainEngine.CreateNewRoom(ourName, users, nameRoom);
        }
        public ChatRoom[] GetRooms(ChatUser user)
        {
            return mainEngine.GetRooms(user).ToArray();
        }

        public void ExitRoom(ChatUser user,ChatRoom room)
        {
            mainEngine.ExitRoom(user,room);
        }

        public void AddInRoom(ChatUser user,ChatUser ourname, ChatRoom room)
        {
            mainEngine.AddInRoom(user,ourname,room);
        }
    }
}
