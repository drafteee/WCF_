using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using WcfChatPost;

namespace ChatClient
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private ChannelFactory<IChatService> remoteFactory;
        private IChatService remoteProxy;
        private ChatUser clientUser;
        private bool isConnected = false;
        private RealizationCallback realization = new RealizationCallback();

        private void InsertMessage(ChatMessage message)
        {
            StringBuilder builder = new StringBuilder();
            tbChat.Text += builder.AppendFormat("\n{0} says ({1}):\n{2}\n", message.User, message.Date, message.Message).ToString();
        }

        private bool ConfirmUser(ChatUser ourName)
        {
            Confirm confirm = new Confirm(ourName);
            confirm.ShowDialog(this);

            return confirm.GetFlag();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            realization = new RealizationCallback();

            realization.delConfirmUser += new DelegateOfConfirmUser(ConfirmUser);
           // realization.GotUserToAddEvent += new GotUserToAddDelegate(AddUserToUsersList);
            // realization.GotUserToDeleteEvent += new GotUserToDeleteDelegate(DeleteUserFromUsersList);
            // realization.GotRoomAddEvent += new GotRoomAddDelegate(AddAllUsersToUsersList);
            // realization.GotRoomDeleteEvent += new GotRoomDeleteDelegate(AddRoomToTabControl);
            // realization.GotRoomEnterEvent += new GotRoomEnterDelegate(AddRoomToTabControl);
            // realization.GotUpdateEvent += new GotUpdateDelegate(AddRoomToTabControl);
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isConnected)
            {
                realization.SendNewMessage(new ChatMessage()
                {
                    Date = DateTime.Now,
                    Message = "Adios",
                    User = clientUser,
                }, remoteProxy);

                realization.RemoveUser(clientUser, remoteProxy);
            }
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbInputMsg.Text))
            {
                ChatMessage newMessage = new ChatMessage()
                {
                    Date = DateTime.Now,
                    Message = tbInputMsg.Text,
                    User = clientUser,
                };

                realization.SendNewMessage(newMessage, remoteProxy);
                InsertMessage(newMessage);
                tbInputMsg.Text = String.Empty;
            }
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                Login loginDialog = new Login();
                loginDialog.ShowDialog(this);

                if (!String.IsNullOrEmpty(loginDialog.UserName))
                {
                    InstanceContext instanceContext = new InstanceContext(new RealizationCallback());

                    remoteFactory = new DuplexChannelFactory<IChatService>(instanceContext, "ChatConfig");
                     remoteProxy = remoteFactory.CreateChannel();
                    clientUser = new ChatUser();
                    clientUser = realization.ClientConnect(loginDialog.UserName, remoteProxy);

                    if (clientUser != null)
                    {
                        timUpdateMsg.Enabled = true;
                        timUpdateUsers.Enabled = true;
                        isConnected = true;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Client can't connect" + exp.Message, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void timUpdateMsg_Tick(object sender, EventArgs e)
        {
            List<ChatMessage> listMessages = realization.GetNewMessages(clientUser, remoteProxy);
            if (listMessages != null)
            {
                foreach (var message in listMessages)
                {
                    InsertMessage(message);
                }
            }
        }

        private void timUpdateUsers_Tick(object sender, EventArgs e)
        {
            List<ChatUser> listUsers = realization.GetAllUsers(remoteProxy);
            lbUsers.DataSource = listUsers;
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            ListUsers formUsers = new ListUsers(remoteProxy);
            formUsers.ShowDialog(this);

            realization.CreateNewRoom(clientUser,formUsers.GetName(),formUsers.GetNameRoom(),remoteProxy);
        }
    }
}
