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
    public class BooksController : ApiController
    {
        private BookRepository bookRepository = new BookRepository();
        [HttpGet]
        public Book Get(int id)
        {
            return bookRepository.Get(id);
        }
        // GET books
        [HttpGet]
        public List<Book> Get()
        {
            return bookRepository.GetAll();
        }
        [HttpGet]
        public Book UriGet([FromUri] int id)
        {
            return bookRepository.Get(id);
        }
        [HttpGet]
        public List<Book> SearchGet([FromUri] string search)
        {
            if (bookRepository.GetAll() == null)
                return null;
            return bookRepository.GetAll().Where(x => x.BookTitle.Contains(search)).ToList();

        }

        // PUT api/books/5
        [HttpPut]
        public Book Put(int id, [FromBody] Book book)
        {
            Book newAuthor = new Book() { Id = id, BookTitle = book.BookTitle, ISBN = book.ISBN };
            return bookRepository.Update(newAuthor);
        }
        // POST api/books
        // return for example 5
        [HttpPost]
        public int Post([FromBody] Book book)
        {
            return bookRepository.Add(book);
        }
        [HttpDelete]
        public bool Delete(int id)
        {
            return bookRepository.Delete(id);
        }
    }
}
