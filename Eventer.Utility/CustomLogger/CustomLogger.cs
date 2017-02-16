using Eventer.Model.Dto.ApiActivity;
using NLog;

namespace Eventer.Utility.CustomLogger
{
    public class CustomLogger : ICustomLogger
    {
        private static Logger _logger;
        public void Log(string loggerName, string message)
        {
            _logger = LogManager.GetLogger(loggerName);
            LogEventInfo logInfo = new LogEventInfo { Message = message };
            _logger.Debug(logInfo);
        }

        public void Log(string errorMessage, string innerErrorMessage, string stackTrace)
        {
            _logger = LogManager.GetLogger("error");
            LogEventInfo logInfo = new LogEventInfo();
            logInfo.Properties["ErrorMessage"] = errorMessage;
            logInfo.Properties["InnerErrorMessage"] = innerErrorMessage;
            logInfo.Properties["StackTrace"] = stackTrace;
            _logger.Debug(logInfo);

            //var dbLogger = LogManager.GetLogger("errorDatabaseLog");
            //LogEventInfo dbLogInfo = new LogEventInfo();
            //dbLogInfo.Properties["ErrorMessage"] = errorMessage;
            //dbLogInfo.Properties["InnerErrorMessage"] = innerErrorMessage;
            //dbLogInfo.Properties["StackTrace"] = stackTrace;
            //dbLogger.Debug(dbLogInfo);
        }

        public void Log(ApiActivityLogDto model)
        {
            _logger = LogManager.GetLogger("activity");
            LogEventInfo logInfo = new LogEventInfo();
            logInfo.Properties["User"] = "User: " + model.User;
            logInfo.Properties["UserHostAddress"] = "UserHostAddress: " + model.UserHostAddress;
            logInfo.Properties["RequestContentType"] = "RequestContentType: " + model.RequestContentType;
            logInfo.Properties["RequestContentBody"] = "RequestContentBody: " + model.RequestContentBody;
            logInfo.Properties["RequestUri"] = "RequestUri: " + model.RequestUri;
            logInfo.Properties["RequestMethod"] = "RequestMethod: " + model.RequestMethod;
            logInfo.Properties["RequestRouteTemplate"] = "RequestRouteTemplate: " + model.RequestRouteTemplate;
            logInfo.Properties["RequestHeaders"] = "RequestHeaders: " + model.RequestHeaders;
            logInfo.Properties["RequestTimestamp"] = "RequestTimestamp: " + model.RequestTimestamp;
            logInfo.Properties["ResponseContentType"] = "ResponseContentType: " + model.ResponseContentType;
            logInfo.Properties["ResponseContentBody"] = "ResponseContentBody: " + model.ResponseContentBody;
            logInfo.Properties["ResponseStatusCode"] = "ResponseStatusCode: " + model.ResponseStatusCode;
            logInfo.Properties["ResponseHeaders"] = "ResponseHeaders: " + model.ResponseHeaders;
            logInfo.Properties["ResponseTimestamp"] = "ResponseTimestamp: " + model.ResponseTimestamp;
            _logger.Debug(logInfo);
        }
    }
}