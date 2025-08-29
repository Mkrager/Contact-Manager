using ContactManager.App.Contracts;
using ContactManager.App.Models;
using System.Text.Json;

namespace ContactManager.App.Services
{
    public class ContactDataService : IContactDataService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ContactDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;

            _jsonOptions = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }

        public async Task<List<ContactViewModel>> GetAllContacts()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://localhost:7237/api/contact");

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();

                var contactList = JsonSerializer.Deserialize<List<ContactViewModel>>(responseContent, _jsonOptions);

                return contactList;
            }

            return new List<ContactViewModel>();
        }
    }
}
