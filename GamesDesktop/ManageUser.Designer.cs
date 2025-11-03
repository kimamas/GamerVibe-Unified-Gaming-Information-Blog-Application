namespace GamesDesktop
{
    partial class ManageUser
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
            btSubmit = new Button();
            lbInfoUsername = new Label();
            lbInfoEmail = new Label();
            lbInfoPassword = new Label();
            label4 = new Label();
            tbUsername = new TextBox();
            dtpDateOfBirth = new DateTimePicker();
            tbPassword = new TextBox();
            tbEmail = new TextBox();
            lbInfoAdmin = new Label();
            cbAdmin = new ComboBox();
            pbPicture = new PictureBox();
            btChangePicture = new Button();
            btGenerateRandomAvatar = new Button();
            ((System.ComponentModel.ISupportInitialize)pbPicture).BeginInit();
            SuspendLayout();
            // 
            // btSubmit
            // 
            btSubmit.BackColor = Color.DarkSeaGreen;
            btSubmit.Location = new Point(221, 322);
            btSubmit.Name = "btSubmit";
            btSubmit.Size = new Size(168, 36);
            btSubmit.TabIndex = 0;
            btSubmit.Text = "Submit";
            btSubmit.UseVisualStyleBackColor = false;
            btSubmit.Click += btSubmit_Click;
            // 
            // lbInfoUsername
            // 
            lbInfoUsername.AutoSize = true;
            lbInfoUsername.Location = new Point(29, 29);
            lbInfoUsername.Name = "lbInfoUsername";
            lbInfoUsername.Size = new Size(63, 15);
            lbInfoUsername.TabIndex = 1;
            lbInfoUsername.Text = "Username:";
            // 
            // lbInfoEmail
            // 
            lbInfoEmail.AutoSize = true;
            lbInfoEmail.Location = new Point(29, 86);
            lbInfoEmail.Name = "lbInfoEmail";
            lbInfoEmail.Size = new Size(39, 15);
            lbInfoEmail.TabIndex = 2;
            lbInfoEmail.Text = "Email:";
            // 
            // lbInfoPassword
            // 
            lbInfoPassword.AutoSize = true;
            lbInfoPassword.Location = new Point(29, 148);
            lbInfoPassword.Name = "lbInfoPassword";
            lbInfoPassword.Size = new Size(60, 15);
            lbInfoPassword.TabIndex = 3;
            lbInfoPassword.Text = "Password:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(29, 221);
            label4.Name = "label4";
            label4.Size = new Size(78, 15);
            label4.TabIndex = 4;
            label4.Text = "Date Of Birth:";
            // 
            // tbUsername
            // 
            tbUsername.Location = new Point(141, 26);
            tbUsername.Name = "tbUsername";
            tbUsername.Size = new Size(168, 23);
            tbUsername.TabIndex = 6;
            // 
            // dtpDateOfBirth
            // 
            dtpDateOfBirth.Format = DateTimePickerFormat.Short;
            dtpDateOfBirth.Location = new Point(141, 215);
            dtpDateOfBirth.Name = "dtpDateOfBirth";
            dtpDateOfBirth.Size = new Size(81, 23);
            dtpDateOfBirth.TabIndex = 7;
            // 
            // tbPassword
            // 
            tbPassword.Location = new Point(141, 145);
            tbPassword.Name = "tbPassword";
            tbPassword.PasswordChar = '*';
            tbPassword.Size = new Size(168, 23);
            tbPassword.TabIndex = 9;
            // 
            // tbEmail
            // 
            tbEmail.Location = new Point(141, 83);
            tbEmail.Name = "tbEmail";
            tbEmail.Size = new Size(168, 23);
            tbEmail.TabIndex = 10;
            // 
            // lbInfoAdmin
            // 
            lbInfoAdmin.AutoSize = true;
            lbInfoAdmin.Location = new Point(29, 275);
            lbInfoAdmin.Name = "lbInfoAdmin";
            lbInfoAdmin.Size = new Size(46, 15);
            lbInfoAdmin.TabIndex = 11;
            lbInfoAdmin.Text = "Admin:";
            // 
            // cbAdmin
            // 
            cbAdmin.FormattingEnabled = true;
            cbAdmin.Items.AddRange(new object[] { "No", "Yes" });
            cbAdmin.Location = new Point(141, 272);
            cbAdmin.Name = "cbAdmin";
            cbAdmin.Size = new Size(81, 23);
            cbAdmin.TabIndex = 12;
            cbAdmin.Text = "No";
            // 
            // pbPicture
            // 
            pbPicture.BackgroundImageLayout = ImageLayout.None;
            pbPicture.BorderStyle = BorderStyle.FixedSingle;
            pbPicture.Location = new Point(343, 26);
            pbPicture.Name = "pbPicture";
            pbPicture.Size = new Size(215, 199);
            pbPicture.SizeMode = PictureBoxSizeMode.Zoom;
            pbPicture.TabIndex = 44;
            pbPicture.TabStop = false;
            // 
            // btChangePicture
            // 
            btChangePicture.Location = new Point(343, 231);
            btChangePicture.Name = "btChangePicture";
            btChangePicture.Size = new Size(215, 24);
            btChangePicture.TabIndex = 43;
            btChangePicture.Text = "Upload\\Change picture (optional)";
            btChangePicture.UseVisualStyleBackColor = true;
            btChangePicture.Click += btChangePicture_Click;
            // 
            // btGenerateRandomAvatar
            // 
            btGenerateRandomAvatar.Location = new Point(343, 261);
            btGenerateRandomAvatar.Name = "btGenerateRandomAvatar";
            btGenerateRandomAvatar.Size = new Size(215, 23);
            btGenerateRandomAvatar.TabIndex = 45;
            btGenerateRandomAvatar.Text = "Generate random avatar";
            btGenerateRandomAvatar.UseVisualStyleBackColor = true;
            btGenerateRandomAvatar.Click += btGenerateRandomAvatar_Click;
            // 
            // ManageUser
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Cornsilk;
            ClientSize = new Size(594, 381);
            Controls.Add(btGenerateRandomAvatar);
            Controls.Add(pbPicture);
            Controls.Add(btChangePicture);
            Controls.Add(cbAdmin);
            Controls.Add(lbInfoAdmin);
            Controls.Add(tbEmail);
            Controls.Add(tbPassword);
            Controls.Add(dtpDateOfBirth);
            Controls.Add(tbUsername);
            Controls.Add(label4);
            Controls.Add(lbInfoPassword);
            Controls.Add(lbInfoEmail);
            Controls.Add(lbInfoUsername);
            Controls.Add(btSubmit);
            Name = "ManageUser";
            Text = "User";
            ((System.ComponentModel.ISupportInitialize)pbPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btSubmit;
        private Label lbInfoUsername;
        private Label lbInfoEmail;
        private Label lbInfoPassword;
        private Label label4;
        private TextBox tbUsername;
        private DateTimePicker dtpDateOfBirth;
        private TextBox tbPassword;
        private TextBox tbEmail;
        private Label lbInfoAdmin;
        private ComboBox cbAdmin;
        private PictureBox pbPicture;
        private Button btChangePicture;
        private Button btGenerateRandomAvatar;
    }
}