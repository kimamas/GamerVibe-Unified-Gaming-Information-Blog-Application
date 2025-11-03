using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLClassLibrary.Entity
{
    public class Blog
    {
        private int blogId;
        private string name;
        private string description;
        private string gameName;
        private string userUsername;
        private string category;
        private DateTime created;
        public int BlogId { get => blogId; set => blogId = value; }
        public string GameName { get => gameName; set => gameName = value; }
        public string UserUsername { get => userUsername; set => userUsername = value; }
        public string Category { get => category; set => category = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public DateTime Created { get => created; set => created = value; }

        public Blog(int blogId, string gameName, string userUsername, string category, string name, string description, DateTime created)
        {
            BlogId = blogId;
            GameName = gameName;
            UserUsername = userUsername;
            Category = category;
            Name = name;
            Description = description;
            Created = created;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
