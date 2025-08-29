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
    }
}