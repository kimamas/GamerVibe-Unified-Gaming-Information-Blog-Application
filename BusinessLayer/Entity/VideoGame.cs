using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BLLClassLibrary.Entity
{
    public class VideoGame : Game
    {
        public List<string> genres { get; set; }
        public List<string> platforms { get; set; }

        public VideoGame(int id, string name, string description, string developer, DateTime releaseDate, double rating, int numberOfVisitors, string gameType, List<string> platforms, List<string> genres, string imageUrl) : base(id, name, description, developer, releaseDate, rating, numberOfVisitors, gameType, imageUrl)
        {
            this.genres = genres;
            this.platforms = platforms;
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
                    return genres;
                case 5:
                    return platforms;
                default:
                    return null;
            }
        }
    }
}
