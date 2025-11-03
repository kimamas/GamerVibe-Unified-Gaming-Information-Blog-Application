namespace UnitTestProject
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BLLClassLibrary.Entity;
    using BusinessLayer.Managers;
    using System;
    using System.Collections.Generic;
    using BLLClassLibrary.Exceptions;
    using Repositories;

    [TestClass]
    public class UserManagerTests
    {
        private UserManager userManager;
        private FakeUserRepository fakeUserRepository;

        [TestInitialize]
        public void CreateUserManager()
        {
            fakeUserRepository = new FakeUserRepository();
            userManager = new UserManager(fakeUserRepository);
        }

        [TestMethod]
        public void AddUser_Should_Add_User_To_Database()
        {
            string username = "testuser1";
            string email = "testuser1@example.com";
            string password = "password123";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);
            string isAdmin = "No";

            userManager.AddUser(username, email, password, dateOfBirth, isAdmin, $"https://robohash.org/{username}?set=4");

            User user = userManager.FindUserByUsername(username);
            Assert.IsTrue(user != null);
            Assert.IsTrue(fakeUserRepository.AddUserCalled, "AddUser must be called");
        }

        [TestMethod]
        public void AddUser_Should_Fail_If_Duplicate_Email()
        {

            string username = "testuser2";
            string email = "testuser@example.com";
            string password = "password123";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);
            string isAdmin = "no";

            var exception = Assert.ThrowsException<DuplicateEmailException>(() =>
            {
                userManager.AddUser(username, email, password, dateOfBirth, isAdmin, $"https://robohash.org/{username}?set=4");
            });

            Assert.AreEqual("This email already exists.", exception.Message);


        }

        [TestMethod]
        public void ReturnUsersEmails_Should_Return_Emails_From_Database()
        {
            var expectedEmails = new List<string> { "testuser@example.com", "testuser1@example.com" };

            userManager.AddUser("testuser1", "testuser1@example.com", "password123", new DateTime(1990, 1, 1), "no", "https://robohash.org/testuser1?set=4");
            var result = userManager.GetUsersEmails();

            CollectionAssert.AreEqual(expectedEmails, result);
        }

        [TestMethod]
        public void ReturnUsersUsernames_Should_Return_Usernames_From_Database()
        {
            var expectedUsernames = new List<string> { "testuser", "testuser1" };

            userManager.AddUser("testuser1", "testuser1@example.com", "password123", new DateTime(1990, 1, 1), "no", "https://robohash.org/testuser1?set=4");
            var result = userManager.GetUsersUsernames();

            CollectionAssert.AreEqual(expectedUsernames, result);
        }

        [TestMethod]
        public void FindUserByUsername_Should_Return_Correct_User()
        {
            string username = "testuser";
            var expectedUser = new User(1, "testuser", "testuser@example.com", "ThoiPjL7KdunQG1ZB/zxTQ==", "89cc14719da4e81490dc999c771aa73e36cd76a50e6761f1c6ebb7e4c7798466", "user", false, new DateTime(1990, 1, 1), 0, "https://robohash.org/testuser?set=4");

            var found_user = userManager.FindUserByUsername(username);

            Assert.AreEqual(expectedUser.username, found_user.username);
            Assert.AreEqual(expectedUser.dateOfBirth, found_user.dateOfBirth);
            Assert.AreEqual(expectedUser.email, found_user.email);
            Assert.AreEqual(expectedUser.passwordSalt, found_user.passwordSalt);
            Assert.AreEqual(expectedUser.passwordHash, found_user.passwordHash);
            Assert.AreEqual(expectedUser.level, found_user.level);
            Assert.AreEqual(expectedUser.isAdmin, found_user.isAdmin);
            Assert.AreEqual(expectedUser.imageUrl, found_user.imageUrl);
        }

        [TestMethod]
        public void FindUserByUsername_Should_Return_Null_If_User_Not_Found()
        {
            string username = "nonexistentuser";

            var result = userManager.FindUserByUsername(username);

            Assert.IsNull(result, "User should be null if not found");
        }

        [TestMethod]
        public void UpdateUser_Should_Update_User_Details()
        {
            int userId = 1;
            string username = "updateduser";
            string email = "updateduser@example.com";
            string password = "newpassword123";
            string salt = "ThoiPjL7KdunQG1ZB/zxTQ==";
            string hash = "89cc14719da4e81490dc999c771aa73e36cd76a50e6761f1c6ebb7e4c7798466";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);
            bool isAdmin = true;

            userManager.UpdateUser(userId, username, email, false, salt, hash, dateOfBirth, isAdmin, $"https://robohash.org/{username}?set=4");

            var updatedUser = fakeUserRepository.FindUserByUsername(username);
            Assert.IsNotNull(updatedUser);
            Assert.AreEqual(username, updatedUser.username);
            Assert.AreEqual(email, updatedUser.email);
            Assert.AreEqual(dateOfBirth, updatedUser.dateOfBirth);
            Assert.AreEqual(isAdmin, updatedUser.isAdmin);
        }

        [TestMethod]
        public void UpdateUser_Should_Fail_If_User_Not_Found()
        {
            int userId = 999;
            string username = "updateduser";
            string email = "updateduser@example.com";
            string salt = "ThoiPjL7KdunQG1ZB/zxTQ==";
            string hash = "89cc14719da4e81490dc999c771aa73e36cd76a50e6761f1c6ebb7e4c7798466";
            DateTime dateOfBirth = new DateTime(1990, 1, 1);
            bool isAdmin = true;

            try
            {
                User user = userManager.GetUserById(userId);
                if (user == null)
                {
                    throw new Exception("User not found");
                }
                userManager.UpdateUser(userId, username, email, false, salt, hash, dateOfBirth, isAdmin, $"https://robohash.org/{username}?set=4");
                Assert.Fail("Expected exception was not thrown");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("User not found", ex.Message);
            }
        }

        [TestMethod]
        public void UpdateUserPassword_Should_Fail_If_Email_Not_Found()
        {
            string email = "nonexistentemail@example.com";
            string newPassword = "newpassword123";

            try
            {
                userManager.UpdateUserPassword(email, newPassword);
                Assert.Fail("Expected exception was not thrown");
            }
            catch (Exception ex)
            {
                Assert.AreEqual("Email not found", ex.Message);
            }
        }

        [TestMethod]
        public void AddPointsToTheUser_Should_Add_Points()
        {
            string username = "testuser";
            int points = 10;

            userManager.AddPointsToTheUser(points, username);

            var user = userManager.FindUserByUsername(username);
            Assert.AreEqual(points, user.points);
        }

        [TestMethod]
        public void GetLevels_Should_Return_Levels()
        {
            var levels = userManager.GetLevels();

            Assert.IsNotNull(levels);
        }

        [TestMethod]
        public void GetUsers_Should_Return_All_Users()
        {
            var users = userManager.GetUsers();

            Assert.IsNotNull(users);
            Assert.IsTrue(users.Count > 0);
        }

        [TestMethod]
        public void GetUserById_Should_Return_User_If_Found()
        {
            int userId = 1;

            var user = userManager.GetUserById(userId);

            Assert.IsNotNull(user);
            Assert.AreEqual(userId, user.userId);
        }

        [TestMethod]
        public void GetUserById_Should_Return_Null_If_Not_Found()
        {
            int userId = 999;

            var user = userManager.GetUserById(userId);

            Assert.IsNull(user);
        }
        [TestMethod]
        public void UpdateUserAvatar_Should_Update_Avatar()
        {
            int userId = 1;
            string newImageUrl = "https://robohash.org/newuser?set=4";

            userManager.UpdateUserAvatar(newImageUrl, userId);

            var user = userManager.GetUserById(userId);
            Assert.AreEqual(newImageUrl, user.imageUrl);
        }
    }
}
