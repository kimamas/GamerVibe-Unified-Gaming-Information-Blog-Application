using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLClassLibrary.Entity;
using System.Security.Cryptography;
using System.Data;
using BLLClassLibrary.Exceptions;
namespace BusinessLayer.Managers
{
    public class UserManager
    {
        private readonly IUser userRepository;
        public UserManager(IUser userRepository)
        {
            this.userRepository = userRepository;
        }
        public void AddUser(string username, string email, string password, DateTime dateOfBirth, string isAdmin, string imageUrl)
        {
            if (userRepository.GetUsersUsernames().Contains(username))
            {
                throw new DuplicateUsernameException();
            }

            if (userRepository.GetUsersEmails().Contains(email))
            {
                throw new DuplicateEmailException();
            }

            string salt = GenerateSalt();
            string hashedPassword = HashPassword(password, salt);

            bool isAdminBool = false;
            if (isAdmin == "Yes") { isAdminBool = true; }
            userRepository.AddUser(username, email, salt, hashedPassword, dateOfBirth, isAdminBool, imageUrl);
        }
        public void UpdateUserPassword(string email, string password)
        {
            string salt = GenerateSalt();
            string hashedPassword = HashPassword(password, salt);
            userRepository.UpdateUserPassword(email, salt, hashedPassword);
        }
        public void UpdateUser(int userId, string username, string email, bool passwordUpdate, string passwordSalt, string passwordOrHash, DateTime dateOfBirth, bool isAdmin, string imageUrl)
        {
            List<string> usernames = userRepository.GetUsersUsernames();
            usernames.Remove(userRepository.GetUserById(userId).username);
            if (usernames.Contains(username))
            {
                throw new DuplicateUsernameException();
            }

            List<string> emails = userRepository.GetUsersEmails();
            emails.Remove(userRepository.GetUserById(userId).email);
            if (emails.Contains(email))
            {
                throw new DuplicateEmailException();
            }

            if (passwordUpdate)
            {
                passwordOrHash = HashPassword(passwordOrHash, passwordSalt);
            }
            userRepository.UpdateUser(userId, username, email, passwordSalt, passwordOrHash, dateOfBirth, isAdmin, imageUrl);

        }
        public bool VerifyPassword(string enteredPassword, string storedSalt, string storedHash)
        {
            string hashedEnteredPassword = HashPassword(enteredPassword, storedSalt);
            return hashedEnteredPassword == storedHash;
        }
        private string GenerateSalt(int size = 16)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var saltBytes = new byte[size];
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }
        private string HashPassword(string password, string salt)
        {
            string combined = salt + password;
            using (var sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        public List<string> GetUsersUsernames() => userRepository.GetUsersUsernames();
        public List<string> GetUsersEmails() => userRepository.GetUsersEmails();
        public User FindUserByUsername(string username) => userRepository.FindUserByUsername(username);
        public void AddPointsToTheUser(int points, string username) => userRepository.AddPointsToTheUser(points, username);
        public List<(string Name, int Points)> GetLevels() => userRepository.GetLevels();
        public List<User> GetUsers() => userRepository.GetUsers();
        public User GetUserById(int userId) => userRepository.GetUserById(userId);
        public void UpdateUserAvatar(string imageUrl, int userId) => userRepository.UpdateUserAvatar(imageUrl, userId);
        //public void DeleteUser(int userId, string username) => userRepository.DeleteUser(userId, username);
    }
}
