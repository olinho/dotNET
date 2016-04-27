using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.ObjectsManager.Interfaces;
using WebApplication1.ObjectsManager.PostgreSQL;
using WebApplication1.ObjectsManager.LiteDB;
using WebApplication1.Models;
using WebApplication1.LogServices;

namespace WebApplication1.Controllers
{
    public class ArtistsController : ApiController
    {
        private IArtistsRepository artistRep;
        private readonly ILogger logger;

        public ArtistsController(IArtistsRepository artistRepository, ILogger ilogger)
        {
            artistRep = artistRepository;
            logger = ilogger;
        }
        //GET api/Artists
        public List<Artist> GetArtists()
        {
            logger.WriteInfoLog(string.Format("{0} for {1} was called",
                System.Reflection.MethodBase.GetCurrentMethod().Name,
                this.GetType().Name));
            return artistRep.GetAll();
        }

        [HttpGet]
        public Artist UriGet([FromUri] int id)
        {
            logger.WriteInfoLog(string.Format("{0} for {1} was called",
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            this.GetType().Name));
            return artistRep.Get(id);
        }

        [HttpGet]
        public Artist Get(int id)
        {
            logger.WriteInfoLog(string.Format("{0} for {1} was called",
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            this.GetType().Name));
            return artistRep.Get(id);
        }

        [HttpPut]
        public Artist Put(int id, [FromBody] Artist artist)
        {
            logger.WriteInfoLog(string.Format("{0} for {1} was called",
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            this.GetType().Name));
            Artist newArtist = new Artist()
            {
                Id = artist.Id,
                ArtistName = artist.ArtistName,
                ArtistSurname = artist.ArtistSurname
            };
            return artistRep.Update(newArtist);
        }

        [HttpPost]
        public int Post([FromBody] Artist artist)
        {
            logger.WriteInfoLog(string.Format("{0} for {1} was called",
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            this.GetType().Name));
            return artistRep.Add(artist);
        }

        [HttpDelete]
        public bool Delete(int id)
        {
            logger.WriteInfoLog(string.Format("{0} for {1} was called",
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            this.GetType().Name));
            return artistRep.Delete(id);
        }
    }
}
