using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfChatPost
{
    [ServiceContract]
    
    public interface IChatCallback
    {
        [OperationContract(IsOneWay = false)]
        bool ConfirmUser(ChatUser ourName);
        [OperationContract(IsOneWay =true)]
        void CreateRoom(ChatUser ourName, ChatUser requiredName, ChatRoom nameRoom);
        [OperationContract(IsOneWay = true)]
        void UpdateUsers(ChatUser[] users);
        [OperationContract(IsOneWay = true)]
        void UpdateRooms(ChatRoom[] rooms);
        [OperationContract(IsOneWay = true)]
        void UpdateMsg(ChatMessage[] msgs);
        [OperationContract(IsOneWay = false)]
        ChatRoom UpdateRoom();
    }


    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = false)]
        ChatUser ClientConnect(string userName);

        [OperationContract(IsOneWay = false)]
        ChatUser[] GetAllUserss();

        [OperationContract(IsOneWay = true)]
        void SendNewMessage(ChatMessage newMessage,ChatRoom room);

        [OperationContract(IsOneWay = true)]
        void RemoveUser(ChatUser user);

        [OperationContract]
        bool CreateNewRoom(ChatUser ourName, string[] users, ChatRoom nameRoom);

        [OperationContract(IsOneWay = true)]
        void ExitRoom(ChatUser user,ChatRoom room);
        [OperationContract(IsOneWay = true)]
        void AddInRoom(ChatUser user,ChatUser ourname, ChatRoom room);
    }

    [DataContract]
    public class ChatRoom
    {
        private string nameRoom;
        [DataMember]
        public string NameRoom
        {
            get
            {
                return nameRoom;
            }

            set
            {
                nameRoom = value;
            }
        }
    }
    [DataContract]
    public class ChatUser: IEquatable<ChatUser>
    {

        [DataMember]
        public string UserName { get; set; }


        public bool Equals(ChatUser other) => UserName == other.UserName;

        public override string ToString()
        {
            return this.UserName;
        }
    }

    [DataContract]
    public class ChatMessage
    {
        private ChatUser user;
        private string message;
        private DateTime date;

        [DataMember]
        public DateTime Date
        {
            get
            {
                return date;
            }

            set
            {
                date = value;
            }
        }

        [DataMember]
        public string Message
        {
            get
            {
                return message;
            }

            set
            {
                message = value;
            }
        }

        [DataMember]
        public ChatUser User
        {
            get
            {
                return user;
            }

            set
            {
                user = value;
            }
        }
    }
}
