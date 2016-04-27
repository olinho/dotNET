using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ObjectsManager.Interfaces
{
    public interface IPaintingsRepository
    {
        List<Painting> GetAll();
        void Delete(int id);
        Painting Update(Painting painting);
        int Add(Painting painting);
        Painting Get(int id);
    }
}
