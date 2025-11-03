namespace GamesDesktop
{
    partial class Games
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
            btAddGame = new Button();
            btEditGame = new Button();
            lbxGames = new ListBox();
            lbInfoGames = new Label();
            btFiltering = new Button();
            pcHome = new PictureBox();
            btDeleteGame = new Button();
            ((System.ComponentModel.ISupportInitialize)pcHome).BeginInit();
            SuspendLayout();
            // 
            // btAddGame
            // 
            btAddGame.BackColor = Color.ForestGreen;
            btAddGame.Location = new Point(34, 285);
            btAddGame.Margin = new Padding(3, 2, 3, 2);
            btAddGame.Name = "btAddGame";
            btAddGame.Size = new Size(222, 90);
            btAddGame.TabIndex = 0;
            btAddGame.Text = "Add Game";
            btAddGame.UseVisualStyleBackColor = false;
            btAddGame.Click += btAddGame_Click;
            // 
            // btEditGame
            // 
            btEditGame.BackColor = Color.Chocolate;
            btEditGame.Location = new Point(305, 285);
            btEditGame.Margin = new Padding(3, 2, 3, 2);
            btEditGame.Name = "btEditGame";
            btEditGame.Size = new Size(222, 90);
            btEditGame.TabIndex = 1;
            btEditGame.Text = "Edit Game";
            btEditGame.UseVisualStyleBackColor = false;
            btEditGame.Click += btEditGame_Click;
            // 
            // lbxGames
            // 
            lbxGames.FormattingEnabled = true;
            lbxGames.ItemHeight = 15;
            lbxGames.Location = new Point(558, 11);
            lbxGames.Margin = new Padding(3, 2, 3, 2);
            lbxGames.Name = "lbxGames";
            lbxGames.Size = new Size(222, 244);
            lbxGames.TabIndex = 2;
            lbxGames.DoubleClick += lbxGames_DoubleClick;
            // 
            // lbInfoGames
            // 
            lbInfoGames.AutoSize = true;
            lbInfoGames.Location = new Point(489, 15);
            lbInfoGames.Name = "lbInfoGames";
            lbInfoGames.Size = new Size(63, 15);
            lbInfoGames.TabIndex = 3;
            lbInfoGames.Text = "All Games:";
            // 
            // btFiltering
            // 
            btFiltering.BackColor = Color.Cyan;
            btFiltering.Location = new Point(395, 11);
            btFiltering.Name = "btFiltering";
            btFiltering.Size = new Size(75, 23);
            btFiltering.TabIndex = 4;
            btFiltering.Text = "Filtering";
            btFiltering.UseVisualStyleBackColor = false;
            btFiltering.Click += btFiltering_Click;
            // 
            // pcHome
            // 
            pcHome.Image = DesktopApp.Properties.Resources.Home;
            pcHome.Location = new Point(12, 11);
            pcHome.Name = "pcHome";
            pcHome.Size = new Size(40, 40);
            pcHome.SizeMode = PictureBoxSizeMode.Zoom;
            pcHome.TabIndex = 5;
            pcHome.TabStop = false;
            pcHome.Click += pictureBox1_Click;
            // 
            // btDeleteGame
            // 
            btDeleteGame.BackColor = Color.Red;
            btDeleteGame.Location = new Point(558, 285);
            btDeleteGame.Margin = new Padding(3, 2, 3, 2);
            btDeleteGame.Name = "btDeleteGame";
            btDeleteGame.Size = new Size(222, 90);
            btDeleteGame.TabIndex = 6;
            btDeleteGame.Text = "Delete Game";
            btDeleteGame.UseVisualStyleBackColor = false;
            btDeleteGame.Click += btDeleteGame_Click;
            // 
            // Games
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ButtonShadow;
            ClientSize = new Size(846, 410);
            Controls.Add(btDeleteGame);
            Controls.Add(pcHome);
            Controls.Add(btFiltering);
            Controls.Add(lbInfoGames);
            Controls.Add(lbxGames);
            Controls.Add(btEditGame);
            Controls.Add(btAddGame);
            Margin = new Padding(3, 2, 3, 2);
            Name = "Games";
            Text = "Games";
            FormClosing += YourForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pcHome).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btAddGame;
        private Button btEditGame;
        private ListBox lbxGames;
        private Label lbInfoGames;
        private Button btFiltering;
        private PictureBox pcHome;
        private Button btDeleteGame;
    }
}