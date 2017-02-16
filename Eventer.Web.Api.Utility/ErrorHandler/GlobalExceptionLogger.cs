using System.Web.Http.ExceptionHandling;
using Eventer.Utility.CustomLogger;

namespace Eventer.Web.Api.Utility.ErrorHandler
{
    public class GlobalExceptionLogger : ExceptionLogger
    {
        public override void Log(ExceptionLoggerContext context)
        {
            var innerException = string.Empty;
            if (context.Exception.InnerException?.InnerException?.InnerException != null)
                innerException = context.Exception.InnerException.InnerException.InnerException.Message;

            if (context.Exception.InnerException?.InnerException != null)
                innerException = context.Exception.InnerException.InnerException.Message;

            if (context.Exception.InnerException != null)
                innerException = context.Exception.InnerException.Message;

            ICustomLogger logger = new CustomLogger();
            logger.Log(context.Exception.Message, innerException, context.Exception.StackTrace);
        }
    }
}