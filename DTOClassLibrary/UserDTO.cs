using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOClassLibrary
{
    public class UserDTO
    {
        public UserDTO(int userId, string username, string email, string password, DateTime dateOfBirth, string level, bool isAdmin)
        {
            this.userId = userId;
            this.username = username;
            this.email = email;
            this.password = password;
            this.dateOfBirth = dateOfBirth;
            this.level = level;
            this.isAdmin = isAdmin;
        }

        public int userId { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public DateTime dateOfBirth { get; set; }
        public string level { get; set; }
        public bool isAdmin { get; set; }
    }
}
