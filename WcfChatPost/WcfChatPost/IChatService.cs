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
        [OperationContract(IsOneWay = true)]
        void CreateRoom(ChatUser ourName, ChatUser requiredName, ChatRoom nameRoom);
    }


    [ServiceContract(SessionMode = SessionMode.Required, CallbackContract = typeof(IChatCallback))]
    public interface IChatService
    {
        [OperationContract(IsOneWay = false)]
        ChatUser ClientConnect(string userName);

        [OperationContract(IsOneWay = false)]
        List<ChatUser> GetAllUsers();

        [OperationContract(IsOneWay = true)]
        void SendNewMessage(ChatMessage newMessage);

        [OperationContract(IsOneWay = true)]
        void RemoveUser(ChatUser user);
        [OperationContract(IsOneWay = false)]
        bool CreateNewRoom(ChatUser ourName, ChatUser name, ChatRoom nameRoom);

    }


    [DataContract]
    public class ChatRoom
    {
        private string nameRoom;

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
    public class ChatUser
    {
        
        private string userName, ipAddress, hostName;

        [DataMember]
        public string HostName
        {
            get
            {
                return hostName;
            }

            set
            {
                hostName = value;
            }
        }

        [DataMember]
        public string IpAddress
        {
            get
            {
                return ipAddress;
            }

            set
            {
                ipAddress = value;
            }
        }

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
