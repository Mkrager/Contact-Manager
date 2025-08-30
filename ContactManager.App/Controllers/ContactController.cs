using ContactManager.App.Contracts;
using ContactManager.App.Models;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.App.Controllers
{
    public class ContactController : Controller
    {
        private readonly IContactDataService _contactDataService;

        public ContactController(IContactDataService contactDataService)
        {
            _contactDataService = contactDataService;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _contactDataService.DeleteContactAsync(id);

            return Json(new { redirectUrl = Url.Action("Index", "Home") });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            await _contactDataService.UploadCsvAsync(file.OpenReadStream());

            return RedirectToAction("Index", "Home");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContactViewModel contactViewModel)
        {
            await _contactDataService.UpdateContactAsync(contactViewModel);

            return RedirectToAction("Index", "Home");
        }
    }
}