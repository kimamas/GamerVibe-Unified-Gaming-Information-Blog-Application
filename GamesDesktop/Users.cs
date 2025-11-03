using BusinessLayer.Managers;
using DALClassLibrary;
using BLLClassLibrary.Entity;
using System.Data.SqlClient;
namespace GamesDesktop
{
    public partial class Users : Form
    {
        private readonly Main main;
        private readonly UserManager userManager;
        private bool closeApp = true;
        public Users(Main main)
        {
            InitializeComponent();
            this.main = main;
            this.userManager = new UserManager(new UserRepository());
            RefreshUsers();
        }

        private void RefreshUsers()
        {
            lbxUsers.Items.Clear();
            lbxUsers.Items.AddRange(userManager.GetUsers().ToArray());
        }

        private void btAddUser_Click(object sender, EventArgs e)
        {
            ManageUser addUser = new ManageUser(userManager);
            addUser.ShowDialog();
            RefreshUsers();
        }

        private void YourForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (closeApp) { Application.Exit(); }
        }

        private void btEditUser_Click(object sender, EventArgs e)
        {
            if (lbxUsers.SelectedIndex > -1)
            {
                try
                {
                    User selectedUser = (User)lbxUsers.SelectedItem;
                    if (selectedUser.isAdmin)
                    {
                        ManageUser updateUser = new ManageUser(userManager, (User)lbxUsers.SelectedItem);
                        updateUser.ShowDialog();
                        RefreshUsers();
                        MessageBox.Show("User updated successfully.");
                    }
                    else
                    {
                        MessageBox.Show("You can edit only admins");
                    }
                }
                catch (SqlException)
                {
                    MessageBox.Show("Please try again later.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Select user");
            }
        }

        private void pcHome_Click(object sender, EventArgs e)
        {
            main.Show();
            closeApp = false;
            this.Close();
        }

        //private void btDeleteUser_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if(userManager.GetUsers().Where(u=>u.isAdmin).Count() < 2)
        //        {
        //            throw new Exception("You can't delete all admins");
        //        }
        //        if (((User)lbxUsers.SelectedItem).isAdmin)
        //        {
        //            userManager.DeleteUser(((User)lbxUsers.SelectedItem).userId, ((User)lbxUsers.SelectedItem).username);
        //            RefreshUsers();
        //            throw new Exception("User was successfully deleted");
        //        }
        //        throw new Exception("You only can delete admins");
        //    }
        //    catch (NullReferenceException)
        //    {
        //        MessageBox.Show("Select User");
        //    }
        //    catch (SqlException)
        //    {
        //        MessageBox.Show("Try again after some time");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
    }
}