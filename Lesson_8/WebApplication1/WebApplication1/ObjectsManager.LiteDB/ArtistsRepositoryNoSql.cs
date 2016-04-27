using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;
using WebApplication1.ObjectsManager.Interfaces;
using WebApplication1.ObjectsManager.LiteDB.Model;

namespace WebApplication1.ObjectsManager.LiteDB
{
    public class ArtistsRepositoryNoSql : IArtistsRepository
    {
        private readonly string _artistConnection = DatabaseConnections.ArtistConnection;

        public ArtistsRepositoryNoSql()
        {

        }
        public int Add(Artist artist)
        {
            using (var db = new LiteDatabase(this._artistConnection))
            {

                var repository = db.GetCollection<ArtistDB>("artists");
                
                ArtistDB dbArtist = InverseMap(artist);
                if (repository.FindById(dbArtist.Id) != null)
                {
                    repository.Update(dbArtist);
                }
                else
                {
                    repository.Insert(dbArtist);
                }
                return dbArtist.Id;
            }
        }

        public bool Delete(int id)
        {
            using (var db = new LiteDatabase(this._artistConnection))
            {
                var repository = db.GetCollection<ArtistDB>("artists");

                return repository.Delete(id);
            }
        }

        public Artist Get(int id)
        {
            using (var db = new LiteDatabase(this._artistConnection))
            {
                var repository = db.GetCollection<ArtistDB>("artists");
                return Map(repository.FindById(id));
            }
        }

        public List<Artist> GetAll()
        {
            using (var db = new LiteDatabase(this._artistConnection))
            {
                var repository = db.GetCollection<ArtistDB>("artists");
                List<Artist> list = new List<Artist>();
                foreach (ArtistDB a in repository.FindAll())
                {
                    list.Add(Map(a));
                }
                return list;
            }
        }

        public Artist Update(Artist artist)
        {
            using (var db = new LiteDatabase(this._artistConnection))
            {
                var repository = db.GetCollection<ArtistDB>("artists");
                ArtistDB dbArtist = InverseMap(artist);
                if (repository.Update(dbArtist))
                    return Map(dbArtist);
                return null;
            }
        }

        internal Artist Map(ArtistDB dbArtist)
        {
            if (dbArtist == null)
                return null;
            return new Artist()
            {
                Id = dbArtist.Id,
                ArtistName = dbArtist.ArtistName,
                ArtistSurname = dbArtist.ArtistSurname
            };
        }
        internal ArtistDB InverseMap(Artist artist)
        {
            if (artist == null)
                return null;
            return new ArtistDB()
            {
                Id = artist.Id,
                ArtistSurname = artist.ArtistSurname,
                ArtistName = artist.ArtistName
            };
        }
    }
}