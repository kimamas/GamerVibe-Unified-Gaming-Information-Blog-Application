using BLLClassLibrary.Entity;
using BusinessLayer.Managers;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BLLClassLibrary.Exceptions;
using System.Data.SqlClient;

namespace GamesDesktop
{
    public partial class EditGame : Form
    {
        private readonly Game game;
        private readonly GameManager gameManager;
        private string imagePath;
        private readonly Cloudinary cloudinary;
        private bool imageExist = false;
        public EditGame(GameManager gameManager, Game game)
        {
            InitializeComponent();
            this.game = game;
            this.gameManager = gameManager;
            Account account = new Account("dwjem2zmu", "495892954549617", "rYEEgAYNX8YVzZ6z2Cw4hWs0SeY");
            cloudinary = new Cloudinary(account);

            clbGenres.Items.AddRange(gameManager.AddGenres().ToArray());
            clbPlatforms.Items.AddRange(gameManager.AddPlatforms().ToArray());
            clbBoardTypes.Items.AddRange(gameManager.AddTypes().ToArray());
            InsertAllDateAboutGame();

            ManageLists(game.GetTypeOfTheGame());
        }

        private void ManageLists(string type)
        {
            if (type == "video")
            {
                clbGenres.Enabled = true;
                clbPlatforms.Enabled = true;
                clbBoardTypes.Enabled = false;
            }
            else
            {
                clbGenres.Enabled = false;
                clbPlatforms.Enabled = false;
                clbBoardTypes.Enabled = true;
            }
        }

        private async Task InsertAllDateAboutGame()
        {
            tbName.Text = (string)game.GetDataGame(0);
            tbDeveloper.Text = (string)game.GetDataGame(2);
            rtbDescription.Text = (string)game.GetDataGame(1);
            dtpDate.Value = (DateTime)game.GetDataGame(3);
            for (int i = 0; i < clbGenres.Items.Count; i++)
            {
                List<string> genres = game.GetDataGame(4) as List<string>;

                if (genres != null && genres.Contains(clbGenres.Items[i]))
                {
                    clbGenres.SetItemChecked(i, true);
                }
            }
            for (int i = 0; i < clbPlatforms.Items.Count; i++)
            {
                List<string> genres = game.GetDataGame(5) as List<string>;

                if (genres != null && genres.Contains(clbPlatforms.Items[i]))
                {
                    clbPlatforms.SetItemChecked(i, true);
                }
            }
            for (int i = 0; i < clbBoardTypes.Items.Count; i++)
            {
                List<string> types = game.GetDataGame(4) as List<string>;

                if (types != null && types.Contains(clbBoardTypes.Items[i]))
                {
                    clbBoardTypes.SetItemChecked(i, true);
                }
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] imageBytes = await client.GetByteArrayAsync(game.imageUrl);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        pbPicture.Image = Image.FromStream(ms);
                    }
                    imageExist = true;
                }
                catch (Exception ex)
                {
                    pbPicture.Image = null;
                    MessageBox.Show($"Failed to load image: {ex.Message}");
                }
            }
        }

        private void btSubmit_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbDeveloper.Text) && !string.IsNullOrEmpty(tbName.Text) && !string.IsNullOrEmpty(rtbDescription.Text))
            {
                try
                {
                    if (imageExist)
                    {
                        string imageUrl = null;
                        if (File.Exists(imagePath))
                        {
                            var uploadParams = new ImageUploadParams()
                            {
                                File = new FileDescription(imagePath),
                                PublicId = $"Games photos/{tbName.Text}"
                            };

                            var uploadResult = cloudinary.Upload(uploadParams);
                            if (uploadResult == null)
                            {
                                MessageBox.Show("Upload result is null.");
                                return;
                            }
                            if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                            {
                                string originalImageUrl = uploadResult.SecureUrl.ToString();
                                imageUrl = originalImageUrl.Replace("upload/", "upload/c_scale,w_300/");
                            }
                            else
                            {
                                MessageBox.Show("Image upload failed!");
                                return;
                            }
                        }

                        List<string> selectedGenres = new List<string>();
                        List<string> selectedPlatforms = new List<string>();
                        List<string> selectedTypes = new List<string>();

                        foreach (string genre in clbGenres.Items)
                        {
                            if (clbGenres.CheckedItems.Contains(genre))
                            {
                                selectedGenres.Add(genre);
                            }
                        }
                        foreach (string platform in clbPlatforms.Items)
                        {
                            if (clbPlatforms.CheckedItems.Contains(platform))
                            {
                                selectedPlatforms.Add(platform);
                            }
                        }
                        foreach (string type in clbBoardTypes.Items)
                        {
                            if (clbBoardTypes.CheckedItems.Contains(type))
                            {
                                selectedTypes.Add(type);
                            }
                        }

                        gameManager.UpdateGameInformation(game.name, (int)game.id, tbName.Text, rtbDescription.Text, tbDeveloper.Text, dtpDate.Value, selectedTypes, selectedGenres, selectedPlatforms, game.GetTypeOfTheGame(), imageUrl);
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("You must select an image");
                    }

                }
                catch (DuplicateGameNameException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (SqlException)
                {
                    MessageBox.Show("Try again later");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error happened: {ex}");
                }
            }
            else
            {
                MessageBox.Show("Enter all information");
            }
        }
        private void btChangePicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = openFileDialog.FileName;
                    pbPicture.Image = Image.FromFile(imagePath);
                }
            }
        }
    }
}
