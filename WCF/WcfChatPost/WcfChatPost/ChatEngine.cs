using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Timers;
using System.Text;
using System.Threading.Tasks;

namespace WcfChatPost
{
    public class ChatEngine
    {
        public static System.Timers.Timer aTimer;
        private List<ChatUser> connectedUsers = new List<ChatUser>();
        private Dictionary<ChatRoom,Dictionary<ChatUser,List<ChatMessage>>> incomingMessages = new Dictionary<ChatRoom,Dictionary<ChatUser,List<ChatMessage>>>();
        private Dictionary<ChatRoom,List<ChatUser>> usersOfRoom = new Dictionary<ChatRoom,List<ChatUser>>();
        private Dictionary<ChatUser, IChatCallback> callbackUsers = new Dictionary<ChatUser, IChatCallback>();
        private Dictionary<ChatUser, bool> confirmArrayUsers = new Dictionary<ChatUser, bool>();
        
        IChatCallback callback = null;

        public ChatEngine()
        {
            aTimer = new System.Timers.Timer(1000);
            aTimer.Elapsed += new ElapsedEventHandler(UpdateTimer);
        }
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

        public List<ChatUser> GetConnectUsers()
        {
            return ConnectedUsers;
        }

        public void AddInRoom(ChatUser user,ChatUser ourname, ChatRoom room)
        {
            bool confirm = callbackUsers[ConnectedUsers.Where(x => x.UserName == user.UserName).First()].ConfirmUser(ourname);
            if (confirm)
            {
                usersOfRoom[usersOfRoom.Keys.Where(x=>x.NameRoom==room.NameRoom).First()].Add(user);
                incomingMessages[usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First()].Add(user, new List<ChatMessage>());
            }
        }
        public void ExitRoom(ChatUser user,ChatRoom room)
        {
            incomingMessages[usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First()].Remove(usersOfRoom[usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First()].Where(x => x.UserName == user.UserName).First());
            usersOfRoom[usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First()].Remove(ConnectedUsers.Where(x => x.UserName == user.UserName).First());
            if (incomingMessages[usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First()].Count == 0)
                incomingMessages.Remove(usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First());
            if (usersOfRoom[usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First()].Count == 0)
                usersOfRoom.Remove(usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First());
        }

        public void Update()
        {
            try {
                foreach (var user in callbackUsers)
                {
                    if (((ICommunicationObject)user.Value).State == CommunicationState.Opened)
                    {
                        user.Value.UpdateUsers(connectedUsers.ToArray());
                        user.Value.UpdateRooms(GetRooms(user.Key).ToArray());
                        user.Value.UpdateMsg(GetNewMessage(user.Value.UpdateRoom(),user.Key).ToArray());
                                
                    }
                }
            }catch(Exception ex) { }
        }
        public void UpdateTimer(object source, ElapsedEventArgs e)
        {
            Update();   
        }
        public ChatUser AddNewChatUser(ChatUser newuser)
        {
            aTimer.Enabled = false;

            if((usersOfRoom.Count==0))
            {
                usersOfRoom.Add(new ChatRoom() { NameRoom = "Общая комната"},new List<ChatUser>()
                {
                    newuser
                });
            }
            else
            {
                usersOfRoom.First().Value.Add(newuser);
            }
            var exist = from ChatUser e in this.ConnectedUsers
                        where e.UserName == newuser.UserName
                        select e;

            if (exist.Count() == 0)
            {
                callback = OperationContext.Current.GetCallbackChannel<IChatCallback>();

                this.ConnectedUsers.Add(newuser);
                if (!incomingMessages.ContainsKey(usersOfRoom.First().Key))
                    incomingMessages.Add(usersOfRoom.First().Key, new Dictionary<ChatUser, List<ChatMessage>>()
                    {
                        {newuser, new List<ChatMessage>()
                                {
                                    new ChatMessage() {User = newuser,Date=DateTime.Now,Message="welcome" }
                                }
                        }
                    });
                else
                    incomingMessages[usersOfRoom.First().Key].Add(newuser, new List<ChatMessage>() { new ChatMessage() { User = newuser, Date = DateTime.Now, Message = "welcome" } });

                callbackUsers.Add(newuser, callback);
                AddNewMessage(new ChatMessage() { User = newuser, Date = DateTime.Now, Message = "welcome" }, usersOfRoom.First().Key);
                Console.WriteLine("\nnew user connected");
                
                aTimer.Enabled = true;
                return newuser;
            }
            else
                return null;
        } 

        public void AddNewMessage(ChatMessage newMessage,ChatRoom room)
        {
            Console.WriteLine(newMessage.User.UserName + " says:" + newMessage.Message + " at" + newMessage.Date);

            foreach(var user in this.ConnectedUsers)
            {
                if ((!newMessage.User.UserName.Equals(user.UserName))&&(usersOfRoom[usersOfRoom.Keys.Where(x=>x.NameRoom==room.NameRoom).First()].Contains(user)))
                {
                    if (incomingMessages.ContainsKey(usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First()))
                    {
                        incomingMessages[(usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom)).Last()][usersOfRoom[usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First()].Where(x => x.UserName == user.UserName).First()].Add(newMessage);
                    }
                }
            }
        }

        public List<ChatMessage> GetNewMessage(ChatRoom room,ChatUser user)
        {
            List<ChatMessage> myNewMessages = incomingMessages[usersOfRoom.Keys.Where(x=>x.NameRoom==room.NameRoom).First()][usersOfRoom[usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First()].Where(x => x.UserName == user.UserName).First()];
            incomingMessages[usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First()][usersOfRoom[usersOfRoom.Keys.Where(x => x.NameRoom == room.NameRoom).First()].Where(x => x.UserName == user.UserName).First()] = new List<ChatMessage>();
            return myNewMessages;
        }

        public void RemoveUser(ChatUser user)
        {
            this.ConnectedUsers.RemoveAll(x => x.UserName == user.UserName);
        }

        public bool CreateNewRoom(ChatUser ourName, string[] users, ChatRoom nameRoom)
        {
            foreach(ChatUser user in ConnectedUsers)
                if (users.Contains(user.UserName))
                {
                    if(user!=ourName)
                        confirmArrayUsers.Add(user, callbackUsers[user].ConfirmUser(ourName));
                }

            foreach (var confirm in confirmArrayUsers)
                if (confirm.Value)
                {
                    if (!usersOfRoom.ContainsKey(nameRoom))
                    {
                        usersOfRoom.Add(nameRoom, new List<ChatUser>()
                        {
                            confirm.Key
                        });
                    }
                    else
                        usersOfRoom[nameRoom].Add(confirm.Key);
                    if (!incomingMessages.ContainsKey(nameRoom))
                        incomingMessages.Add(nameRoom, new Dictionary<ChatUser, List<ChatMessage>>() { { confirm.Key, new List<ChatMessage>() } });
                    else
                        incomingMessages[nameRoom].Add(confirm.Key, new List<ChatMessage>());
                }
            if(incomingMessages.ContainsKey(nameRoom))
                incomingMessages[nameRoom].Add(ourName, new List<ChatMessage>());
            if ((usersOfRoom.ContainsKey(nameRoom)) && (usersOfRoom[nameRoom].Count != 0))
            {
                usersOfRoom[nameRoom].Add(ourName);
            }
            confirmArrayUsers = new Dictionary<ChatUser, bool>();
            return usersOfRoom.ContainsKey(nameRoom) ? true : false;
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
