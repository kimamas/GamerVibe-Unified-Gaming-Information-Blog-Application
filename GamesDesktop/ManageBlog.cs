using BLLClassLibrary.Managers;
using System.Data;
using BLLClassLibrary.Entity;
using BusinessLayer.Managers;
using BLLClassLibrary.Exceptions;
using System.Data.SqlClient;
namespace DesktopApp
{
    public partial class ManageBlog : Form
    {
        private readonly BlogManager blogManager;
        private readonly GameManager gameManager;
        private readonly UserManager userManager;
        private readonly Blog blog;
        private readonly bool update = false;
        public ManageBlog(BlogManager blogManager, GameManager gameManager, UserManager userManager)
        {
            InitializeComponent();
            this.blogManager = blogManager;
            this.gameManager = gameManager;
            this.userManager = userManager;
            FillComboBoxes();
            cbGames.Enabled = true;
            cbUsers.Enabled = true;
        }
        public ManageBlog(BlogManager blogManager, GameManager gameManager, UserManager userManager, Blog blog)
        {
            InitializeComponent();
            this.blogManager = blogManager;
            this.gameManager = gameManager;
            this.userManager = userManager;
            this.blog = blog;
            update = true;
            FillComboBoxes();
        }


        private void ManageBlog_Load(object sender, EventArgs e)
        {
        }

        private void btSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(tbName.Text) && !string.IsNullOrEmpty(tbDescription.Text))
                {
                    if (cbGames.SelectedIndex > -1 && cbUsers.SelectedIndex > -1 && cbCategory.SelectedIndex > -1)
                    {
                        if (update)
                        {
                            blogManager.UpdateBlog(blog.BlogId, blog.Name, tbName.Text, tbDescription.Text, (cbGames.SelectedItem as Game).name, (cbUsers.SelectedItem as User).username, cbCategory.SelectedItem.ToString());
                            MessageBox.Show("Blog updated successfully");
                        }
                        else
                        {
                            blogManager.AddBlog(tbName.Text, tbDescription.Text, (cbGames.SelectedItem as Game).name, (cbUsers.SelectedItem as User).username, cbCategory.SelectedItem.ToString());
                            MessageBox.Show("Blog added successfully");
                        }
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Please select all required fields.");
                    }
                }
                else
                {
                    MessageBox.Show("Please enter all required fields.");
                }
            }
            catch (DuplicateBlogNameException ex)
            {
                MessageBox.Show(ex.Message);
            }
            catch (SqlException)
            {
                MessageBox.Show("Try again later");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An unexpected error happened: {ex.Message}");
            }
        }
        private void FillComboBoxes()
        {
            cbGames.Items.Clear();
            cbGames.Items.AddRange(gameManager.GetGames().ToList().ToArray());

            cbUsers.Items.Clear();
            cbUsers.Items.AddRange(userManager.GetUsers().Where(u => u.isAdmin).ToList().ToArray());

            cbCategory.Items.Clear();
            cbCategory.Items.AddRange(blogManager.GetBlogCategories().ToArray());
            if (update)
            {
                cbCategory.Text = blog.Category;
                cbGames.Text = cbGames.Items.Cast<Game>().FirstOrDefault(g => g.name == blog.GameName).ToString();
                cbUsers.Text = cbUsers.Items.Cast<User>().FirstOrDefault(u => u.username == blog.UserUsername).ToString();
                tbDescription.Text = blog.Description;
                tbName.Text = blog.Name;
            }
        }
    }
}
