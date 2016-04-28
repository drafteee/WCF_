using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChatClient
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }
        public string UserName { get; set; }

        private void btnAcceptLogin_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(tbLogin.Text))
            {
                this.UserName = tbLogin.Text;
                this.Close();
            }
            else
                MessageBox.Show("Input your Name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
