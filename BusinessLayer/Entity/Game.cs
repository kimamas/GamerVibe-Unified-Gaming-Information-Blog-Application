namespace BLLClassLibrary.Entity
{
    public abstract class Game
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string developer { get; set; }
        public DateTime releaseDate { get; set; }
        public double rating { get; set; }
        public int numberOfVisitors { get; set; }
        public string gameType { get; set; }
        public string imageUrl { get; set; }

        public Game(int id, string name, string description, string developer, DateTime releaseDate, double rating, int numberOfVisitors, string gameType, string imageUrl)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.developer = developer;
            this.releaseDate = releaseDate;
            this.rating = rating;
            this.numberOfVisitors = numberOfVisitors;
            this.gameType = gameType;
            this.imageUrl = imageUrl;
        }
        public abstract object GetDataGame(int number);

        public string GetTypeOfTheGame()
        {
            return gameType;
        }
        public override string ToString()
        {
            return name;
        }
    }
}
