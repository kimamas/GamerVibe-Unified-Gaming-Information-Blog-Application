namespace GamesDesktop
{
    partial class AddGame
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
            tbName = new TextBox();
            tbDeveloper = new TextBox();
            rtbDescription = new RichTextBox();
            dtpDate = new DateTimePicker();
            lbInfoGenres = new Label();
            lbInfoPlatforms = new Label();
            clbPlatforms = new CheckedListBox();
            clbGenres = new CheckedListBox();
            lbInfoDate = new Label();
            lbInfoDeveloper = new Label();
            lbInfoDescription = new Label();
            lbInfoName = new Label();
            btSubmit = new Button();
            clbBoardTypes = new CheckedListBox();
            lbInfoTypes = new Label();
            rbVideoGame = new RadioButton();
            rbBoardGame = new RadioButton();
            btUploadPicture = new Button();
            pbPicture = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)pbPicture).BeginInit();
            SuspendLayout();
            // 
            // tbName
            // 
            tbName.Location = new Point(121, 58);
            tbName.Name = "tbName";
            tbName.Size = new Size(189, 23);
            tbName.TabIndex = 1;
            // 
            // tbDeveloper
            // 
            tbDeveloper.Location = new Point(121, 248);
            tbDeveloper.Name = "tbDeveloper";
            tbDeveloper.Size = new Size(189, 23);
            tbDeveloper.TabIndex = 2;
            // 
            // rtbDescription
            // 
            rtbDescription.Location = new Point(121, 110);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.Size = new Size(189, 113);
            rtbDescription.TabIndex = 3;
            rtbDescription.Text = "";
            // 
            // dtpDate
            // 
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(121, 297);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(189, 23);
            dtpDate.TabIndex = 4;
            // 
            // lbInfoGenres
            // 
            lbInfoGenres.AutoSize = true;
            lbInfoGenres.Location = new Point(616, 63);
            lbInfoGenres.Name = "lbInfoGenres";
            lbInfoGenres.Size = new Size(46, 15);
            lbInfoGenres.TabIndex = 6;
            lbInfoGenres.Text = "Genres:";
            // 
            // lbInfoPlatforms
            // 
            lbInfoPlatforms.AutoSize = true;
            lbInfoPlatforms.Location = new Point(825, 63);
            lbInfoPlatforms.Name = "lbInfoPlatforms";
            lbInfoPlatforms.Size = new Size(61, 15);
            lbInfoPlatforms.TabIndex = 7;
            lbInfoPlatforms.Text = "Platforms:";
            // 
            // clbPlatforms
            // 
            clbPlatforms.FormattingEnabled = true;
            clbPlatforms.Location = new Point(896, 62);
            clbPlatforms.Name = "clbPlatforms";
            clbPlatforms.Size = new Size(127, 292);
            clbPlatforms.TabIndex = 8;
            // 
            // clbGenres
            // 
            clbGenres.FormattingEnabled = true;
            clbGenres.Location = new Point(671, 62);
            clbGenres.Name = "clbGenres";
            clbGenres.Size = new Size(127, 292);
            clbGenres.TabIndex = 9;
            // 
            // lbInfoDate
            // 
            lbInfoDate.AutoSize = true;
            lbInfoDate.Location = new Point(31, 301);
            lbInfoDate.Name = "lbInfoDate";
            lbInfoDate.Size = new Size(75, 15);
            lbInfoDate.TabIndex = 10;
            lbInfoDate.Text = "Release date:";
            // 
            // lbInfoDeveloper
            // 
            lbInfoDeveloper.AutoSize = true;
            lbInfoDeveloper.Location = new Point(31, 250);
            lbInfoDeveloper.Name = "lbInfoDeveloper";
            lbInfoDeveloper.Size = new Size(63, 15);
            lbInfoDeveloper.TabIndex = 11;
            lbInfoDeveloper.Text = "Developer:";
            // 
            // lbInfoDescription
            // 
            lbInfoDescription.AutoSize = true;
            lbInfoDescription.Location = new Point(31, 112);
            lbInfoDescription.Name = "lbInfoDescription";
            lbInfoDescription.Size = new Size(70, 15);
            lbInfoDescription.TabIndex = 12;
            lbInfoDescription.Text = "Description:";
            // 
            // lbInfoName
            // 
            lbInfoName.AutoSize = true;
            lbInfoName.Location = new Point(31, 61);
            lbInfoName.Name = "lbInfoName";
            lbInfoName.Size = new Size(42, 15);
            lbInfoName.TabIndex = 13;
            lbInfoName.Text = "Name:";
            // 
            // btSubmit
            // 
            btSubmit.BackColor = Color.DarkSeaGreen;
            btSubmit.Location = new Point(121, 391);
            btSubmit.Name = "btSubmit";
            btSubmit.Size = new Size(189, 35);
            btSubmit.TabIndex = 15;
            btSubmit.Text = "Submit";
            btSubmit.UseVisualStyleBackColor = false;
            btSubmit.Click += btSubmit_Click;
            // 
            // clbBoardTypes
            // 
            clbBoardTypes.FormattingEnabled = true;
            clbBoardTypes.Location = new Point(1102, 62);
            clbBoardTypes.Name = "clbBoardTypes";
            clbBoardTypes.Size = new Size(125, 292);
            clbBoardTypes.TabIndex = 18;
            // 
            // lbInfoTypes
            // 
            lbInfoTypes.AutoSize = true;
            lbInfoTypes.Location = new Point(1054, 63);
            lbInfoTypes.Name = "lbInfoTypes";
            lbInfoTypes.Size = new Size(39, 15);
            lbInfoTypes.TabIndex = 19;
            lbInfoTypes.Text = "Types:";
            // 
            // rbVideoGame
            // 
            rbVideoGame.AutoSize = true;
            rbVideoGame.Location = new Point(671, 30);
            rbVideoGame.Margin = new Padding(3, 2, 3, 2);
            rbVideoGame.Name = "rbVideoGame";
            rbVideoGame.Size = new Size(89, 19);
            rbVideoGame.TabIndex = 20;
            rbVideoGame.TabStop = true;
            rbVideoGame.Text = "Video Game";
            rbVideoGame.UseVisualStyleBackColor = true;
            rbVideoGame.CheckedChanged += rbVideoGame_CheckedChanged;
            // 
            // rbBoardGame
            // 
            rbBoardGame.AutoSize = true;
            rbBoardGame.Location = new Point(1102, 30);
            rbBoardGame.Margin = new Padding(3, 2, 3, 2);
            rbBoardGame.Name = "rbBoardGame";
            rbBoardGame.Size = new Size(90, 19);
            rbBoardGame.TabIndex = 21;
            rbBoardGame.TabStop = true;
            rbBoardGame.Text = "Board Game";
            rbBoardGame.UseVisualStyleBackColor = true;
            rbBoardGame.CheckedChanged += rbBoardGame_CheckedChanged;
            // 
            // btUploadPicture
            // 
            btUploadPicture.Location = new Point(379, 354);
            btUploadPicture.Name = "btUploadPicture";
            btUploadPicture.Size = new Size(159, 23);
            btUploadPicture.TabIndex = 22;
            btUploadPicture.Text = "Upload\\Change picture";
            btUploadPicture.UseVisualStyleBackColor = true;
            btUploadPicture.Click += btUploadPicture_Click;
            // 
            // pbPicture
            // 
            pbPicture.BackgroundImageLayout = ImageLayout.None;
            pbPicture.BorderStyle = BorderStyle.FixedSingle;
            pbPicture.Location = new Point(353, 58);
            pbPicture.Name = "pbPicture";
            pbPicture.Size = new Size(215, 262);
            pbPicture.SizeMode = PictureBoxSizeMode.Zoom;
            pbPicture.TabIndex = 23;
            pbPicture.TabStop = false;
            // 
            // AddGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Cornsilk;
            ClientSize = new Size(1277, 444);
            Controls.Add(pbPicture);
            Controls.Add(btUploadPicture);
            Controls.Add(rbBoardGame);
            Controls.Add(rbVideoGame);
            Controls.Add(lbInfoTypes);
            Controls.Add(clbBoardTypes);
            Controls.Add(btSubmit);
            Controls.Add(lbInfoName);
            Controls.Add(lbInfoDescription);
            Controls.Add(lbInfoDeveloper);
            Controls.Add(lbInfoDate);
            Controls.Add(clbGenres);
            Controls.Add(clbPlatforms);
            Controls.Add(lbInfoPlatforms);
            Controls.Add(lbInfoGenres);
            Controls.Add(dtpDate);
            Controls.Add(rtbDescription);
            Controls.Add(tbDeveloper);
            Controls.Add(tbName);
            Name = "AddGame";
            Text = "Add Game";
            ((System.ComponentModel.ISupportInitialize)pbPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private TextBox tbName;
        private TextBox tbDeveloper;
        private RichTextBox rtbDescription;
        private DateTimePicker dtpDate;
        private Label lbInfoGenres;
        private Label lbInfoPlatforms;
        private CheckedListBox clbPlatforms;
        private CheckedListBox clbGenres;
        private Label lbInfoDate;
        private Label lbInfoDeveloper;
        private Label lbInfoDescription;
        private Label lbInfoName;
        private Button btSubmit;
        private ListBox lbVideoGames;
        private Label lbInfoVideoGames;
        private CheckedListBox clbBoardTypes;
        private Label lbInfoTypes;
        private RadioButton rbVideoGame;
        private RadioButton rbBoardGame;
        private Button btUploadPicture;
        private PictureBox pbPicture;
    }
}