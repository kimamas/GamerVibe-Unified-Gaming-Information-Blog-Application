namespace DesktopApp
{
    partial class ManageBlog
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
            lbInfoName = new Label();
            lbInfoDescription = new Label();
            lbInfoGame = new Label();
            btSubmit = new Button();
            tbName = new TextBox();
            tbDescription = new TextBox();
            cbGames = new ComboBox();
            cbCategory = new ComboBox();
            label1 = new Label();
            lbInfoUser = new Label();
            cbUsers = new ComboBox();
            SuspendLayout();
            // 
            // lbInfoName
            // 
            lbInfoName.AutoSize = true;
            lbInfoName.Location = new Point(27, 30);
            lbInfoName.Name = "lbInfoName";
            lbInfoName.Size = new Size(42, 15);
            lbInfoName.TabIndex = 0;
            lbInfoName.Text = "Name:";
            // 
            // lbInfoDescription
            // 
            lbInfoDescription.AutoSize = true;
            lbInfoDescription.Location = new Point(27, 76);
            lbInfoDescription.Name = "lbInfoDescription";
            lbInfoDescription.Size = new Size(70, 15);
            lbInfoDescription.TabIndex = 1;
            lbInfoDescription.Text = "Description:";
            // 
            // lbInfoGame
            // 
            lbInfoGame.AutoSize = true;
            lbInfoGame.Location = new Point(27, 129);
            lbInfoGame.Name = "lbInfoGame";
            lbInfoGame.Size = new Size(41, 15);
            lbInfoGame.TabIndex = 2;
            lbInfoGame.Text = "Game:";
            // 
            // btSubmit
            // 
            btSubmit.BackColor = Color.DarkSeaGreen;
            btSubmit.Location = new Point(176, 296);
            btSubmit.Margin = new Padding(3, 2, 3, 2);
            btSubmit.Name = "btSubmit";
            btSubmit.Size = new Size(158, 36);
            btSubmit.TabIndex = 3;
            btSubmit.Text = "Submit";
            btSubmit.UseVisualStyleBackColor = false;
            btSubmit.Click += btSubmit_Click;
            // 
            // tbName
            // 
            tbName.Location = new Point(176, 28);
            tbName.Margin = new Padding(3, 2, 3, 2);
            tbName.Name = "tbName";
            tbName.Size = new Size(159, 23);
            tbName.TabIndex = 4;
            // 
            // tbDescription
            // 
            tbDescription.Location = new Point(176, 74);
            tbDescription.Margin = new Padding(3, 2, 3, 2);
            tbDescription.Name = "tbDescription";
            tbDescription.Size = new Size(159, 23);
            tbDescription.TabIndex = 5;
            // 
            // cbGames
            // 
            cbGames.Enabled = false;
            cbGames.FormattingEnabled = true;
            cbGames.Location = new Point(176, 127);
            cbGames.Margin = new Padding(3, 2, 3, 2);
            cbGames.Name = "cbGames";
            cbGames.Size = new Size(159, 23);
            cbGames.TabIndex = 6;
            // 
            // cbCategory
            // 
            cbCategory.FormattingEnabled = true;
            cbCategory.Location = new Point(176, 178);
            cbCategory.Margin = new Padding(3, 2, 3, 2);
            cbCategory.Name = "cbCategory";
            cbCategory.Size = new Size(159, 23);
            cbCategory.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(27, 181);
            label1.Name = "label1";
            label1.Size = new Size(58, 15);
            label1.TabIndex = 9;
            label1.Text = "Category:";
            // 
            // lbInfoUser
            // 
            lbInfoUser.AutoSize = true;
            lbInfoUser.Location = new Point(27, 232);
            lbInfoUser.Name = "lbInfoUser";
            lbInfoUser.Size = new Size(33, 15);
            lbInfoUser.TabIndex = 11;
            lbInfoUser.Text = "User:";
            // 
            // cbUsers
            // 
            cbUsers.Enabled = false;
            cbUsers.FormattingEnabled = true;
            cbUsers.Location = new Point(176, 230);
            cbUsers.Margin = new Padding(3, 2, 3, 2);
            cbUsers.Name = "cbUsers";
            cbUsers.Size = new Size(159, 23);
            cbUsers.TabIndex = 10;
            // 
            // ManageBlog
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Cornsilk;
            ClientSize = new Size(436, 358);
            Controls.Add(lbInfoUser);
            Controls.Add(cbUsers);
            Controls.Add(label1);
            Controls.Add(cbCategory);
            Controls.Add(cbGames);
            Controls.Add(tbDescription);
            Controls.Add(tbName);
            Controls.Add(btSubmit);
            Controls.Add(lbInfoGame);
            Controls.Add(lbInfoDescription);
            Controls.Add(lbInfoName);
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "ManageBlog";
            Text = "ManageBlog";
            Load += ManageBlog_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lbInfoName;
        private Label lbInfoDescription;
        private Label lbInfoGame;
        private Button btSubmit;
        private TextBox tbName;
        private TextBox tbDescription;
        private ComboBox cbGames;
        private ComboBox cbCategory;
        private Label label1;
        private Label lbInfoUser;
        private ComboBox cbUsers;
    }
}