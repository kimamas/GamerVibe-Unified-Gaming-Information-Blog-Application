using BLLClassLibrary.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLLClassLibrary.Intefaces
{
    public interface IReview
    {
        public void AddReview(string username, string gameName, string description, int rating, DateTime date);
        public List<Review> GetReviewsForTheGame(string gameName);
        public void UpdateReview(int reviewId, string description, int rating, string gameName);
        public void DeleteReview(int reviewId, string gameName);
    }
}
