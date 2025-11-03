namespace GamesDesktop
{
    partial class Users
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btAddUser = new Button();
            btEditUser = new Button();
            lbxUsers = new ListBox();
            lbInfoUsers = new Label();
            pcHome = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pcHome).BeginInit();
            SuspendLayout();
            // 
            // btAddUser
            // 
            btAddUser.BackColor = Color.ForestGreen;
            btAddUser.Location = new Point(12, 294);
            btAddUser.Name = "btAddUser";
            btAddUser.Size = new Size(193, 71);
            btAddUser.TabIndex = 0;
            btAddUser.Text = "Add User";
            btAddUser.UseVisualStyleBackColor = false;
            btAddUser.Click += btAddUser_Click;
            // 
            // btEditUser
            // 
            btEditUser.BackColor = Color.Chocolate;
            btEditUser.Location = new Point(231, 294);
            btEditUser.Name = "btEditUser";
            btEditUser.Size = new Size(193, 71);
            btEditUser.TabIndex = 1;
            btEditUser.Text = "Edit User";
            btEditUser.UseVisualStyleBackColor = false;
            btEditUser.Click += btEditUser_Click;
            // 
            // lbxUsers
            // 
            lbxUsers.FormattingEnabled = true;
            lbxUsers.ItemHeight = 15;
            lbxUsers.Location = new Point(451, 12);
            lbxUsers.Name = "lbxUsers";
            lbxUsers.Size = new Size(193, 244);
            lbxUsers.TabIndex = 2;
            // 
            // lbInfoUsers
            // 
            lbInfoUsers.AutoSize = true;
            lbInfoUsers.Location = new Point(385, 12);
            lbInfoUsers.Name = "lbInfoUsers";
            lbInfoUsers.Size = new Size(55, 15);
            lbInfoUsers.TabIndex = 3;
            lbInfoUsers.Text = "All Users:";
            // 
            // pcHome
            // 
            pcHome.Image = DesktopApp.Properties.Resources.Home;
            pcHome.Location = new Point(12, 12);
            pcHome.Name = "pcHome";
            pcHome.Size = new Size(41, 35);
            pcHome.SizeMode = PictureBoxSizeMode.Zoom;
            pcHome.TabIndex = 4;
            pcHome.TabStop = false;
            pcHome.Click += pcHome_Click;
            // 
            // Users
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonShadow;
            ClientSize = new Size(660, 410);
            Controls.Add(pcHome);
            Controls.Add(lbInfoUsers);
            Controls.Add(lbxUsers);
            Controls.Add(btEditUser);
            Controls.Add(btAddUser);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "Users";
            Text = "Users";
            FormClosing += YourForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pcHome).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btAddUser;
        private Button btEditUser;
        private ListBox lbxUsers;
        private Label lbInfoUsers;
        private PictureBox pcHome;
    }
}