using ContactManager.App.Models;

namespace ContactManager.App.Contracts
{
    public interface IContactDataService
    {
        Task<List<ContactViewModel>> GetAllContactsAsync();
        Task DeleteContactAsync(Guid id);
        Task UpdateContactAsync(ContactViewModel contactViewModel);
        Task UploadCsvAsync(Stream fileStream);
    }
}
