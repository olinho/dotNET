using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.ObjectsManager.Interfaces;
using WebApplication1.ObjectsManager.LiteDB;
using WebApplication1.ObjectsManager.LiteDB.Model;

namespace WebApplication1.ObjectsManager.LiteDB
{
    public class PaintingsRepositoryNoSql : IPaintingsRepository
    {
        private readonly string _paintingConnection = DatabaseConnections.PaintingConnection;
        public int Add(Painting painting)
        {
            using (var db = new LiteDatabase(this._paintingConnection))
            {
                PaintingDB dbPainting = InverseMap(painting);
                var repository = db.GetCollection<PaintingDB>("paintings");

                if (repository.FindById(dbPainting.Id) != null)
                {
                    repository.Update(dbPainting);
                }
                else
                {
                    repository.Insert(dbPainting);
                }
                return dbPainting.Id;
            }
        }

        public void Delete(int id)
        {
            using (var db = new LiteDatabase(this._paintingConnection))
            {
                var repository = db.GetCollection<PaintingDB>("paintings");

                repository.Delete(id);
            }
        }

        public Painting Get(int id)
        {
            using (var db = new LiteDatabase(this._paintingConnection))
            {
                var repository = db.GetCollection<PaintingDB>("paintings");
                return Map(repository.FindById(id));
            }
        }

        public List<Painting> GetAll()
        {
            using (var db = new LiteDatabase(this._paintingConnection))
            {
                var repository = db.GetCollection<PaintingDB>("paintings");
                List<Painting> list = new List<Painting>();
                foreach (PaintingDB p in repository.FindAll())
                {
                    list.Add(Map(p));
                }
                return list;
            }
        }

        public Painting Update(Painting painting)
        {
            using (var db = new LiteDatabase(this._paintingConnection))
            {
                var repository = db.GetCollection<PaintingDB>("paintings");
                PaintingDB dbPainting = InverseMap(painting);
                if (repository.Update(dbPainting))
                    return Map(dbPainting);
                return null;
            }
        }

        internal Painting Map(PaintingDB dbPainting)
        {
            if (dbPainting == null)
                return null;
            return new Painting()
            {
                Id = dbPainting.Id,
                Title = dbPainting.Title,
                Year = dbPainting.Year
            };
        }
        internal PaintingDB InverseMap(Painting painting)
        {
            if (painting == null)
                return null;
            return new PaintingDB()
            {
                Id = painting.Id,
                Title = painting.Title,
                Year = painting.Year
            };
        }
    }
}