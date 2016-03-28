using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

using ObjectManager.Model;
using ObjectsManager.Interfaces;
using ObjectsManager.LiteDB;

namespace Wcf_Review
{
    
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Service_Review : IService_Review
    {
        private readonly IReviewRepository _reviewRepository;

        public Service_Review()
        {
            _reviewRepository = new ReviewRepository();
        }
        public int AddReview(Review r)
        {
            return _reviewRepository.Add(r);
        }

        public bool DeleteReview(int id)
        {
            return _reviewRepository.Delete(id);
        }

        public List<Review> GetAllReviews()
        {
            return _reviewRepository.GetAll();
        }

        public Review GetReview(int id)
        {
            return _reviewRepository.Get(id);
        }

        public Review UpdateReview(Review r)
        {
            return _reviewRepository.Update(r);
        }
    }
}
