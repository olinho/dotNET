using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.ObjectsManager.Interfaces;
using WebApplication1.DAL;

namespace WebApplication1.ObjectsManager.PostgreSQL
{
    public class PaintingsRepositoryPsql : IPaintingsRepository
    {
        private MuseumContext db = new MuseumContext();


        public int Add(Painting painting)
        {
            Painting newP = db.Paintings.Add(new Painting()
            {
                Title = painting.Title,
                Year = painting.Year
            });
            db.SaveChanges();
            return newP.Id;
        }

        public void Delete(int id)
        {
            if (db.Paintings.Find(id) == null)
                return;
            Painting p = db.Paintings.Find(id);
            db.Paintings.Remove(p);
            db.SaveChanges();
        }

        public Painting Get(int id)
        {
            if (db.Paintings.Find(id) == null)
                return null;
            return db.Paintings.Find(id);
        }

        public List<Painting> GetAll()
        {
            List<Painting> list = db.Paintings.ToList();
            return list;
        }

        public Painting Update(Painting painting)
        {
            Painting oldP = db.Paintings.Find(painting.Id);
            db.Paintings.Remove(oldP);
            db.Paintings.Add(painting);
            db.SaveChanges();
            return painting;
        }
    }
}