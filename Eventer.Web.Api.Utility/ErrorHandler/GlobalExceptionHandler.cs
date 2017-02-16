using System.Net;
using System.Net.Http;
using System.Web.Http.ExceptionHandling;
using System.Web.Http.Results;
using Eventer.Utility.CustomException;

namespace Eventer.Web.Api.Utility.ErrorHandler
{
    public class GlobalExceptionHandler : ExceptionHandler
    {
        public override void Handle(ExceptionHandlerContext context)
        {
            if (context.Exception is NotFoundException)
            {
                string errorMessage = string.Format($"Resource not found. {context.Exception.Message}");
                var response = context.Request.CreateResponse(HttpStatusCode.NotFound,
                    new
                    {
                        Message = errorMessage
                    });

                response.Headers.Add("NotFound", errorMessage);
                context.Result = new ResponseMessageResult(response);
            }
            else if (context.Exception.InnerException is NotFoundException)
            {
                string errorMessage = string.Format($"Resource not found. {context.Exception.InnerException.Message}");
                var response = context.Request.CreateResponse(HttpStatusCode.NotFound,
                    new
                    {
                        Message = errorMessage
                    });

                response.Headers.Add("NotFound", errorMessage);
                context.Result = new ResponseMessageResult(response);
            }
            else if (context.Exception is BadRequestException)
            {
                string errorMessage = string.Format($"Bad request. {context.Exception.Message}");
                var response = context.Request.CreateResponse(HttpStatusCode.BadRequest,
                    new
                    {
                        Message = errorMessage
                    });

                response.Headers.Add("BadRequest", errorMessage);
                context.Result = new ResponseMessageResult(response);
            }
            else if (context.Exception.InnerException is BadRequestException)
            {
                string errorMessage = string.Format($"Bad request. {context.Exception.InnerException.Message}");
                var response = context.Request.CreateResponse(HttpStatusCode.BadRequest,
                    new
                    {
                        Message = errorMessage
                    });

                response.Headers.Add("BadRequest", errorMessage);
                context.Result = new ResponseMessageResult(response);
            }
            else
            {
                const string errorMessage =
                    "Unexpected error occured. Please contact Administrator michalglowaczewski@gmail.com.";
                var response = context.Request.CreateResponse(HttpStatusCode.InternalServerError,
                    new
                    {
                        Message = errorMessage
                    });

                response.Headers.Add("InternalServerError", errorMessage);
                context.Result = new ResponseMessageResult(response);
            }
        }
    }
}