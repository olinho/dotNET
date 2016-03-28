using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiteDB;
using ObjectManager.Model;
using ObjectsManager.LiteDB.Model;
using ObjectsManager.Interfaces;

namespace ObjectsManager.LiteDB
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly string _reviewConnection = DatabaseConnections.ReviewConnection;
        public int Add(Review review)
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var dbObject = InverseMap(review);

                var repository = db.GetCollection<ReviewDB>("reviews");
                if (repository.FindById(review.Id) != null)
                    repository.Update(dbObject);
                else
                    repository.Insert(dbObject);

                return dbObject.Id;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var repository = db.GetCollection<ReviewDB>("reviews");
                return repository.Delete(id);
            }
        }

        public Review Get(int id)
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var repository = db.GetCollection<ReviewDB>("reviews");
                var result = repository.FindById(id);
                return Map(result);
            }
        }

        public List<Review> GetAll()
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var repository = db.GetCollection<ReviewDB>("reviews");
                var results = repository.FindAll();
                return results.Select(x => Map(x)).ToList();
            }
        }

        public Review Update(Review review)
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var dbObject = InverseMap(review);

                var repository = db.GetCollection<ReviewDB>("reviews");
                if (repository.Update(dbObject))
                    return Map(dbObject);
                else
                    return null;
            }
        }

        internal List<ReviewDB> GetReviews(int[] ids)
        {
            using (var db = new LiteDatabase(this._reviewConnection))
            {
                var repository = db.GetCollection<ReviewDB>("reviews");
                var results = repository.FindAll().Where(x => ids.Contains(x.Id));

                return results.ToList();
            }
        }

        internal Review Map(ReviewDB dbReview)
        {
            if (dbReview == null)
                return null;
            return new Review()
            {
                Id = dbReview.Id,
                Author = dbReview.Map(dbReview.Author),
                Content = dbReview.Content,
                MovieId = dbReview.MovieId,
                Score = dbReview.Score
            };
        }

        internal ReviewDB InverseMap(Review review)
        {
            if (review == null)
                return null;
            return new ReviewDB()
            {
                Id = review.Id,
                Author = new ReviewDB().InverseMap(review.Author),
                Content = review.Content,
                MovieId = review.MovieId,
                Score = review.Score
            };
        }

    }
}
