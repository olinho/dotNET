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
    public class AuthorRepository : IAuthorRepository
    {
        private readonly string _authorConnection = DatabaseConnections.AuthorConnection;
        public AuthorRepository()
        {
           // Add(new Author() { AuthorName = "Weronika", AuthorSurname = "Tym" });
        }
        public int Add(Author author)
        {
            using (var db = new LiteDatabase(this._authorConnection))
            {
                AuthorDB dbAuthor = InverseMap(author);
                var repository = db.GetCollection<AuthorDB>("authors");

                if (repository.FindById(dbAuthor.Id) != null)
                {
                    repository.Update(dbAuthor);
                }
                else
                {
                    repository.Insert(dbAuthor);
                }
                return dbAuthor.Id;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(this._authorConnection))
            {
                var repository = db.GetCollection<AuthorDB>("authors");
                return repository.Delete(id);
            }
        }

        public Author Get(int id)
        {
            using (var db = new LiteDatabase(this._authorConnection))
            {
                var repository = db.GetCollection<AuthorDB>("authors");
                return Map(repository.FindById(id));
            }
        }

        public List<Author> GetAll()
        {
            using (var db = new LiteDatabase(this._authorConnection))
            {
                var repository = db.GetCollection<AuthorDB>("authors");
                var results = repository.FindAll();
                return results.Select(x => Map(x)).ToList();
            }
        }

        public Author Update(Author author)
        {
            using (var db = new LiteDatabase(this._authorConnection))
            {
                var repository = db.GetCollection<AuthorDB>("authors");
                AuthorDB dbAuthor = InverseMap(author);
                if (repository.Update(dbAuthor))
                    return Map(dbAuthor);
                return null;

            }
        }

        internal Author Map(AuthorDB dbAuthor)
        {
            if (dbAuthor == null)
                return null;
            return new Author() { Id = dbAuthor.Id, AuthorName = dbAuthor.AuthorName, AuthorSurname = dbAuthor.AuthorSurname };
        }

        internal AuthorDB InverseMap(Author author)
        {
            if (author == null)
                return null;
            return new AuthorDB() { Id = author.Id, AuthorName = author.AuthorName, AuthorSurname = author.AuthorSurname };
        }
    }
}
