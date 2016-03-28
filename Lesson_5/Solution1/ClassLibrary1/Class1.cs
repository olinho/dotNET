using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1
{
    public class Movie
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ReleaseYear { get; set; }
    }

    public class Review
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public Person Author { get; set; }
        public int MovieId { get; set; } 
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
