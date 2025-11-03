using BLLClassLibrary.Entity;
using BusinessLayer.Managers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Xml.Linq;
using System.Data.SqlClient;
using BLLClassLibrary.Exceptions;

namespace GamesDesktop
{
    public partial class ManageUser : Form
    {
        private readonly UserManager userManager;
        private readonly User user;
        private string imagePath;
        private readonly Cloudinary cloudinary;
        private string randomAvatarUrl;
        public ManageUser(UserManager userManager)
        {
            InitializeComponent();
            Account account = new Account("dwjem2zmu", "495892954549617", "rYEEgAYNX8YVzZ6z2Cw4hWs0SeY");
            cloudinary = new Cloudinary(account);
            this.userManager = userManager;
            this.user = null;
        }
        public ManageUser(UserManager userManager, User user)
        {
            InitializeComponent();
            Account account = new Account("dwjem2zmu", "495892954549617", "rYEEgAYNX8YVzZ6z2Cw4hWs0SeY");
            cloudinary = new Cloudinary(account);
            this.userManager = userManager;
            this.user = user;
            InsertDateAboutUser();
        }
        private async Task InsertDateAboutUser()
        {
            tbUsername.Text = user.username;
            tbEmail.Text = user.email;
            tbPassword.Text = "******";
            tbPassword.Enabled = false;
            dtpDateOfBirth.Value = user.dateOfBirth;
            if (user.isAdmin)
            {
                cbAdmin.Text = "Yes";
            }

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] imageBytes = await client.GetByteArrayAsync(user.imageUrl);
                    using (MemoryStream ms = new MemoryStream(imageBytes))
                    {
                        pbPicture.Image = Image.FromStream(ms);
                    }
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
            if (!string.IsNullOrEmpty(tbUsername.Text) && !string.IsNullOrEmpty(tbEmail.Text) && !string.IsNullOrEmpty(tbPassword.Text))
            {
                if (user == null)
                {
                    AddNewUser();
                }
                else
                {
                    UpdateExistingUser();
                }
            }
            else
            {
                MessageBox.Show("Enter all data");
            }
        }
        private void AddNewUser()
        {
            if (Regex.IsMatch(tbEmail.Text, @"@[\p{L}]+"))
                try
                {
                    string imageUrl = UploadImage();
                    if (string.IsNullOrEmpty(imageUrl) && string.IsNullOrEmpty(randomAvatarUrl))
                    {
                        MessageBox.Show("Please select or generate an avatar.");
                        return;
                    }
                    if (imageUrl == null) { imageUrl = randomAvatarUrl; }

                    userManager.AddUser(tbUsername.Text, tbEmail.Text, tbPassword.Text, dtpDateOfBirth.Value, cbAdmin.SelectedItem.ToString(), imageUrl);
                    this.Close();
                }
                catch (DuplicateUsernameException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (DuplicateEmailException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"During user adding mistake happened: {ex.Message}");
                }
            else
            {
                MessageBox.Show("Enter a valid email address");
            }
        }
        private void UpdateExistingUser()
        {
            if (Regex.IsMatch(tbEmail.Text, @"@[\p{L}]+"))
            {
                try
                {
                    string imageUrl = UploadImage();
                    bool isAdminBool = cbAdmin.Text == "Yes";
                    userManager.UpdateUser(user.userId, tbUsername.Text, tbEmail.Text, false, user.passwordSalt, user.passwordHash, dtpDateOfBirth.Value, isAdminBool, imageUrl);
                    this.Close();
                }
                catch (DuplicateUsernameException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (DuplicateEmailException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"During user updating mistake happened: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Enter a valid email address");
            }
        }
        private string UploadImage()
        {
            string imageUrl = null;
            if (File.Exists(imagePath))
            {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(imagePath),
                    PublicId = $"Users photos/{tbUsername.Text}"
                };

                var uploadResult = cloudinary.Upload(uploadParams);
                if (uploadResult == null)
                {
                    MessageBox.Show("Upload result is null.");
                    return null;
                }
                if (uploadResult.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string originalImageUrl = uploadResult.SecureUrl.ToString();
                    imageUrl = originalImageUrl.Replace("upload/", "upload/c_scale,w_300/");
                }
                else
                {
                    MessageBox.Show("Image upload failed!");
                    return null;
                }
            }
            return imageUrl;
        }
        private void btChangePicture_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                imagePath = openFileDialog.FileName;
                pbPicture.Image = Image.FromFile(imagePath);
                randomAvatarUrl = null;
            }
        }
        private string GenerateRandomAvatar(string username)
        {
            string[] sets = { "set1", "set2", "set3", "set4", "set5" };
            Random random = new Random();
            string set = sets[random.Next(sets.Length)];
            return $"https://robohash.org/{username}?set={set}";
        }
        private void btGenerateRandomAvatar_Click(object sender, EventArgs e)
        {
            string username = tbUsername.Text;
            if (!string.IsNullOrEmpty(username))
            {
                randomAvatarUrl = GenerateRandomAvatar(username);
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        byte[] imageBytes = client.GetByteArrayAsync(randomAvatarUrl).Result;
                        using (MemoryStream ms = new MemoryStream(imageBytes))
                        {
                            pbPicture.Image = Image.FromStream(ms);
                        }
                    }
                    catch (Exception ex)
                    {
                        pbPicture.Image = null;
                        MessageBox.Show($"Failed to load image: {ex.Message}");
                    }
                }
                imagePath = null;
            }
            else
            {
                MessageBox.Show("Please enter a username to generate an avatar.");
            }
        }
    }
}
