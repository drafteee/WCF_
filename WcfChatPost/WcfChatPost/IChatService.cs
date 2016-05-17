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
    }


    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = false)]
        ChatUser ClientConnect(string userName);

        [OperationContract(IsOneWay = false)]
        ChatUser[] GetAllUserss();

        [OperationContract(IsOneWay = true)]
        void SendNewMessage(ChatMessage newMessage);

        [OperationContract(IsOneWay = true)]
        void RemoveUser(ChatUser user);

        [OperationContract]
        ChatMessage[] GetNewMessagess(ChatUser user);

        [OperationContract]
        bool CreateNewRoom(ChatUser ourName, string[] users, ChatRoom nameRoom);

        [OperationContract]
        ChatRoom[] GetRooms(ChatUser user);
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

        private string userName;

        [DataMember]
        public string UserName
        {
            get
            {
                return userName;
            }

            set
            {
                userName = value;
            }
        }


        public bool Equals(ChatUser other)
        {
            if (this.UserName == other.UserName)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

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
