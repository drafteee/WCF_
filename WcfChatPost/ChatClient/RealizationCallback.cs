using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfChatPost;
using System.ServiceModel;


namespace ChatClient
{
    public delegate bool DelegateOfConfirmUser(ChatUser ourName);
    public delegate void GotUserToAddDelegate(string userName);
    public delegate void GotUserToDeleteDelegate(string userName);
    public delegate void GotUpdateDelegate(List<string> usersNames);
    public delegate void GotRoomAddDelegate(string roomName);
    public delegate void GotRoomEnterDelegate(string roomName);
    public delegate void GotRoomDeleteDelegate(string roomName);
    class RealizationCallback : IChatCallback
    {
        public event DelegateOfConfirmUser delConfirmUser;
        public event GotUserToAddDelegate GotUserToAddEvent;
        public event GotUserToDeleteDelegate GotUserToDeleteEvent;
        public event GotUpdateDelegate GotUpdateEvent;
        public event GotRoomAddDelegate GotRoomAddEvent;
        public event GotRoomEnterDelegate GotRoomEnterEvent;
        public event GotRoomDeleteDelegate GotRoomDeleteEvent;

        public ChatServiceClient client;
        public bool ConfirmUser(ChatUser ourName)
        {
            if (delConfirmUser != null)
            {
                return delConfirmUser(ourName);
            }
            else
            {
                return false;
            }
        }

        public void CreateRoom(ChatUser ourName, ChatUser requiredName, ChatRoom nameRoom)
        {
            throw new NotImplementedException();
        }

        public ChatUser ClientConnect(string login,IChatService proxy)
        {
            client = new ChatServiceClient(new InstanceContext(this));
            return client.ClientConnect(login);
        }

        public List<ChatUser> GetAllUsers(IChatService proxy)
        {
            InstanceContext s = new InstanceContext(this);
            ChatServiceClient client = new ChatServiceClient(s);
            return client.GetAllUsers();
        }

        public List<ChatMessage> GetNewMessages(ChatUser user,IChatService proxy)
        {
            return GetNewMessages(user);
        }

        public void SendNewMessage(ChatMessage msg, IChatService proxy)
        {
            proxy.SendNewMessage(msg);
        }

        public void RemoveUser(ChatUser user,IChatService proxy)
        {
            proxy.RemoveUser(user);
        }

        public bool CreateNewRoom(ChatUser ourUser,ChatUser name, ChatRoom nameRoom,IChatService proxy)
        {
            return proxy.CreateNewRoom(ourUser, name, nameRoom);
        }
    }
}
