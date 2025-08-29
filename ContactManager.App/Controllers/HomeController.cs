using ContactManager.App.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace ContactManager.App.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactDataService _contactDataService;

        public HomeController(IContactDataService contactDataService)
        {
            _contactDataService = contactDataService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var contacts = await _contactDataService.GetAllContacts();
            return View(contacts);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            ms.Position = 0;

            await _contactDataService.UploadCsv(ms);

            return RedirectToAction("Index");
        }
    }
}