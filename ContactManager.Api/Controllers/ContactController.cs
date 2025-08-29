using ContactManager.Application.Features.Contacts.Commands.UploadContactCsv;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController(IMediator mediator) : Controller
    {
        [HttpPost(Name = "UploadContacts")]
        public async Task<ActionResult<Guid>> Create(IFormFile file)
        {
            var id = await mediator.Send(new UploadContactCsvCommand()
            {
                FileStream = file.OpenReadStream()
            });
            return Ok(id);
        }
    }
}
