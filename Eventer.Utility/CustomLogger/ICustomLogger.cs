using Eventer.Model.Dto.ApiActivity;

namespace Eventer.Utility.CustomLogger
{
    public interface ICustomLogger
    {
        void Log(string loggerName, string message);
        void Log(string errorMessage, string innerErrorMessage, string stackTrace);
        void Log(ApiActivityLogDto model);
    }
}