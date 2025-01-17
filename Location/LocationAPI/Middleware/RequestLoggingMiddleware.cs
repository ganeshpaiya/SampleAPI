using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LocationAPI.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger<RequestLoggingMiddleware> logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next(context);
            }
            catch (System.Exception ex)
            {

                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception ex)
        {
            
            logger.LogError(
                   "Request {method} {url} => {statusCode} =>{ErrorMassage}",
                   context.Request?.Method,
                   context.Request?.Path.Value,
                   context.Response?.StatusCode,
                   ex.Message);

            var code = HttpStatusCode.InternalServerError; // 500 if unexpected

            if (context.Response?.StatusCode is (int)HttpStatusCode.NotFound) code = HttpStatusCode.NotFound;
            else if (context.Response?.StatusCode is (int)HttpStatusCode.Unauthorized) code = HttpStatusCode.Unauthorized;
            else if (context.Response?.StatusCode is (int)HttpStatusCode.BadRequest) code = HttpStatusCode.BadRequest;

            var result = JsonConvert.SerializeObject(new { error = ex.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsync(result);
        }
    }
}
