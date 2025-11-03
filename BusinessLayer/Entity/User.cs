using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLLClassLibrary.Entity
{
    public class User
    {
        public int userId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string passwordSalt { get; set; }
        public string passwordHash { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string level { get; set; }
        public bool isAdmin { get; set; }
        public int points { get; set; }
        public string imageUrl { get; set; }
        public User(int userId, string username, string email, string passwordSalt, string passwordHash, string level, bool isAdmin, DateTime dateOfBirth, int points, string imageUrl)
        {
            this.userId = userId;
            this.username = username;
            this.email = email;
            this.passwordSalt = passwordSalt;
            this.passwordHash = passwordHash;
            this.level = level;
            this.isAdmin = isAdmin;
            this.dateOfBirth = dateOfBirth;
            this.points = points;
            this.imageUrl = imageUrl;
        }
        public string Role()
        {
            if (this.isAdmin == true)
                return "Admin";
            return string.Empty;
        }
        public override string ToString()
        {
            return username;
        }
    }
}
