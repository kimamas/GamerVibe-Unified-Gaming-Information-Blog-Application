namespace GamesDesktop
{
    partial class Filtering
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
            cbFiltering = new ComboBox();
            lbInfoFiltering = new Label();
            tbName = new TextBox();
            tbDeveloper = new TextBox();
            btConfirm = new Button();
            lbInfoName = new Label();
            lbInfoDeveloper = new Label();
            dtpDate = new DateTimePicker();
            lbInfoDate = new Label();
            clbBoardTypes = new CheckedListBox();
            clbGenres = new CheckedListBox();
            clbPlatforms = new CheckedListBox();
            cbDate = new ComboBox();
            lbInfoGenres = new Label();
            lbInfoTypes = new Label();
            lbInfoPlatforms = new Label();
            SuspendLayout();
            // 
            // cbFiltering
            // 
            cbFiltering.FormattingEnabled = true;
            cbFiltering.Items.AddRange(new object[] { "Most Relevant", "Name ascending", "Name descending", "Top Rated", "Most Viewed", "Video Games", "Board Games" });
            cbFiltering.Location = new Point(102, 23);
            cbFiltering.Name = "cbFiltering";
            cbFiltering.Size = new Size(147, 23);
            cbFiltering.TabIndex = 0;
            cbFiltering.Text = "Most Relevant";
            // 
            // lbInfoFiltering
            // 
            lbInfoFiltering.AutoSize = true;
            lbInfoFiltering.Location = new Point(18, 26);
            lbInfoFiltering.Name = "lbInfoFiltering";
            lbInfoFiltering.Size = new Size(53, 15);
            lbInfoFiltering.TabIndex = 1;
            lbInfoFiltering.Text = "Filtering:";
            // 
            // tbName
            // 
            tbName.Location = new Point(102, 70);
            tbName.Name = "tbName";
            tbName.Size = new Size(147, 23);
            tbName.TabIndex = 2;
            // 
            // tbDeveloper
            // 
            tbDeveloper.Location = new Point(102, 120);
            tbDeveloper.Name = "tbDeveloper";
            tbDeveloper.Size = new Size(147, 23);
            tbDeveloper.TabIndex = 3;
            // 
            // btConfirm
            // 
            btConfirm.BackColor = Color.DarkSeaGreen;
            btConfirm.Location = new Point(102, 238);
            btConfirm.Name = "btConfirm";
            btConfirm.Size = new Size(147, 29);
            btConfirm.TabIndex = 4;
            btConfirm.Text = "Confirm";
            btConfirm.UseVisualStyleBackColor = false;
            btConfirm.Click += btConfirm_Click;
            // 
            // lbInfoName
            // 
            lbInfoName.AutoSize = true;
            lbInfoName.Location = new Point(18, 73);
            lbInfoName.Name = "lbInfoName";
            lbInfoName.Size = new Size(42, 15);
            lbInfoName.TabIndex = 5;
            lbInfoName.Text = "Name:";
            // 
            // lbInfoDeveloper
            // 
            lbInfoDeveloper.AutoSize = true;
            lbInfoDeveloper.Location = new Point(18, 128);
            lbInfoDeveloper.Name = "lbInfoDeveloper";
            lbInfoDeveloper.Size = new Size(63, 15);
            lbInfoDeveloper.TabIndex = 6;
            lbInfoDeveloper.Text = "Developer:";
            // 
            // dtpDate
            // 
            dtpDate.Format = DateTimePickerFormat.Custom;
            dtpDate.Location = new Point(102, 172);
            dtpDate.Name = "dtpDate";
            dtpDate.Size = new Size(82, 23);
            dtpDate.TabIndex = 7;
            // 
            // lbInfoDate
            // 
            lbInfoDate.AutoSize = true;
            lbInfoDate.Location = new Point(18, 178);
            lbInfoDate.Name = "lbInfoDate";
            lbInfoDate.Size = new Size(74, 15);
            lbInfoDate.TabIndex = 8;
            lbInfoDate.Text = "Release Year:";
            // 
            // clbBoardTypes
            // 
            clbBoardTypes.FormattingEnabled = true;
            clbBoardTypes.Location = new Point(698, 47);
            clbBoardTypes.Name = "clbBoardTypes";
            clbBoardTypes.Size = new Size(131, 220);
            clbBoardTypes.TabIndex = 26;
            // 
            // clbGenres
            // 
            clbGenres.FormattingEnabled = true;
            clbGenres.Location = new Point(312, 47);
            clbGenres.Name = "clbGenres";
            clbGenres.Size = new Size(130, 220);
            clbGenres.TabIndex = 25;
            // 
            // clbPlatforms
            // 
            clbPlatforms.FormattingEnabled = true;
            clbPlatforms.Location = new Point(503, 47);
            clbPlatforms.Name = "clbPlatforms";
            clbPlatforms.Size = new Size(129, 220);
            clbPlatforms.TabIndex = 24;
            // 
            // cbDate
            // 
            cbDate.FormattingEnabled = true;
            cbDate.Items.AddRange(new object[] { "No", "Yes" });
            cbDate.Location = new Point(199, 172);
            cbDate.Name = "cbDate";
            cbDate.Size = new Size(50, 23);
            cbDate.TabIndex = 32;
            cbDate.Text = "No";
            cbDate.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // lbInfoGenres
            // 
            lbInfoGenres.AutoSize = true;
            lbInfoGenres.Location = new Point(312, 26);
            lbInfoGenres.Name = "lbInfoGenres";
            lbInfoGenres.Size = new Size(46, 15);
            lbInfoGenres.TabIndex = 33;
            lbInfoGenres.Text = "Genres:";
            // 
            // lbInfoTypes
            // 
            lbInfoTypes.AutoSize = true;
            lbInfoTypes.Location = new Point(698, 26);
            lbInfoTypes.Name = "lbInfoTypes";
            lbInfoTypes.Size = new Size(39, 15);
            lbInfoTypes.TabIndex = 34;
            lbInfoTypes.Text = "Types:";
            // 
            // lbInfoPlatforms
            // 
            lbInfoPlatforms.AutoSize = true;
            lbInfoPlatforms.Location = new Point(503, 26);
            lbInfoPlatforms.Name = "lbInfoPlatforms";
            lbInfoPlatforms.Size = new Size(61, 15);
            lbInfoPlatforms.TabIndex = 35;
            lbInfoPlatforms.Text = "Platforms:";
            // 
            // Filtering
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Cornsilk;
            ClientSize = new Size(873, 310);
            Controls.Add(lbInfoPlatforms);
            Controls.Add(lbInfoTypes);
            Controls.Add(lbInfoGenres);
            Controls.Add(cbDate);
            Controls.Add(clbBoardTypes);
            Controls.Add(clbGenres);
            Controls.Add(clbPlatforms);
            Controls.Add(lbInfoDate);
            Controls.Add(dtpDate);
            Controls.Add(lbInfoDeveloper);
            Controls.Add(lbInfoName);
            Controls.Add(btConfirm);
            Controls.Add(tbDeveloper);
            Controls.Add(tbName);
            Controls.Add(lbInfoFiltering);
            Controls.Add(cbFiltering);
            FormBorderStyle = FormBorderStyle.None;
            Name = "Filtering";
            Text = "Filtering";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ComboBox cbFiltering;
        private Label lbInfoFiltering;
        private TextBox tbName;
        private TextBox tbDeveloper;
        private Button btConfirm;
        private Label lbInfoName;
        private Label lbInfoDeveloper;
        private DateTimePicker dtpDate;
        private Label lbInfoDate;
        private CheckedListBox clbBoardTypes;
        private CheckedListBox clbGenres;
        private CheckedListBox clbPlatforms;
        private ComboBox cbDate;
        private Label lbInfoGenres;
        private Label lbInfoTypes;
        private Label lbInfoPlatforms;
    }
}