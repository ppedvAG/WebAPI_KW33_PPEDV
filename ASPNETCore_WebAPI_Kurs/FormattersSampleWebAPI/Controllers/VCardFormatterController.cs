using FormattersSampleWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FormattersSampleWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VCardFormatterController : ControllerBase
    {

        [HttpGet]
        public Contact GetContact()
        {
            Contact contact = new Contact();
            contact.Id = 1;
            contact.Firstname = "Otto";
            contact.Lastname = "Walkes";

            return contact;
        }


        [HttpPost]
        public IActionResult Insert(Contact contact)
        {
            return Ok();
        }
    }
}
