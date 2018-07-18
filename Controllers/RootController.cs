
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Messaging.Models;
using Steeltoe.Extensions.Configuration.CloudFoundry;
using Messaging;
using Microsoft.EntityFrameworkCore;

namespace Preferences.Controllers
{
    [Produces("application/json")]
    public class RootController : Controller
    {

		// TODO: Convert to Async await pattern, use constructor injection and create a repository layer

        private MessagingContext _context;

		public RootController(MessagingContext context)
		{
            _context = context;
		}

		// TODO: Add access control security
		// TODO: Add IDataProtectionProvider to encrypt ids

        // GET ALL  
    	[HttpGet()]
		public JsonResult Get()
		{
            var messages = _context.Messages.Where(m => m.DeletedDate == null);
			return Json(messages);

		}
		
        // GET ONE: /id
        [HttpGet("{id}")]
        public JsonResult Get(int id)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == id && m.DeletedDate == null);

            // ToDO: Update ViewedDate if null and current user = to user

            return Json(message);
        }

        [HttpGet("touser/{usertype}/{id}")]
        public JsonResult GetByToId(int usertype, int id)
        {
                var message = _context.Messages.Where(m => m.ToUserType == usertype && m.ToId == id);

			    return Json(message);
        }
        
        [HttpGet("fromuser/{usertype}/{id}")]
        public JsonResult GetByFromId(int usertype, int id)
        {
                var message = _context.Messages.Where(m => m.FromUserType == usertype && m.FromId == id);

			    return Json(message);
        }
        
        // POST - CREATE:
        [HttpPost("/create")]
        public IActionResult Post([FromBody] Message message)
        {
            var messages = _context.Messages.Add(message);
            _context.SaveChanges();
            return Ok();
        }
        
        // DELETE: Root/id
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var message = _context.Messages.FirstOrDefault(m => m.Id == id);

            if(message == null)
            {
                return NotFound();
            }

            message.DeletedDate = DateTime.UtcNow;
            _context.SaveChanges();

			return Ok();
        }
    }
}