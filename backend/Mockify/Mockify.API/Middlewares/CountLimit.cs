using Microsoft.AspNetCore.Http;
using Mockify.API.Helper;
using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Nodes;

namespace Mockify.API.Middlewares
{
    public class CountLimit
    {
        private readonly RequestDelegate _next;

        public CountLimit(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (context.Request.RouteValues.ContainsKey("limit") && context.Request.RouteValues.TryGetValue("limit", out var limit))
            {
                if (limit != null && int.TryParse(limit.ToString(), out var count))
                {
                    if (count > 100)
                    {                          
                        context.Response.ContentType = "application/json";
                        var response = new ApiResponse<JsonObject>
                        {
                            Data = null,
                            Message = "Invalid request: 'count' parameter cannot exceed 100.",
                            StatusCode = (int)HttpStatusCode.InternalServerError,
                            Success = false,
                        };

                        await context.Response.WriteAsync(JsonConvert.SerializeObject(response));
                        return;
                    }
                }

            }
            await _next(context);
        }
    }
}
