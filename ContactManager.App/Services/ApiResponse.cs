using System.Net;

namespace ContactManager.App.Services
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorText { get; set; }
        public bool IsSuccess => StatusCode == HttpStatusCode.OK;

        public ApiResponse(HttpStatusCode statusCode, string? errorText = null)
        {
            StatusCode = statusCode;
            ErrorText = errorText;
        }
    }
}