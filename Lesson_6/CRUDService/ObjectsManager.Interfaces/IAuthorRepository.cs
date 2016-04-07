using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectsManager.Model;

namespace ObjectsManager.Interfaces
{
    public interface IAuthorRepository
    {
        List<Author> GetAll();
        int Add(Author author);
        bool Delete(int id);
        Author Get(int id);
        Author Update(Author author);
    }
}
