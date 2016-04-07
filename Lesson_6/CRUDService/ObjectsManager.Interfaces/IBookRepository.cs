using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ObjectsManager.Model; 

namespace ObjectsManager.Interfaces
{
    public interface IBookRepository
    {
        List<Book> GetAll();
        int Add(Book book);
        bool Delete(int id);
        Book Get(int id);
        Book Update(Book book);
    }
}
