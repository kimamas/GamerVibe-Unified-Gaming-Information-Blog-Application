using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLLClassLibrary.Intefaces;
using BLLClassLibrary.Entity;
namespace BLLClassLibrary.Managers
{
    public class ReviewManager
    {
        private readonly IReview iReview;
        public ReviewManager(IReview iReview)
        {
            this.iReview = iReview;
        }
        public void AddReview(string username, string gameName, string description, int rating, DateTime date)
        {
            iReview.AddReview(username, gameName, description, rating, date);
        }

        public List<Review> GetReviewsForTheGame(string gameName) => iReview.GetReviewsForTheGame(gameName);
        public void UpdateReview(int reviewId, string description, int rating, string gameName) => iReview.UpdateReview(reviewId,description, rating, gameName);
        public void DeleteReview(int reviewId, string gameName) => iReview.DeleteReview(reviewId, gameName);
    }
}
