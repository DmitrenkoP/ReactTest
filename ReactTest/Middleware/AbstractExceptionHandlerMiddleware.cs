using System;
using System.Net;
using System.Reflection;
using ReactTest.Database;

namespace ReactTest.Middleware
{
    public abstract class AbstractExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public abstract (HttpStatusCode code, string message) GetResponse(Exception exception, HttpRequest request, IJournalRepository repository);

        public AbstractExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IJournalRepository repository)
        {
            try
            {
                await _next(context);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var (status, message) = GetResponse(exception, context.Request, repository);
                response.StatusCode = (int)status;
                await response.WriteAsync(message);
            }
        }
    }
}
