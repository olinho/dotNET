using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Models;

namespace WebApplication2.DAL
{
    public class StoreInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<StoreContext>
    {
        protected override void Seed(StoreContext context)
        {
            var authors = new List<Author>
            {
                new Author() { AuthorName = "Jan", AuthorSurname = "Broda" },
                new Author() {AuthorName = "Gosia", AuthorSurname = "Herbut" },
            };

            authors.ForEach(a => context.Authors.Add(a));
            context.SaveChanges();

            var books = new List<Book>
            {
                new Book() {Title = "Pan Wołodyjowski", ISBN = "1234" },
                new Book() {Title = "Pan Tadeusz", ISBN = "4432" },
            };
            books.ForEach(b => context.Books.Add(b));
            context.SaveChanges();
        }
    }
}