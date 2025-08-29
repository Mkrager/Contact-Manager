using ContactManager.App.Models;

namespace ContactManager.App.Contracts
{
    public interface IContactDataService
    {
        Task<List<ContactViewModel>> GetAllContacts();
    }
}
