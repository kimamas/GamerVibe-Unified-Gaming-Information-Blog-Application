using BLLClassLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTestProject
{
    public class FakeUserRepository : IUser
    {
        public bool AddUserCalled { get; private set; }
        private List<User> users;

        public FakeUserRepository()
        {
            AddUserCalled = false;
            users = new List<User>
            {
                new User(1, "testuser", "testuser@example.com", "ThoiPjL7KdunQG1ZB/zxTQ==","89cc14719da4e81490dc999c771aa73e36cd76a50e6761f1c6ebb7e4c7798466" , "user", false, new DateTime(1990, 1, 1), 0, "https://robohash.org/testuser?set=4")
            };
        }


        public void AddUser(string username, string email, string passwordSalt, string passwordHash, DateTime dateOfBirth, bool isAdmin, string imageUrl)
        {
            User user = new User(users.Count + 1, username, email, passwordSalt, passwordHash, "user", isAdmin, dateOfBirth, 0, $"https://robohash.org/{username}?set=4");
            users.Add(user);
            AddUserCalled = true;
        }

        public List<string> GetUsersEmails()
        {
            return users.Select(u => u.email).ToList();
        }

        public List<string> GetUsersUsernames()
        {
            return users.Select(u => u.username).ToList();
        }

        public User FindUserByUsername(string username)
        {
            return users.FirstOrDefault(u => u.username == username);
        }

        public void UpdateUser(int userId, string username, string email, string passwordSalt, string passwordHash, DateTime dateOfBirth, bool isAdmin, string imageUrl)
        {
            var user = users.Find(u => u.userId == userId);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            user.username = username;
            user.email = email;
            user.passwordSalt = passwordSalt;
            user.passwordHash = passwordHash;
            user.dateOfBirth = dateOfBirth;
            user.isAdmin = isAdmin;
            user.imageUrl = imageUrl;
        }

        public void UpdateUserPassword(string email, string passwordSalt, string passwordHash)
        {
            var user = users.Find(u => u.email == email);
            if (user == null)
            {
                throw new Exception("Email not found");
            }
            user.passwordSalt = passwordSalt;
            user.passwordHash = passwordHash;
        }

        public void AddPointsToTheUser(int points, string username)
        {
            var user = users.Find(u => u.username == username);
            if (user != null)
            {
                user.points += points;
            }
        }

        public List<(string Name, int Points)> GetLevels()
        {
            return users.Select(u => (u.username, u.points)).ToList();
        }

        public List<User> GetUsers()
        {
            return users;
        }

        public User GetUserById(int userId)
        {
            return users.Find(u => u.userId == userId);
        }

        public void UpdateUserAvatar(string imageUrl, int userId)
        {
            var user = users.Find(u => u.userId == userId);
            if (user != null)
            {
                user.imageUrl = imageUrl;
            }
        }

        public void DeleteUser(int userId)
        {
            throw new NotImplementedException();
        }
    }
}
