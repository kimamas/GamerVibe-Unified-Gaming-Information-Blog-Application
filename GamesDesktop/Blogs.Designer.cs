namespace DesktopApp
{
    partial class Blogs
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
            pcHome = new PictureBox();
            lbInfoBlogs = new Label();
            lbxBlogs = new ListBox();
            btEditBlog = new Button();
            btAddBlog = new Button();
            ((System.ComponentModel.ISupportInitialize)pcHome).BeginInit();
            SuspendLayout();
            // 
            // pcHome
            // 
            pcHome.Image = Properties.Resources.Home;
            pcHome.Location = new Point(12, 12);
            pcHome.Name = "pcHome";
            pcHome.Size = new Size(41, 35);
            pcHome.SizeMode = PictureBoxSizeMode.Zoom;
            pcHome.TabIndex = 9;
            pcHome.TabStop = false;
            pcHome.Click += pcHome_Click;
            // 
            // lbInfoBlogs
            // 
            lbInfoBlogs.AutoSize = true;
            lbInfoBlogs.Location = new Point(351, 12);
            lbInfoBlogs.Name = "lbInfoBlogs";
            lbInfoBlogs.Size = new Size(56, 15);
            lbInfoBlogs.TabIndex = 8;
            lbInfoBlogs.Text = "All Blogs:";
            // 
            // lbxBlogs
            // 
            lbxBlogs.FormattingEnabled = true;
            lbxBlogs.ItemHeight = 15;
            lbxBlogs.Location = new Point(418, 12);
            lbxBlogs.Name = "lbxBlogs";
            lbxBlogs.Size = new Size(193, 244);
            lbxBlogs.TabIndex = 7;
            lbxBlogs.DoubleClick += lbxBlogs_DoubleClick;
            // 
            // btEditBlog
            // 
            btEditBlog.BackColor = Color.Chocolate;
            btEditBlog.Location = new Point(418, 294);
            btEditBlog.Name = "btEditBlog";
            btEditBlog.Size = new Size(193, 71);
            btEditBlog.TabIndex = 6;
            btEditBlog.Text = "Edit Blog";
            btEditBlog.UseVisualStyleBackColor = false;
            btEditBlog.Click += btEditBlog_Click;
            // 
            // btAddBlog
            // 
            btAddBlog.BackColor = Color.ForestGreen;
            btAddBlog.Location = new Point(62, 294);
            btAddBlog.Name = "btAddBlog";
            btAddBlog.Size = new Size(193, 71);
            btAddBlog.TabIndex = 5;
            btAddBlog.Text = "Add Blog";
            btAddBlog.UseVisualStyleBackColor = false;
            btAddBlog.Click += btAddBlog_Click;
            // 
            // Blogs
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.Cornsilk;
            ClientSize = new Size(634, 389);
            Controls.Add(pcHome);
            Controls.Add(lbInfoBlogs);
            Controls.Add(lbxBlogs);
            Controls.Add(btEditBlog);
            Controls.Add(btAddBlog);
            ForeColor = SystemColors.ControlText;
            FormBorderStyle = FormBorderStyle.SizableToolWindow;
            Name = "Blogs";
            Text = "Blogs";
            FormClosing += YourForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)pcHome).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox pcHome;
        private Label lbInfoBlogs;
        private ListBox lbxBlogs;
        private Button btEditBlog;
        private Button btAddBlog;
    }
}