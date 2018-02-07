using DndSpellDatabase.EntityDataModel;
using DndSpellDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DndSpellDatabase.Controllers
{
    public class SpellController : ApiController
    {
        // GET: api/Spell
        public IEnumerable<Spell> Get()
        {
            return SpellRepository.GetSpell();
        }

        // GET: api/Spell/5
        public Spell Get(int id)
        {
            return SpellRepository.GetSpell().FirstOrDefault(p => p.Id == id);
        }

        // POST: api/Spell
        public HttpResponseMessage Post(Spell spell)
        {
            SpellRepository.InsertSpell(spell);
            var response = Request.CreateResponse(HttpStatusCode.Created, spell);
            string url = Url.Link("DefaultApi", new { spell.Id });
            response.Headers.Location = new Uri(url);
            return response;
        }

        // DELETE: api/Spell/5
        public HttpResponseMessage Delete(int id)
        {
            SpellRepository.DeleteSpell(id);
            var response = Request.CreateResponse(HttpStatusCode.OK, id);
            return response;
        }
    }
}
