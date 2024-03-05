using System.Net;

namespace ApiRest.ViewModels
{
    public class ErrorResponseApi
    {
        public HttpStatusCode StatusCode { get; set; }
        public string? ErrorMessage { get; set; }

        public ErrorResponseApi(HttpStatusCode statusCode, string errorMessage)
        {
            StatusCode = statusCode;
            ErrorMessage = errorMessage;
        }

        public ErrorResponseApi()
        {
        }
    }
}
