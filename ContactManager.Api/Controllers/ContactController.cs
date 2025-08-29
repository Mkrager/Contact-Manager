using ContactManager.Application.Features.Contacts.Commands.DeleteContact;
using ContactManager.Application.Features.Contacts.Commands.UploadContactCsv;
using ContactManager.Application.Features.Contacts.Queries.GetContactsList;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController(IMediator mediator) : Controller
    {
        [HttpGet(Name = "GetAllContacts")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<List<ContactsListVm>>> GetAllContacts()
        {
            var dtos = await mediator.Send(new GetContactsListQuery());
            return Ok(dtos);
        }


        [HttpPost(Name = "UploadContacts")]
        public async Task<ActionResult<Guid>> Create(IFormFile file)
        {
            var id = await mediator.Send(new UploadContactCsvCommand()
            {
                FileStream = file.OpenReadStream()
            });
            return Ok(id);
        }

        [HttpDelete("{id}", Name = "DeleteContact")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult> DeleteContact(Guid id)
        {
            var deleteCourseCommand = new DeleteContactCommand() { Id = id };
            await mediator.Send(deleteCourseCommand);
            return NoContent();
        }
    }
}
