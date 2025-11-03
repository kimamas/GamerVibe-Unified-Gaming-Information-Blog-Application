using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOClassLibrary
{
    public class GameDTO
    {
        public GameDTO(int id, string name, string description, string developer, DateTime releaseDate, double rating, int numberOfVisitors, string gameType, List<string>? types, List<string>? videoGamesGenres, List<string>? platforms)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.developer = developer;
            this.releaseDate = releaseDate;
            this.rating = rating;
            this.numberOfVisitors = numberOfVisitors;
            this.gameType = gameType;
            this.types = types;
            this.videoGamesGenres = videoGamesGenres;
            this.platforms = platforms;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string developer { get; set; }
        public DateTime releaseDate { get; set; }
        public double rating { get; set; }
        public int numberOfVisitors { get; set; }
        public string gameType { get; set; }
        public List<string>? types { get; set; }
        public List<string>? videoGamesGenres { get; set; }
        public List<string>? platforms { get; set; }
    }
}
