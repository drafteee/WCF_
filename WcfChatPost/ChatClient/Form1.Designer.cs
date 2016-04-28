namespace ChatClient
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSend = new System.Windows.Forms.Button();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.tbChat = new System.Windows.Forms.TextBox();
            this.tbInputMsg = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.timUpdateMsg = new System.Windows.Forms.Timer(this.components);
            this.timUpdateUsers = new System.Windows.Forms.Timer(this.components);
            this.btnRoom = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(368, 230);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(71, 34);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // lbUsers
            // 
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.Location = new System.Drawing.Point(368, 12);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(212, 212);
            this.lbUsers.TabIndex = 1;
            // 
            // tbChat
            // 
            this.tbChat.Location = new System.Drawing.Point(12, 12);
            this.tbChat.Multiline = true;
            this.tbChat.Name = "tbChat";
            this.tbChat.Size = new System.Drawing.Size(350, 212);
            this.tbChat.TabIndex = 2;
            // 
            // tbInputMsg
            // 
            this.tbInputMsg.Location = new System.Drawing.Point(12, 230);
            this.tbInputMsg.Multiline = true;
            this.tbInputMsg.Name = "tbInputMsg";
            this.tbInputMsg.Size = new System.Drawing.Size(350, 34);
            this.tbInputMsg.TabIndex = 3;
            // 
            // btnLogin
            // 
            this.btnLogin.Location = new System.Drawing.Point(445, 230);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(70, 34);
            this.btnLogin.TabIndex = 4;
            this.btnLogin.Text = "Login";
            this.btnLogin.UseVisualStyleBackColor = true;
            this.btnLogin.Click += new System.EventHandler(this.btnLogin_Click);
            // 
            // timUpdateMsg
            // 
            this.timUpdateMsg.Interval = 500;
            this.timUpdateMsg.Tick += new System.EventHandler(this.timUpdateMsg_Tick);
            // 
            // timUpdateUsers
            // 
            this.timUpdateUsers.Interval = 500;
            this.timUpdateUsers.Tick += new System.EventHandler(this.timUpdateUsers_Tick);
            // 
            // btnRoom
            // 
            this.btnRoom.Location = new System.Drawing.Point(521, 230);
            this.btnRoom.Name = "btnRoom";
            this.btnRoom.Size = new System.Drawing.Size(59, 34);
            this.btnRoom.TabIndex = 5;
            this.btnRoom.Text = "Room";
            this.btnRoom.UseVisualStyleBackColor = true;
            this.btnRoom.Click += new System.EventHandler(this.btnRoom_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 276);
            this.Controls.Add(this.btnRoom);
            this.Controls.Add(this.btnLogin);
            this.Controls.Add(this.tbInputMsg);
            this.Controls.Add(this.tbChat);
            this.Controls.Add(this.lbUsers);
            this.Controls.Add(this.btnSend);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.TextBox tbChat;
        private System.Windows.Forms.TextBox tbInputMsg;
        private System.Windows.Forms.Button btnLogin;
        private System.Windows.Forms.Timer timUpdateMsg;
        private System.Windows.Forms.Timer timUpdateUsers;
        private System.Windows.Forms.Button btnRoom;
    }
}

