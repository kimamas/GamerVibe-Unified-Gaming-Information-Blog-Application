using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLClassLibrary.Entity
{
    public class BoardGame : Game
    {
        public List<string> types { get; set; }
        public BoardGame(int id, string name, string description, string developer, DateTime releaseDate, double rating, int numberOfVisitors, List<string> types, string gameType, string imageUrl) : base(id, name, description, developer, releaseDate, rating, numberOfVisitors, gameType, imageUrl)
        {
            this.types = types;
        }
        public override object GetDataGame(int number)
        {
            switch (number)
            {
                case 0:
                    return name;
                case 1:
                    return description;
                case 2:
                    return developer;
                case 3:
                    return releaseDate;
                case 4:
                    return types;
                case 5:
                    return null;
                default:
                    return null;
            }
        }
    }
}
