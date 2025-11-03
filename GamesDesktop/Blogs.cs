using BLLClassLibrary.Entity;
using BLLClassLibrary.Managers;
using BusinessLayer.Managers;
using DALClassLibrary;
using GamesDesktop;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DesktopApp
{
    public partial class Blogs : Form
    {
        private readonly Main main;
        private readonly BlogManager blogManager;
        private readonly GameManager gameManager;
        private readonly UserManager userManager;
        private bool closeApp = true;
        public Blogs(Main main)
        {
            InitializeComponent();
            this.main = main;
            blogManager = new BlogManager(new BlogRepository());
            gameManager = new GameManager(new GameRepository());
            userManager = new UserManager(new UserRepository());
            RefreshBlogs();
        }
        private void YourForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeApp) { Application.Exit(); }
        }

        private void pcHome_Click(object sender, EventArgs e)
        {
            closeApp = false;
            this.Close();
            main.Show();
        }

        private void btAddBlog_Click(object sender, EventArgs e)
        {
            ManageBlog addBlog = new ManageBlog(blogManager, gameManager, userManager);
            addBlog.ShowDialog();
            RefreshBlogs();
        }
        private void RefreshBlogs()
        {
            lbxBlogs.Items.Clear();
            lbxBlogs.Items.AddRange(blogManager.GetBlogs().ToArray());
        }

        private void btEditBlog_Click(object sender, EventArgs e)
        {
            if (lbxBlogs.SelectedIndex > -1)
            {
                try
                {
                    Blog blog = lbxBlogs.SelectedItem as Blog;
                    if (blog == null) { throw new NullReferenceException("Blog was not found"); }
                    if (userManager.FindUserByUsername(blog.UserUsername).isAdmin)
                    {
                        ManageBlog addBlog = new ManageBlog(blogManager, gameManager, userManager, blog);
                        addBlog.ShowDialog();
                        RefreshBlogs();
                    }
                    else
                    {
                        MessageBox.Show("Admins can edit blogs posted by admins");
                    }
                }
                catch (NullReferenceException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception) { MessageBox.Show("Try again later"); }
            }
            else
            {
                MessageBox.Show("Select Blog");
            }
        }

        private void lbxBlogs_DoubleClick(object sender, EventArgs e)
        {
            Blog blog = (Blog)lbxBlogs.SelectedItem;
            Game game = gameManager.FindGameByName(blog.GameName);
            User user = userManager.FindUserByUsername(blog.UserUsername);
            MessageBox.Show($"Game: {game.name}\n\n" +
                $"User: {user.username}\n\n" +
                $"Category: {blog.Category}\n\n" +
                $"Description: {blog.Description}");
        }
    }
}
