using System.Data;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.OData;
using System.Web.OData.Routing;
using Library;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using Library;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<CardShirt>("CardShirts");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class CardShirtsController : ODataController
    {
        private GamesContext db = new GamesContext();

        [HttpGet]
        [ODataRoute("GetAvailableCardShirts")]
        public IHttpActionResult GetAvailableCardsShirts()
        {
            var cards = db.CardShirts;
            return Ok(cards);
        }

        // GET: odata/CardShirts
        [EnableQuery]
        public IQueryable<CardShirt> GetCardShirts()
        {
            return db.CardShirts;
        }

        // GET: odata/CardShirts(5)
        [EnableQuery]
        public SingleResult<CardShirt> GetCardShirt([FromODataUri] int key)
        {
            return SingleResult.Create(db.CardShirts.Where(cardShirt => cardShirt.Id == key));
        }

        // PUT: odata/CardShirts(5)
        public async Task<IHttpActionResult> Put([FromODataUri] int key, Delta<CardShirt> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CardShirt cardShirt = await db.CardShirts.FindAsync(key);
            if (cardShirt == null)
            {
                return NotFound();
            }

            patch.Put(cardShirt);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardShirtExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(cardShirt);
        }

        // POST: odata/CardShirts
        public async Task<IHttpActionResult> Post(CardShirt cardShirt)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CardShirts.Add(cardShirt);
            await db.SaveChangesAsync();

            return Created(cardShirt);
        }

        // PATCH: odata/CardShirts(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] int key, Delta<CardShirt> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CardShirt cardShirt = await db.CardShirts.FindAsync(key);
            if (cardShirt == null)
            {
                return NotFound();
            }

            patch.Patch(cardShirt);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardShirtExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(cardShirt);
        }

        // DELETE: odata/CardShirts(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] int key)
        {
            CardShirt cardShirt = await db.CardShirts.FindAsync(key);
            if (cardShirt == null)
            {
                return NotFound();
            }

            db.CardShirts.Remove(cardShirt);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardShirtExists(int key)
        {
            return db.CardShirts.Count(e => e.Id == key) > 0;
        }
    }
}
