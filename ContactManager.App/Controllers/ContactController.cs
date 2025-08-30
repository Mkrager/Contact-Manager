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
            var result = await _contactDataService.DeleteContactAsync(id);

            if (result.IsSuccess)
                return Ok(new { redirectToUrl = Url.Action("Index", "Home") });

            TempData["ErrorMessage"] = result.ErrorText;

            return Ok(new { redirectToUrl = Url.Action("Index", "Home") });
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var result = await _contactDataService.UploadCsvAsync(file.OpenReadStream());

            if (result.IsSuccess)
            {
                TempData["SuccessMessage"] = "Success!";
                return RedirectToAction("Index", "Home");
            }

            TempData["ErrorMessage"] = result.ErrorText;

            return RedirectToAction("Index", "Home");
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ContactViewModel contactViewModel)
        {
            var result = await _contactDataService.UpdateContactAsync(contactViewModel);

            if (result.IsSuccess)
                return Ok(new { redirectToUrl = Url.Action("Index", "Home") });

            TempData["ErrorMessage"] = result.ErrorText;

            return Ok(new { redirectToUrl = Url.Action("Index", "Home") });
        }
    }
}