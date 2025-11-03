using BusinessLayer;
using DesktopApp;

namespace GamesDesktop
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private void btGames_Click(object sender, EventArgs e)
        {
            Games games = new Games(this);
            this.Hide();
            games.Show();
        }

        private void btUsers_Click(object sender, EventArgs e)
        {
            Users users = new Users(this);
            this.Hide();
            users.Show();
        }

        private void btBlogs_Click(object sender, EventArgs e)
        {
            Blogs blogs = new Blogs(this);
            this.Hide();
            blogs.Show();
        }
    }
}
