using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.ObjectsManager.Interfaces
{
    public interface IArtistsRepository
    {
        List<Artist> GetAll();
        bool Delete(int id);
        int Add(Artist artist);
        Artist Update(Artist artist);
        Artist Get(int id);
    }
}
