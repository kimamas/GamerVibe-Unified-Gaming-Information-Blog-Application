using BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GamesDesktop
{
    public partial class Filtering : Form
    {
        private readonly GameManager gameManager;
        public int selectedFiltering { get; private set; }
        public string name { get; private set; } = null;
        public string developer { get; private set; } = null;
        public DateTime date { get; private set; } = DateTime.Parse("01.01.2000");
        public List<string> genres { get; private set; } = null;
        public List<string> platforms { get; private set; } = null;
        public List<string> types { get; private set; } = null;
        public Filtering(GameManager gameManager)
        {
            InitializeComponent();
            this.gameManager = gameManager;
            dtpDate.CustomFormat = "yyyy";
            clbGenres.Items.AddRange(gameManager.AddGenres().ToArray());
            clbPlatforms.Items.AddRange(gameManager.AddPlatforms().ToArray());
            clbBoardTypes.Items.AddRange(gameManager.AddTypes().ToArray());
            dtpDate.Enabled = false;
            genres = new List<string>();
            platforms = new List<string>();
            types = new List<string>();
        }

        private void btConfirm_Click(object sender, EventArgs e)
        {
            if (cbFiltering.SelectedIndex > -1)
            {
                if (!string.IsNullOrEmpty(tbName.Text)) { name = tbName.Text; }
                if (!string.IsNullOrEmpty(tbDeveloper.Text)) { developer = tbDeveloper.Text; }
                if (dtpDate.Enabled) { date = dtpDate.Value; }

                if (cbFiltering.Text == "Most Relevant")
                {
                    selectedFiltering = 0;
                }
                else if (cbFiltering.Text == "Name ascending")
                {
                    selectedFiltering = 1;
                }
                else if (cbFiltering.Text == "Name descending")
                {
                    selectedFiltering = 2;
                }
                else if (cbFiltering.Text == "Top Rated")
                {
                    selectedFiltering = 3;
                }
                else if (cbFiltering.Text == "Most Viewed")
                {
                    selectedFiltering = 4;
                }
                else if (cbFiltering.Text == "Video Games")
                {
                    selectedFiltering = 5;
                }
                else if (cbFiltering.Text == "Board Games")
                {
                    selectedFiltering = 6;
                }

                foreach (string genre in clbGenres.Items)
                {
                    if (clbGenres.CheckedItems.Contains(genre))
                    {
                        genres.Add(genre);
                    }
                }
                foreach (string platform in clbPlatforms.Items)
                {
                    if (clbPlatforms.CheckedItems.Contains(platform))
                    {
                        platforms.Add(platform);
                    }
                }
                foreach (string type in clbBoardTypes.Items)
                {
                    if (clbBoardTypes.CheckedItems.Contains(type))
                    {
                        types.Add(type);
                    }
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Select filtering");
            }

        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbDate.Text == "Yes")
            {
                dtpDate.Enabled = true;
            }
            else
            {
                dtpDate.Enabled = false;
            }
        }
    }
}
