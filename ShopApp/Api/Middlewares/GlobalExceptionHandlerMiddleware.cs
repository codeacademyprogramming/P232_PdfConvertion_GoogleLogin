using Api.Common;
using Newtonsoft.Json;
using Service.Exceptions;
using System.Net;

namespace Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {

            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                RestExceptionResponse responseModel = new RestExceptionResponse();
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case RestException exp:
                        response.StatusCode = (int)exp.Code;

                        responseModel.Errors = exp.ModelErrors;
                        break;
                    default:
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        responseModel.Errors = null;
                        responseModel.Message = "Internal server error!";
                        break;
                }

                var result = JsonConvert.SerializeObject(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
