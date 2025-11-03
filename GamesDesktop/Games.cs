using BLLClassLibrary.Entity;
using BLLClassLibrary.Exceptions;
using BusinessLayer.Managers;
using DALClassLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace GamesDesktop
{
    public partial class Games : Form
    {
        private readonly Main main;
        private readonly GameManager gameManager;
        private bool closeApp = true;
        public Games(Main main)
        {
            InitializeComponent();
            this.main = main;
            this.gameManager = new GameManager(new GameRepository());
            RefreshGameList();
        }

        private void btAddGame_Click(object sender, EventArgs e)
        {
            AddGame addGame = new AddGame(gameManager);
            addGame.ShowDialog();
            RefreshGameList();
        }
        private void RefreshGameList()
        {
            lbxGames.Items.Clear();
            lbxGames.Items.AddRange(gameManager.GetGames().ToArray());
        }
        private void btEditGame_Click(object sender, EventArgs e)
        {
            if (lbxGames.SelectedIndex > -1)
            {
                try
                {
                    EditGame editGame = new EditGame(gameManager,(Game)lbxGames.SelectedItem);
                    editGame.ShowDialog();
                    RefreshGameList();
                }
                catch (NullReferenceException)
                {
                    MessageBox.Show("Game wasn't found");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Select Game");
            }
        }
        private void btFiltering_Click(object sender, EventArgs e)
        {
            Filtering filtering = new Filtering(gameManager);
            filtering.ShowDialog();
            lbxGames.Items.Clear();
            lbxGames.Items.AddRange(gameManager.GameFiltering(filtering.selectedFiltering, filtering.name, filtering.developer, (DateTime)filtering.date, filtering.genres, filtering.platforms, filtering.types).ToArray());
        }
        private void YourForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeApp)
            {
                Application.Exit();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            main.Show();
            closeApp = false;
            this.Close();
        }

        private void lbxGames_DoubleClick(object sender, EventArgs e)
        {
            Game game = (Game)lbxGames.SelectedItem;
            StringBuilder gameInfo = new StringBuilder();
            gameInfo.AppendLine($"Developer: {game.developer}\n\n");
            gameInfo.AppendLine($"Date of Release: {game.releaseDate:dd MM yyyy}\n\n");
            gameInfo.AppendLine($"Description: {game.description}\n\n");

            if (game.gameType == "video")
            {
                gameInfo.Append("Genres:");
                foreach (string genre in game.GetDataGame(4) as List<string>)
                {
                    gameInfo.Append($" {genre},");
                }
                gameInfo.Length -= 1;
                gameInfo.Append("\n\n");

                gameInfo.Append("Platfroms:");
                foreach (string platform in game.GetDataGame(5) as List<string>)
                {
                    gameInfo.Append($" {platform},");
                }
                gameInfo.Length -= 1;
            }
            else
            {
                gameInfo.Append("Types:");
                foreach (string type in game.GetDataGame(4) as List<string>)
                {
                    gameInfo.Append($" {type},");
                }
                gameInfo.Length -= 1;
            }

            MessageBox.Show(gameInfo.ToString());
        }

        private void btDeleteGame_Click(object sender, EventArgs e)
        {
            try
            {
                gameManager.DeleteGame(((Game)lbxGames.SelectedItem).id);
                RefreshGameList();
                MessageBox.Show("Game was successfully deleted");
            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Select the Game");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error happened: {ex.Message}");
            }
        }
    }
}
