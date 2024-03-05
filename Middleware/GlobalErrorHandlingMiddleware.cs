using ApiRest.ViewModels;
using System.Net;
using System.Text.Json;

namespace ApiRest.Middleware
{
    public class GlobalErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        private static Dictionary<Type, Func<Exception, ErrorResponseApi>> exceptionHandlers = new Dictionary<Type, Func<Exception, ErrorResponseApi>>
        {
            { typeof(NotFoundException), HandleNotFoundException}
        };

        public GlobalErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionsAsync(context, ex);
            }

        }
        private static Task HandleExceptionsAsync(HttpContext context, Exception ex)
        {
            Type expetionType = ex.GetType();
            ErrorResponseApi errorResponseApi = new();
            if (exceptionHandlers.TryGetValue(expetionType, out var handler))
            {
                errorResponseApi = handler(ex);
            }
            else
            {
                errorResponseApi = HandleDefaultException();
            }
            string jsonResponseError = JsonSerializer.Serialize(errorResponseApi);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)errorResponseApi.StatusCode;
            return context.Response.WriteAsync(jsonResponseError);
        }

        private static ErrorResponseApi HandleNotFoundException(Exception ex)
        {
            return new ErrorResponseApi(HttpStatusCode.NotFound, ex.Message);
        }

        private static ErrorResponseApi HandleDefaultException()
        {
            return new ErrorResponseApi(HttpStatusCode.InternalServerError, "Falha na requisição");
        }
    }
}
