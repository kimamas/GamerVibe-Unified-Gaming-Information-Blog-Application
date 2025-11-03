using BusinessLayer.Managers;
using System.Data;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using BLLClassLibrary.Exceptions;

namespace GamesDesktop
{
    public partial class AddGame : Form
    {
        private readonly GameManager gameManager;
        private readonly Cloudinary cloudinary;
        private string selectedImagePath;
        public AddGame(GameManager gameManager)
        {
            InitializeComponent();
            this.gameManager = gameManager;
            Account account = new Account("dwjem2zmu", "495892954549617", "rYEEgAYNX8YVzZ6z2Cw4hWs0SeY");
            cloudinary = new Cloudinary(account);

            clbGenres.Items.AddRange(gameManager.AddGenres().ToArray());
            clbPlatforms.Items.AddRange(gameManager.AddPlatforms().ToArray());
            clbBoardTypes.Items.AddRange(gameManager.AddTypes().ToArray());
            clbGenres.Enabled = false;
            clbPlatforms.Enabled = false;
            clbBoardTypes.Enabled = false;

        }

        private void btSubmit_Click(object sender, EventArgs e)
        {
            if (rbBoardGame.Checked || rbVideoGame.Checked)
            {
                if (!string.IsNullOrEmpty(tbDeveloper.Text) && !string.IsNullOrEmpty(tbName.Text) && !string.IsNullOrEmpty(rtbDescription.Text))
                {
                    try
                    {
                            if (!string.IsNullOrEmpty(selectedImagePath))
                            {
                                string imageUrl = null;

                                var uploadParams = new ImageUploadParams()
                                {
                                    File = new FileDescription(selectedImagePath),
                                    PublicId = $"Games photos/{tbName.Text}"
                                };

                                var uploadResult = cloudinary.Upload(uploadParams);

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

                                if (rbVideoGame.Checked)
                                {
                                    List<string> selectedGenres = new List<string>();
                                    List<string> selectedPlatforms = new List<string>();
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
                                    gameManager.AddGame("video", tbName.Text, rtbDescription.Text, tbDeveloper.Text, dtpDate.Value, selectedGenres, selectedPlatforms, null, imageUrl);
                                    this.Close();
                                }
                                else
                                {
                                    List<string> selectedTypes = new List<string>();
                                    foreach (string type in clbBoardTypes.Items)
                                    {
                                        if (clbBoardTypes.CheckedItems.Contains(type))
                                        {
                                            selectedTypes.Add(type);
                                        }
                                    }
                                    gameManager.AddGame("board", tbName.Text, rtbDescription.Text, tbDeveloper.Text, dtpDate.Value, null, null, selectedTypes, imageUrl);
                                    this.Close();
                                }
                            }
                            else
                            {
                                MessageBox.Show("Select an image");
                            }
                        
                    }
                    catch (DuplicateGameNameException ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred: {ex.Message}");
                    }
                }
                else
                {
                    MessageBox.Show("Enter all information");
                }
            }
            else
            {
                MessageBox.Show("Choose Game type");
            }
        }

        private void rbVideoGame_CheckedChanged(object sender, EventArgs e)
        {
            clbGenres.Enabled = true;
            clbPlatforms.Enabled = true;
            clbBoardTypes.Enabled = false;
        }

        private void rbBoardGame_CheckedChanged(object sender, EventArgs e)
        {
            clbGenres.Enabled = false;
            clbPlatforms.Enabled = false;
            clbBoardTypes.Enabled = true;
        }

        private void btUploadPicture_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    selectedImagePath = openFileDialog.FileName;
                    pbPicture.Image = Image.FromFile(selectedImagePath);
                }
            }
        }
    }
}
