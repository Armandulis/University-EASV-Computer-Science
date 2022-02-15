using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketsApi.Infrastructure;
using TicketsApi.Models;

namespace TicketsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketsRepository _repo;

        public TicketsController(ITicketsRepository repo)
        {
            _repo = repo;
        }


        // GET: api/Tickets
        [HttpGet]
        public IEnumerable<Ticket> Get()
        {
            return _repo.GetAll();
        }

        // GET: api/Tickets/5
        [HttpGet("{id}", Name = "Get")]
        public Ticket Get(int id)
        {
            if (id > 0)
            {
                return _repo.GetTicket(id);
            }

            return null;
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        [Route("GetByTitle/{title}")]
        public Ticket GetByTitle(string title)
        {
            if (title != "")
            {
                return _repo.GetByTitle(title);
            }

            return null;
        }

        // POST: api/Tickets
        [HttpPost]
        public IActionResult Post([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }

            _repo.Create(ticket);
            return Ok();
        }

        // PUT: api/Tickets
        [HttpPut]
        public IActionResult Put([FromBody] Ticket ticket)
        {
            if (ticket == null)
            {
                return BadRequest();
            }
            _repo.Edit(ticket);
            return Ok();
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _repo.Delete(id);
            return Ok();
        }
    }
}
