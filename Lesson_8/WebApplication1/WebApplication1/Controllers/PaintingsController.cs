using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApplication1.ObjectsManager.Interfaces;
using WebApplication1.Models;
using WebApplication1.LogServices;

namespace WebApplication1.Controllers
{
    public class PaintingsController : ApiController
    {
        private readonly IPaintingsRepository paintingRep;
        private readonly ILogger logger;

        public PaintingsController(IPaintingsRepository paintingRepository, ILogger ilogger)
        {
            paintingRep = paintingRepository;
            logger = ilogger;
        }

        public List<Painting> Get()
        {
            logger.WriteInfoLog(string.Format("{0} for {1} was called",
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            this.GetType().Name));
            return paintingRep.GetAll();
        }

        public Painting Get(int id)
        {
            logger.WriteInfoLog(string.Format("{0} for {1} was called",
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            this.GetType().Name));
            return paintingRep.Get(id);
        }

        public int Post(Painting painting)
        {
            logger.WriteInfoLog(string.Format("{0} for {1} was called",
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            this.GetType().Name));
            return paintingRep.Add(painting);
        }

        public Painting Put(int id, [FromBody] Painting painting)
        {
            logger.WriteInfoLog(string.Format("{0} for {1} was called",
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            this.GetType().Name));
            return paintingRep.Update(painting);
        }

        public void Delete(int id)
        {
            logger.WriteInfoLog(string.Format("{0} for {1} was called",
            System.Reflection.MethodBase.GetCurrentMethod().Name,
            this.GetType().Name));
            paintingRep.Delete(id);
        }
    }
}
