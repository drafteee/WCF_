using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WcfChatPost
{
    public class ChatEngine
    {
        private List<ChatUser> connectedUsers = new List<ChatUser>();
        private Dictionary<string, List<ChatMessage>> incomingMessages = new Dictionary<string, List<ChatMessage>>();
        private Dictionary<ChatRoom, List<ChatUser>> usersOfRoom = new Dictionary<ChatRoom, List<ChatUser>>();

        IChatCallback callback = null;
        
        public List<ChatUser> ConnectedUsers
        {
            get
            {
                return connectedUsers;
            }

            set
            {
                connectedUsers = value;
            }
        }

        public ChatUser AddNewChatUser(ChatUser newuser)
        {
            if (!usersOfRoom.ContainsKey(new ChatRoom() { NameRoom = "Общая комната" }))
            {
                usersOfRoom.Add(new ChatRoom() { NameRoom = "Общая комната" }, new List<ChatUser>()
                {
                    new ChatUser()
                    {
                        UserName = newuser.UserName
                    }
                });
            }
            else
            {
                usersOfRoom[new ChatRoom() { NameRoom = "Общая комната" }].Add(newuser);
            }
            var exist = from ChatUser e in this.ConnectedUsers
                        where e.UserName == newuser.UserName
                        select e;

            if (exist.Count() == 0)
            {
                callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();

                this.ConnectedUsers.Add(newuser);
                incomingMessages.Add(newuser.UserName, new List<ChatMessage>()
                {
                    new ChatMessage() {User = newuser,Date=DateTime.Now,Message="welcome" }
                });

                AddNewMessage(new ChatMessage() { User = newuser, Date = DateTime.Now, Message = "welcome" });
                Console.WriteLine("\nnew user connected");
                return newuser;
            }
            else
                return null;
        } 

        public void AddNewMessage(ChatMessage newMessage)
        {
            Console.WriteLine(newMessage.User.UserName + " says:" + newMessage.Message + " at" + newMessage.Date);

            foreach(var user in this.ConnectedUsers)
            {
                if (!newMessage.User.UserName.Equals(user.UserName))
                {
                    incomingMessages[user.UserName].Add(newMessage);
                }
            }
        }

        public List<ChatMessage> GetNewMessage(ChatUser user)
        {
            List<ChatMessage> myNewMessages = incomingMessages[user.UserName];
            incomingMessages[user.UserName] = new List<ChatMessage>();
            
            return myNewMessages;
        }

        public void RemoveUser(ChatUser user)
        {
            this.ConnectedUsers.RemoveAll(x => x.UserName == user.UserName);
        }

        public bool CreateNewRoom(ChatUser ourName, string[] users, ChatRoom nameRoom)
        {
            callback.ConfirmUser(ourName);
            return false;
        }
        public List<ChatRoom> GetRooms(ChatUser user)
        {
            List<ChatRoom> listRooms = new List<ChatRoom>();
            foreach (var room in usersOfRoom)
            {
                if (room.Value.Contains(user))
                {
                    listRooms.Add(room.Key);
                }
            }
            return listRooms;
        }
    }
}
