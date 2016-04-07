using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ObjectsManager.Interfaces;
using ObjectsManager.Model;
using ObjectsManager.LiteDB;

namespace ServiceREST.Controllers
{
    public class AuthorsController : ApiController
    {
        private AuthorRepository authorRepository = new AuthorRepository();
        // GET author
        [HttpGet]
        public Author Get(int id)
        {
            return authorRepository.Get(id);
        }
        // GET authors
        [HttpGet]
        public List<Author> Get()
        {
            return authorRepository.GetAll();
        }
        [HttpGet]
        public Author UriGet([FromUri] int id)
        {
            return authorRepository.Get(id);
        }
        [HttpGet]
        public List<Author> SearchGet([FromUri] string search)
        {
            List<Author> list = new List<Author>();
            foreach (var A in authorRepository.GetAll())
            {
                try
                {
                    if (A.AuthorName == null)
                        continue;
                    if (A.AuthorName.Contains(search))
                    {
                        list.Add(A);
                    }
                }catch(ArgumentNullException e)
                {

                }
            }
            return list;
                
        }

        // PUT api/authors/5
        [HttpPut]
        public Author Put(int id, [FromBody] Author author)
        {
            Author newAuthor = new Author() { Id = id, AuthorName = author.AuthorName, AuthorSurname = author.AuthorSurname };
            return authorRepository.Update(newAuthor);
        }
        // POST api/authors
        // return for example 5
        [HttpPost]
        public int Post([FromBody] Author author)
        {
            return authorRepository.Add(author);
        }
        [HttpDelete]
        public bool Delete(int id)
        {
            return authorRepository.Delete(id);
        }
    }
}
