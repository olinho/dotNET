using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteDB;
using ObjectsManager.Interfaces;
using ObjectsManager.Model;
using ObjectsManager.LiteDB.Model;

namespace ObjectsManager.LiteDB
{
    public class BookRepository : IBookRepository
    {
        private readonly string _bookConnection = DatabaseConnections.BookConnection;
        public int Add(Book book)
        {
            using (var db = new LiteDatabase(this._bookConnection))
            {
                BookDB dbBook = InverseMap(book);
                var repository = db.GetCollection<BookDB>("books");

                if (repository.FindById(dbBook.Id) != null)
                {
                    repository.Update(dbBook);
                }
                else
                {
                    repository.Insert(dbBook);
                }
                return dbBook.Id;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(this._bookConnection))
            {
                var repository = db.GetCollection<BookDB>("books");
                return repository.Delete(id);
            }
        }

        public Book Get(int id)
        {
            using (var db = new LiteDatabase(this._bookConnection))
            {
                var repository = db.GetCollection<BookDB>("books");
                return Map(repository.FindById(id));
            }
        }

        public List<Book> GetAll()
        {
            using (var db = new LiteDatabase(this._bookConnection))
            {
                var repository = db.GetCollection<BookDB>("books");
                var results = repository.FindAll();
                return results.Select(x => Map(x)).ToList();
            }
        }

        public Book Update(Book book)
        {
            using (var db = new LiteDatabase(this._bookConnection))
            {
                var repository = db.GetCollection<BookDB>("books");
                BookDB dbBook = InverseMap(book);
                if (repository.Update(dbBook))
                    return Map(dbBook);
                return null;

            }
        }

        internal Book Map(BookDB dbBook)
        {
            if (dbBook == null)
                return null;
            return new Book() { Id = dbBook.Id, BookTitle = dbBook.BookTitle, ISBN = dbBook.ISBN };
        }

        internal BookDB InverseMap(Book book)
        {
            if (book == null)
                return null;
            return new BookDB() { Id = book.Id, BookTitle = book.BookTitle, ISBN = book.ISBN };
        }
    }
}
