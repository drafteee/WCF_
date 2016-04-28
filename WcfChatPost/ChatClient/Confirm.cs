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
    public partial class Confirm : Form
    {
        private string name;
        private bool flag;
        public Confirm(ChatUser ourName)
        {
            InitializeComponent();
            name = ourName.UserName;
        }

        private void Confirm_Load(object sender, EventArgs e)
        {
            lbConfirmText.Text = name + " хочет создать с вами приватную комнату!";
        }

        public bool GetFlag()
        {
            return flag;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            flag = true;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            flag = false;
            this.Close();
        }
    }
}
