using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Helpdesk_Ticketing.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Helpdesk_Ticketing.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private static readonly TicketTypes[] ticketTypes = new TicketTypes[]
        {
            new TicketTypes{ ID = 1, NameTicket = "Ticket ONE", Comments = "I need help description"},
            new TicketTypes{ ID = 2, NameTicket = "Ticket TWO", Comments = "2nd description"},
            new TicketTypes{ ID = 3, NameTicket = "Ticket THREE", Comments = "The force is strong with this one."},
            new TicketTypes{ ID = 4, NameTicket = "Ticket FOUR", Comments = "Make it so number one."},
            new TicketTypes{ ID = 5, NameTicket = "Ticket FIVE", Comments = "To infinity, and beyond?"}
        };

        private readonly ILogger<ProductController> _logger;

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
        }

        // GET: api/Product
        [HttpGet]
        public IEnumerable<TicketTypes> Get()
        {
            return ticketTypes.ToArray();
        }


        // GET: api/Product/5
        [HttpGet("{id}")]
        public object Get(int id)
        {
            //return "value";
            var product = ticketTypes.Where(x => x.ID == id).ToList();
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // POST: api/Product
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Product/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
