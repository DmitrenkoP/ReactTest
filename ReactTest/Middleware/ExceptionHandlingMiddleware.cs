using System;
using System.Net;
using ReactTest.Database;
using ReactTest.Models;
using Newtonsoft.Json;
using ReactTest.Models.OutputModels;

namespace ReactTest.Middleware
{
	public class ExceptionHandlingMiddleware : AbstractExceptionHandlerMiddleware
	{
		public ExceptionHandlingMiddleware(RequestDelegate next)
			:base(next)
		{
		}

        public override (HttpStatusCode code, string message) GetResponse(Exception exception, HttpRequest request, IJournalRepository repository)
        {
            HttpStatusCode code;
            switch (exception)
            {
                case KeyNotFoundException
                    or FileNotFoundException:
                    code = HttpStatusCode.NotFound;
                    break;
                case UnauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    break;
                case ArgumentException
                    or InvalidOperationException:
                    code = HttpStatusCode.BadRequest;
                    break;
                default:
                    code = HttpStatusCode.InternalServerError;
                    break;
            }
           
            var storedException = new StoredException()
            {
                CreatedAt = DateTime.Now,
                Path = request.Path.ToString(),
                Body = request.QueryString.Value,
                StackTrace = exception.StackTrace

            };
            repository.Add(storedException);
            var eventId = storedException.Id;
            var responseModel = new ExceptionResponseModel() { Id = eventId };
            if (exception is SecureException)
            {
                responseModel.Type = "Secure";
                responseModel.Data = new DataItem() { Message = exception.Message };
            }
            else
            {
                responseModel.Type = "Exception";
                responseModel.Data = new DataItem() { Message = $"Internal server error ID = {eventId}" };
            }
            return (code, JsonConvert.SerializeObject(responseModel));
        }
    }
}

