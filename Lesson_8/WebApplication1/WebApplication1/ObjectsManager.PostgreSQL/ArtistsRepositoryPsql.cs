using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.ObjectsManager.Interfaces;

namespace WebApplication1.ObjectsManager.PostgreSQL
{
    public class ArtistsRepositoryPsql : IArtistsRepository
    {
        private MuseumContext db = new MuseumContext();

        public int Add(Artist artist)
        {
            Artist newArtist = db.Artists.Add(new Artist() { ArtistName = artist.ArtistName, ArtistSurname = artist.ArtistSurname });
            db.SaveChanges();
            return newArtist.Id;    
        }

        public bool Delete(int id)
        {
            if (db.Artists.Find(id) == null)
                return false;
            Artist artist = db.Artists.Find(id);
            artist = db.Artists.Remove(artist);
            if (artist != null)
                return true;
            db.SaveChanges();
            return false;
        }

        public Artist Get(int id)
        {
            return db.Artists.Find(id);
        }

        public List<Artist> GetAll()
        {
            return db.Artists.ToList();
        }

        public Artist Update(Artist artist)
        {
            if (db.Artists.Find(artist.Id) == null)
                return null;
            Artist oldArtist = db.Artists.Find(artist.Id);
            db.Artists.Remove(oldArtist);
            artist.Id = oldArtist.Id;
            db.Artists.Add(artist);
            db.SaveChanges();
            return artist;
        }
    }
}