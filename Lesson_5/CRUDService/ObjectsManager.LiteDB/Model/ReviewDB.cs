using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ObjectsManager.LiteDB.Model
{
    public class ReviewDB
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int Score { get; set; }
        public Person Author { get; set; }
        public int MovieId { get; set; }

        public ObjectManager.Model.Person Map(Person P)
        {
            if (P == null)
                return null;
            return new ObjectManager.Model.Person() { Id = P.Id, Name = P.Name, Surname = P.Surname };
        }

        public Person InverseMap(ObjectManager.Model.Person P)
        {
            if (P == null)
                return null;
            return new Person() { Id = P.Id, Name = P.Name, Surname = P.Surname };
        }
    }
}
