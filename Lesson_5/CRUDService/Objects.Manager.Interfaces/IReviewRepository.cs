using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectManager.Model;

namespace ObjectsManager.Interfaces
{
    public interface IReviewRepository
    {
        List<Review> GetAll();
        Review Get(int id);
        bool Delete(int id);
        Review Update(Review review);
        int Add(Review review);
    }
}
