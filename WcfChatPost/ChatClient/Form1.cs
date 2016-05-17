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
            btnRoom.Enabled = false;
            btnSend.Enabled = false;
        }   
        private ChatUser clientUser;
        private bool isConnected = false;
        public RealizationCallback realization = new RealizationCallback();
        private Dictionary<string, List<string>> oldMessages = new Dictionary<string, List<string>>();


        private void InsertMessage(ChatMessage message)
        {
            StringBuilder builder = new StringBuilder();
            this.tbChat.BeginInvoke(new Action(() => { tbChat.AppendText(builder.AppendFormat("{0} says ({1}):{2}" + Environment.NewLine, message.User.UserName, message.Date, message.Message).ToString()); }));    
        }
        private void UpdateMSG(List<ChatMessage> listMessages)
        {
            
            if (listMessages != null)
            {
                foreach (var message in listMessages)
                {
                    InsertMessage(message);
                }
            }              
        }
        public void UpdateRooms(List<ChatRoom> listRooms)
        {
            if (listRooms.Count > 1)
                tbn_ExitRoom.Enabled = true;
            else
                tbn_ExitRoom.Enabled = false;

            bool flag = false;
            object value = null;

            foreach (ChatRoom room in listRooms)
            {
                if (!lbRooms.Items.Contains(room.NameRoom))
                    lbRooms.Items.Add(room.NameRoom);


                foreach (var roo in lbRooms.Items)
                    if (roo.ToString() == room.NameRoom)
                    {
                        flag = true;
                       
                    }
                    else
                    {
                        flag = false;
                        value = roo;
                    }
                
            }
            if (!flag)
            {
                lbRooms.Items.Remove(value);
            }
            if (label1.Text == "")
            {
                label1.Text = lbRooms.Items[lbRooms.Items.Count-1].ToString();
                lbRooms.SelectedIndex = lbRooms.Items.Count-1;
            }
        }
        public void UpdateUsers(List<ChatUser> listUsers)
        {
            foreach (ChatUser user in listUsers)
                if (!lbUsers.Items.Contains(user.UserName))
                    lbUsers.Items.Add(user.UserName);            
        }
        private bool ConfirmUser(ChatUser ourName)
        {
            Confirm confirm = new Confirm(ourName);
            confirm.ShowDialog(this);

            return confirm.GetFlag();
        }

        ChatRoom UpdateRoom()
        {
            return new ChatRoom() { NameRoom = label1.Text };
        }
        private void MainForm_Load(object sender, EventArgs e)
        {
            realization = new RealizationCallback();

            realization.delConfirmUser += new DelegateOfConfirmUser(ConfirmUser);
            realization.delUpdateUsers += new DelegateOfUpdateUsers(UpdateUsers);
            realization.delUpdateRooms += new DelegateOfUpdateRooms(UpdateRooms);
            realization.delUpdateMsg += new DelegateOfUpdateMessages(UpdateMSG);
            realization.delUpdateRoom += new DelegateOfUpdateRoom(UpdateRoom);
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
                },new ChatRoom() { NameRoom = label1.Text });

                realization.RemoveUser(clientUser);
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

                realization.SendNewMessage(newMessage, new ChatRoom() { NameRoom=label1.Text});
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
                    clientUser = new ChatUser();
                    clientUser = realization.ClientConnect(loginDialog.UserName);

                    if (clientUser != null)
                    {
                        btnRoom.Enabled = true;
                        btnSend.Enabled = true;
                        isConnected = true;
                        btnLogin.Enabled = false;
                        btn_AddUser.Enabled = true;
                    }
                }
            }
            catch (Exception exp)
            {
                MessageBox.Show("Client can't connect" + exp.Message, "FATAL ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CreateRoom(ListUsers formUsers)
        {
            Task t = Task.Run(() =>
            {
                if (realization.CreateNewRoom(clientUser, formUsers.GetName(), formUsers.GetNameRoom()))
                {
                    this.lbRooms.BeginInvoke(new Action(() => { lbRooms.Items.Add(formUsers.GetNameRoom().NameRoom); }));

                    this.lbRooms.BeginInvoke(new Action(() => { lbRooms.SelectedIndex = lbRooms.Items.Count-1; }));
                }
                this.btnRoom.BeginInvoke(new Action(() => { btnRoom.Enabled = true; }));
            });

        }
        private void btnRoom_Click(object sender, EventArgs e)
        {
            btnRoom.Enabled = false;
            ListUsers formUsers = new ListUsers(lbUsers);
            formUsers.ShowDialog(this);
            if (formUsers.GetFlag())
                CreateRoom(formUsers);
        }

        private List<string> GetListMSG()
        {
            return tbChat.Text.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        private void SetListMSG(List<string> list)
        {
            if (list != null)
            {
                foreach (var message in list)
                {
                    tbChat.Text += message + Environment.NewLine;
                }
            }
        }
        private void lbRooms_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (label1.Text != lbRooms.SelectedItem.ToString())
            {

                if ((oldMessages.Count == 0) || (!oldMessages.ContainsKey(label1.Text)))
                {
                    if (oldMessages.Count == 0)
                    {
                        oldMessages.Add(label1.Text, GetListMSG());
                        tbChat.Text = "";
                    }
                    else
                        oldMessages.Add(label1.Text, GetListMSG());
                    
                    label1.Text = lbRooms.SelectedItem.ToString();
                }
                else
                {
                    oldMessages[label1.Text] = GetListMSG();
                    tbChat.Text = "";
                    label1.Text = lbRooms.SelectedItem.ToString();
                    SetListMSG(oldMessages[label1.Text]);
                }

            }
        }

        private void tbn_ExitRoom_Click(object sender, EventArgs e)
        {
            realization.ExitRoom(clientUser,UpdateRoom());
            lbRooms.SelectedIndex = 0;
        }

        private void btn_AddUser_Click(object sender, EventArgs e)
        {
            realization.AddInRoom(new ChatUser() { UserName = lbUsers.SelectedItem.ToString() },clientUser,UpdateRoom());
        }
    }
}
