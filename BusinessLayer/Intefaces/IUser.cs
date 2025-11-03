using BLLClassLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IUser
{
    public void AddUser(string username, string email,string passwordSalt, string passwordHash, DateTime dateOfBirth, bool isAdmin, string imageUrl);
    public List<string> GetUsersEmails();
    public List<string> GetUsersUsernames();
    public User FindUserByUsername(string username);
    public void UpdateUser(int userId, string username, string email, string passwordSalt, string passwordHash, DateTime dateOfBirth, bool isAdmin, string imageUrl);
    public void UpdateUserPassword(string email, string passwordSalt, string passwordHash);
    public void AddPointsToTheUser(int points, string username);
    public List<(string Name, int Points)> GetLevels();
    public List<User> GetUsers();
    public User GetUserById(int userId);
    public void UpdateUserAvatar(string imageUrl, int userId);
    //public void DeleteUser(int userId, string username);
}
