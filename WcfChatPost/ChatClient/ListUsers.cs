using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WcfChatPost;

namespace ChatClient
{
    public partial class ListUsers : Form
    {
        private string[] users;
        private ChatRoom room = new ChatRoom();
        private bool flag= false;
        public ListUsers(ListBox lb)
        {
            InitializeComponent();
            foreach (string user in lb.Items)
                if (!lbUsers.Items.Contains(user))

                    lbUsers.Items.Add(user);

        }

        public string[] GetName()
        {
            return users;
        }

        public ChatRoom GetNameRoom()
        {
            return room;
        }
        public bool GetFlag()
        {
            return flag;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            flag = true;
            int size = lbUsers.SelectedItems.Count;
            users = new string[size];
            for(int i=0;i<size ; i++)
            {
                users[i] = lbUsers.SelectedItems[i].ToString();
            }
            room.NameRoom = tbNameRoom.Text;
            this.Close();
        }
    }
}
