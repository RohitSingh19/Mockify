using System.Net;
using System.Text.Json.Nodes;
using Mockify.API.Helper;
using Newtonsoft;
using Newtonsoft.Json;

namespace Mockify.API.Middlewares
{
    public class GlobalExceptionHandler
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                httpContext.Response.ContentType = "application/json";
                var response = new ApiResponse<JsonObject>
                {
                    Data = null,
                    Message = ex.Message,
                    StatusCode = httpContext.Response.StatusCode,
                    Success = false,
                };

                await httpContext.Response.WriteAsync(JsonConvert.SerializeObject(response));
            }
        }
    }
}