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
        private ChatUser name;
        private ChatRoom room = new ChatRoom();
        public ListUsers(IChatService proxy)
        {
            InitializeComponent();
            RealizationCallback realization = new RealizationCallback();
            
            lbUsers.DataSource = realization.GetAllUsers(proxy);
        }

        public ChatUser GetName()
        {
            return name;
        }

        public ChatRoom GetNameRoom()
        {
            return room;
        }
        private void btnOk_Click(object sender, EventArgs e)
        {
            name = (ChatUser)lbUsers.SelectedItem;
            room.NameRoom = tbNameRoom.Text;

            this.Close();
        }
    }
}
