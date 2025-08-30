using ContactManager.App.Contracts;
using ContactManager.App.Models;
using System.Text;
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

        public async Task DeleteContactAsync(Guid id)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Delete, $"https://localhost:7237/api/contact/{id}");

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    //TODO
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ContactViewModel>> GetAllContactsAsync()
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

        public async Task UpdateContactAsync(ContactViewModel contactViewModel)
        {
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Put, $"https://localhost:7237/api/contact")
                {
                    Content = new StringContent(JsonSerializer.Serialize(contactViewModel), Encoding.UTF8, "application/json")
                };

                var response = await _httpClient.SendAsync(request);

                if (response.IsSuccessStatusCode)
                {
                    //return new ApiResponse(System.Net.HttpStatusCode.OK);
                }

                //var errorContent = await response.Content.ReadAsStringAsync();
                //var errorMessages = JsonSerializer.Deserialize<List<string>>(errorContent);
                //return new ApiResponse(System.Net.HttpStatusCode.BadRequest, errorMessages.FirstOrDefault());
            }
            catch (Exception ex)
            {
                //return new ApiResponse(System.Net.HttpStatusCode.BadRequest, ex.Message);
            }
        }

        public async Task UploadCsvAsync(Stream fileStream)
        {
            using var content = new MultipartFormDataContent();
            using var streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");
            content.Add(streamContent, "file", "contacts.csv");

            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7237/api/contact")
            {
                Content = content
            };

            var response = await _httpClient.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                //TODO
            }
        }
    }
}
