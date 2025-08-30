using ContactManager.App.Models;
using ContactManager.App.Services;

namespace ContactManager.App.Contracts
{
    public interface IContactDataService
    {
        Task<List<ContactViewModel>> GetAllContactsAsync();
        Task<ApiResponse> UploadCsvAsync(Stream fileStream);
        Task<ApiResponse> UpdateContactAsync(ContactViewModel contactViewModel);
        Task<ApiResponse> DeleteContactAsync(Guid id);
    }
}
