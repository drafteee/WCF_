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
            this.btnSend = new System.Windows.Forms.Button();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.tbChat = new System.Windows.Forms.TextBox();
            this.tbInputMsg = new System.Windows.Forms.TextBox();
            this.btnLogin = new System.Windows.Forms.Button();
            this.btnRoom = new System.Windows.Forms.Button();
            this.lbRooms = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tbn_ExitRoom = new System.Windows.Forms.Button();
            this.btn_AddUser = new System.Windows.Forms.Button();
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
            this.tbChat.Location = new System.Drawing.Point(12, 26);
            this.tbChat.Multiline = true;
            this.tbChat.Name = "tbChat";
            this.tbChat.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.tbChat.Size = new System.Drawing.Size(350, 198);
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
            // lbRooms
            // 
            this.lbRooms.FormattingEnabled = true;
            this.lbRooms.Location = new System.Drawing.Point(586, 12);
            this.lbRooms.Name = "lbRooms";
            this.lbRooms.Size = new System.Drawing.Size(182, 212);
            this.lbRooms.TabIndex = 6;
            this.lbRooms.SelectedIndexChanged += new System.EventHandler(this.lbRooms_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 13);
            this.label1.TabIndex = 7;
            // 
            // tbn_ExitRoom
            // 
            this.tbn_ExitRoom.Enabled = false;
            this.tbn_ExitRoom.Location = new System.Drawing.Point(586, 230);
            this.tbn_ExitRoom.Name = "tbn_ExitRoom";
            this.tbn_ExitRoom.Size = new System.Drawing.Size(94, 34);
            this.tbn_ExitRoom.TabIndex = 8;
            this.tbn_ExitRoom.Text = "Exit";
            this.tbn_ExitRoom.UseVisualStyleBackColor = true;
            this.tbn_ExitRoom.Click += new System.EventHandler(this.tbn_ExitRoom_Click);
            // 
            // btn_AddUser
            // 
            this.btn_AddUser.Enabled = false;
            this.btn_AddUser.Location = new System.Drawing.Point(684, 230);
            this.btn_AddUser.Name = "btn_AddUser";
            this.btn_AddUser.Size = new System.Drawing.Size(84, 34);
            this.btn_AddUser.TabIndex = 9;
            this.btn_AddUser.Text = "AddUser";
            this.btn_AddUser.UseVisualStyleBackColor = true;
            this.btn_AddUser.Click += new System.EventHandler(this.btn_AddUser_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 276);
            this.Controls.Add(this.btn_AddUser);
            this.Controls.Add(this.tbn_ExitRoom);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbRooms);
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
        private System.Windows.Forms.Button btnRoom;
        private System.Windows.Forms.ListBox lbRooms;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button tbn_ExitRoom;
        private System.Windows.Forms.Button btn_AddUser;
    }
}

