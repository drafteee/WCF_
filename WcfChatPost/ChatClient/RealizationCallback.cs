using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfChatPost;
using System.ServiceModel;
using System.Windows.Forms;

namespace ChatClient
{
    public delegate bool DelegateOfConfirmUser(ChatUser ourName);
    public delegate void DelegateOfUpdateUsers(List<ChatUser> listUsers);
    public delegate void DelegateOfUpdateRooms(List<ChatRoom> listRooms);
    public delegate void DelegateOfUpdateMessages(List<ChatMessage> listMessages);
    public delegate ChatRoom DelegateOfUpdateRoom();

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Single)]
    
    public class RealizationCallback : IChatServiceCallback
    {
        public event DelegateOfConfirmUser delConfirmUser;
        public event DelegateOfUpdateUsers delUpdateUsers;
        public event DelegateOfUpdateRooms delUpdateRooms;
        public event DelegateOfUpdateMessages delUpdateMsg;
        public event DelegateOfUpdateRoom delUpdateRoom;

        public ChatServiceClient client;

        public ChatUser ClientConnect(string login)
        {
            client = new ChatServiceClient(new InstanceContext(this));
            return client.ClientConnect(login);
        }

        public List<ChatUser> GetAllUsers()
        {
            return (client.GetAllUserss()).ToList<ChatUser>();
        }
        public void SendNewMessage(ChatMessage msg,ChatRoom room)
        {
            client.SendNewMessage(msg,room);
        }

        public void RemoveUser(ChatUser user)
        {
            client.RemoveUser(user);
        }

        public bool CreateNewRoom(ChatUser ourUser, string[] users, ChatRoom nameRoom)
        {
            return client.CreateNewRoom(ourUser, users, nameRoom);
        }
        [OperationBehavior]
        public bool ConfirmUser(ChatUser ourName)
        {
            return delConfirmUser(ourName);
        }

        public void CreateRoom(ChatUser ourName, ChatUser requiredName, ChatRoom nameRoom)
        {
            throw new NotImplementedException();
        }
        public void UpdateUsers(ChatUser[] users)
        {
            delUpdateUsers(users.ToList<ChatUser>());
        }

        public void UpdateMsg(ChatMessage[] msgs)
        {
            delUpdateMsg(msgs.ToList<ChatMessage>());
        }
        public void UpdateRooms(ChatRoom[] rooms)
        {
            delUpdateRooms(rooms.ToList<ChatRoom>());
        }

        public ChatRoom UpdateRoom()
        {
            return delUpdateRoom();
        }

        public void ExitRoom(ChatUser user,ChatRoom room)
        {
            client.ExitRoom(user,room);
        }

        public void AddInRoom(ChatUser user,ChatUser ourname,ChatRoom room)
        {
            client.AddInRoom(user,ourname,room);
        }
    }
}
