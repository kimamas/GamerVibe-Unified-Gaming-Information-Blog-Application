namespace GamesDesktop
{
    partial class Main
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btGames = new Button();
            btUsers = new Button();
            btBlogs = new Button();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btGames
            // 
            btGames.Location = new Point(292, 342);
            btGames.Name = "btGames";
            btGames.Size = new Size(219, 44);
            btGames.TabIndex = 0;
            btGames.Text = "Games";
            btGames.UseVisualStyleBackColor = true;
            btGames.Click += btGames_Click;
            // 
            // btUsers
            // 
            btUsers.Location = new Point(27, 342);
            btUsers.Name = "btUsers";
            btUsers.Size = new Size(219, 44);
            btUsers.TabIndex = 1;
            btUsers.Text = "Users";
            btUsers.UseVisualStyleBackColor = true;
            btUsers.Click += btUsers_Click;
            // 
            // btBlogs
            // 
            btBlogs.Location = new Point(553, 342);
            btBlogs.Name = "btBlogs";
            btBlogs.Size = new Size(219, 44);
            btBlogs.TabIndex = 2;
            btBlogs.Text = "Blogs";
            btBlogs.UseVisualStyleBackColor = true;
            btBlogs.Click += btBlogs_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = DesktopApp.Properties.Resources.Logo;
            pictureBox1.Location = new Point(278, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(255, 238);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 3;
            pictureBox1.TabStop = false;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Peru;
            ClientSize = new Size(790, 450);
            Controls.Add(pictureBox1);
            Controls.Add(btBlogs);
            Controls.Add(btUsers);
            Controls.Add(btGames);
            Name = "Main";
            Text = "Main";
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btGames;
        private Button btUsers;
        private Button btBlogs;
        private PictureBox pictureBox1;
    }
}
