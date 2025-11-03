namespace GamesDesktop
{
    partial class EditGame
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
            lbInfoTypes = new Label();
            clbBoardTypes = new CheckedListBox();
            btSubmit = new Button();
            lbInfoName = new Label();
            lbInfoDescription = new Label();
            lbInfoDeveloper = new Label();
            lbInfoDate = new Label();
            clbGenres = new CheckedListBox();
            clbPlatforms = new CheckedListBox();
            lbInfoPlatforms = new Label();
            lbInfoGenres = new Label();
            dtpDate = new DateTimePicker();
            rtbDescription = new RichTextBox();
            tbDeveloper = new TextBox();
            tbName = new TextBox();
            lbInfoVideo = new Label();
            lbInfoBoard = new Label();
            pbPicture = new PictureBox();
            btChangePicture = new Button();
            ((System.ComponentModel.ISupportInitialize)pbPicture).BeginInit();
            SuspendLayout();
            // 
            // lbInfoTypes
            // 
            lbInfoTypes.AutoSize = true;
            lbInfoTypes.Location = new Point(998, 73);
            lbInfoTypes.Name = "lbInfoTypes";
            lbInfoTypes.Size = new Size(39, 15);
            lbInfoTypes.TabIndex = 38;
            lbInfoTypes.Text = "Types:";
            // 
            // clbBoardTypes
            // 
            clbBoardTypes.FormattingEnabled = true;
            clbBoardTypes.Location = new Point(1046, 72);
            clbBoardTypes.Name = "clbBoardTypes";
            clbBoardTypes.Size = new Size(134, 292);
            clbBoardTypes.TabIndex = 37;
            // 
            // btSubmit
            // 
            btSubmit.BackColor = Color.DarkSeaGreen;
            btSubmit.Location = new Point(108, 409);
            btSubmit.Name = "btSubmit";
            btSubmit.Size = new Size(189, 35);
            btSubmit.TabIndex = 36;
            btSubmit.Text = "Submit";
            btSubmit.UseVisualStyleBackColor = false;
            btSubmit.Click += btSubmit_Click;
            // 
            // lbInfoName
            // 
            lbInfoName.AutoSize = true;
            lbInfoName.Location = new Point(18, 72);
            lbInfoName.Name = "lbInfoName";
            lbInfoName.Size = new Size(42, 15);
            lbInfoName.TabIndex = 34;
            lbInfoName.Text = "Name:";
            // 
            // lbInfoDescription
            // 
            lbInfoDescription.AutoSize = true;
            lbInfoDescription.Location = new Point(18, 123);
            lbInfoDescription.Name = "lbInfoDescription";
            lbInfoDescription.Size = new Size(70, 15);
            lbInfoDescription.TabIndex = 33;
            lbInfoDescription.Text = "Description:";
            // 
            // lbInfoDeveloper
            // 
            lbInfoDeveloper.AutoSize = true;
            lbInfoDeveloper.Location = new Point(18, 261);
            lbInfoDeveloper.Name = "lbInfoDeveloper";
            lbInfoDeveloper.Size = new Size(63, 15);
            lbInfoDeveloper.TabIndex = 32;
            lbInfoDeveloper.Text = "Developer:";
            // 
            // lbInfoDate
            // 
            lbInfoDate.AutoSize = true;
            lbInfoDate.Location = new Point(18, 312);
            lbInfoDate.Name = "lbInfoDate";
            lbInfoDate.Size = new Size(75, 15);
            lbInfoDate.TabIndex = 31;
            lbInfoDate.Text = "Release date:";
            // 
            // clbGenres
            // 
            clbGenres.FormattingEnabled = true;
            clbGenres.Location = new Point(618, 71);
            clbGenres.Name = "clbGenres";
            clbGenres.Size = new Size(125, 292);
            clbGenres.TabIndex = 30;
            // 
            // clbPlatforms
            // 
            clbPlatforms.FormattingEnabled = true;
            clbPlatforms.Location = new Point(840, 72);
            clbPlatforms.Name = "clbPlatforms";
            clbPlatforms.Size = new Size(126, 292);
            clbPlatforms.TabIndex = 29;
            // 
            // lbInfoPlatforms
            // 
            lbInfoPlatforms.AutoSize = true;
            lbInfoPlatforms.Location = new Point(769, 73);
            lbInfoPlatforms.Name = "lbInfoPlatforms";
            lbInfoPlatforms.Size = new Size(61, 15);
            lbInfoPlatforms.TabIndex = 28;
            lbInfoPlatforms.Text = "Platforms:";
            // 
            // lbInfoGenres
            // 
            lbInfoGenres.AutoSize = true;
            lbInfoGenres.Location = new Point(563, 72);
            lbInfoGenres.Name = "lbInfoGenres";
            lbInfoGenres.Size = new Size(46, 15);
            lbInfoGenres.TabIndex = 27;
            lbInfoGenres.Text = "Genres:";
            // 
            // dtpDate
            // 
            dtpDate.Format = DateTimePickerFormat.Short;
            dtpDate.Location = new Point(108, 308);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(189, 23);
            dtpDate.TabIndex = 26;
            // 
            // rtbDescription
            // 
            rtbDescription.Location = new Point(108, 121);
            rtbDescription.Name = "rtbDescription";
            rtbDescription.Size = new Size(189, 113);
            rtbDescription.TabIndex = 25;
            rtbDescription.Text = "";
            // 
            // tbDeveloper
            // 
            tbDeveloper.Location = new Point(108, 259);
            tbDeveloper.Name = "tbDeveloper";
            tbDeveloper.Size = new Size(189, 23);
            tbDeveloper.TabIndex = 24;
            // 
            // tbName
            // 
            tbName.Location = new Point(108, 69);
            tbName.Name = "tbName";
            tbName.Size = new Size(189, 23);
            tbName.TabIndex = 23;
            // 
            // lbInfoVideo
            // 
            lbInfoVideo.AutoSize = true;
            lbInfoVideo.Location = new Point(618, 24);
            lbInfoVideo.Name = "lbInfoVideo";
            lbInfoVideo.Size = new Size(76, 15);
            lbInfoVideo.TabIndex = 39;
            lbInfoVideo.Text = "Video Games";
            // 
            // lbInfoBoard
            // 
            lbInfoBoard.AutoSize = true;
            lbInfoBoard.Location = new Point(1046, 24);
            lbInfoBoard.Name = "lbInfoBoard";
            lbInfoBoard.Size = new Size(77, 15);
            lbInfoBoard.TabIndex = 40;
            lbInfoBoard.Text = "Board Games";
            // 
            // pbPicture
            // 
            pbPicture.BackgroundImageLayout = ImageLayout.None;
            pbPicture.BorderStyle = BorderStyle.FixedSingle;
            pbPicture.Location = new Point(330, 69);
            pbPicture.Name = "pbPicture";
            pbPicture.Size = new Size(215, 262);
            pbPicture.SizeMode = PictureBoxSizeMode.Zoom;
            pbPicture.TabIndex = 42;
            pbPicture.TabStop = false;
            // 
            // btChangePicture
            // 
            btChangePicture.Location = new Point(356, 365);
            btChangePicture.Name = "btChangePicture";
            btChangePicture.Size = new Size(159, 23);
            btChangePicture.TabIndex = 41;
            btChangePicture.Text = "Change picture";
            btChangePicture.UseVisualStyleBackColor = true;
            btChangePicture.Click += btChangePicture_Click;
            // 
            // EditGame
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Cornsilk;
            ClientSize = new Size(1261, 490);
            Controls.Add(pbPicture);
            Controls.Add(btChangePicture);
            Controls.Add(lbInfoBoard);
            Controls.Add(lbInfoVideo);
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
            Name = "EditGame";
            Text = "EditGame";
            ((System.ComponentModel.ISupportInitialize)pbPicture).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Label lbInfoTypes;
        private CheckedListBox clbBoardTypes;
        private Button btSubmit;
        private Label lbInfoName;
        private Label lbInfoDescription;
        private Label lbInfoDeveloper;
        private Label lbInfoDate;
        private CheckedListBox clbGenres;
        private CheckedListBox clbPlatforms;
        private Label lbInfoPlatforms;
        private Label lbInfoGenres;
        private DateTimePicker dtpDate;
        private RichTextBox rtbDescription;
        private TextBox tbDeveloper;
        private TextBox tbName;
        private Label lbInfoVideo;
        private Label lbInfoBoard;
        private PictureBox pbPicture;
        private Button btChangePicture;
    }
}