using ContactManager.App.Contracts;
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
            await _contactDataService.DeleteContact(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            await _contactDataService.UploadCsv(file.OpenReadStream());

            return RedirectToAction("Index");
        }
    }
}