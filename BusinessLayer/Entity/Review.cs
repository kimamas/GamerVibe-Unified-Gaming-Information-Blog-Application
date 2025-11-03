using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLClassLibrary.Entity
{
    public class Review
    {
        private int reviewId;
        private string username;
        private string gameName;
        private string description;
        private double rating;
        private DateTime dateOfPosting;
        private bool updated;
        public string Username { get => username; set => username = value; }
        public string GameName { get => gameName; set => gameName = value; }
        public string Description { get => description; set => description = value; }
        public double Rating { get => rating; set => rating = value; }
        public DateTime DateOfPosting { get => dateOfPosting; set => dateOfPosting = value; }
        public int ReviewId { get => reviewId; set => reviewId = value; }
        public bool Updated { get => updated; set => updated = value; }
        public Review(int reviewId, string username, string gameName, string description, double rating, DateTime dateOfPosting, bool updated)
        {
            ReviewId = reviewId;
            Username = username;
            GameName = gameName;
            Description = description;
            Rating = rating;
            DateOfPosting = dateOfPosting;
            Updated = updated;
        }
    }
}
