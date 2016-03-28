using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

using System.ServiceModel;
using ObjectManager.Model;

namespace Wcf_Review
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract]
    public interface IService_Review
    {
        [OperationContract]
        int AddReview(Review r);

        [OperationContract]
        bool DeleteReview(int id);

        [OperationContract]
        Review UpdateReview(Review r);

        [OperationContract]
        List<Review> GetAllReviews();

        [OperationContract]
        Review GetReview(int id);
    }
}
