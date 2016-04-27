using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class MuseumInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<MuseumContext>
    {
        protected override void Seed(MuseumContext context)
        {
            //base.Seed(context);
            var artists = new List<Artist>()
            {
                new Artist {ArtistName = "Marek", ArtistSurname = "Dzięcioł"},
                new Artist {ArtistName = "Jan", ArtistSurname = "Matejko" },
            };
            artists.ForEach(a => context.Artists.Add(a));
            context.SaveChanges();

            var paintings = new List<Painting>()
            {
                new Painting {Title = "Bitwa pod Grunwaldem", Year=1410 },
                new Painting {Title = "Stasiek Poniatowski", Year = 1750 },
            };
            paintings.ForEach(p => context.Paintings.Add(p));
            context.SaveChanges();  
        }
    }
}