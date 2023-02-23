using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using LinkShorten.Persistence;
using LinkShorten.Models;
using LinkShorten.Entities;

namespace LinkShorten.Controllers
{
    [ApiController]
    [Route("api/ShortenedLinks")]
    public class ShortenLinksController : ControllerBase
    {
        private readonly LinkShortenContext _context;

        public ShortenLinksController(LinkShortenContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var links = _context.Links;

            return Ok(links);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var link = _context.Links.SingleOrDefault(p => p.Id == id);

            if(link == null)
                return NotFound("Link not found");

            return Ok(link);
        }
        /// <summary>
        /// Cadastrar um link encurtado
        /// </summary>
        /// <remarks>
        /// { "title": "Cadastrar um link encurtado", "destinationLink:"" }
        /// </remarks>
        /// <param name="model">Dados do link</param>
        /// <returns>Objetos recém-criado</returns>
        /// <response code:"201">Sucesso</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Create(AddOrUpdateShortenedLinkModel model)
        {
            var link = new ShortenedCustomLink(model.Title, model.DestinationLink);
            
            _context.Links.Add(link);
            _context.SaveChanges();

            return CreatedAtAction("GetById", new {id = link.Id }, link);
        }

        [HttpPut("{id}")]
        public IActionResult Update(AddOrUpdateShortenedLinkModel model, int id)
        {
            var link = _context.Links.SingleOrDefault(p => p.Id == id);

            if(link == null)
                return NotFound("Link not found");

            link.Update(model.Title, model.DestinationLink);
            _context.Links.Update(link);
            _context.SaveChanges();


            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Remove(int id)
        {
            var link = _context.Links.SingleOrDefault(p => p.Id == id);

            if(link == null)
                return NotFound("Link not found");
            
            _context.Links.Remove(link);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpGet("/{code}")]

        public IActionResult RedirectLink(string code)
        {
            var link = _context.Links.SingleOrDefault(p => p.Code == code);

            if(link == null)
                return NotFound();

            return Redirect(link.DestinationLink);

        }
    }
}