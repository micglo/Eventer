using System;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.ErrorLog
{
    public class ErrorLogDto : CommonDto<long>
    {
        public DateTime ErrorDateTime { get; set; }
        public string ErrorLevel { get; set; }
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
        public string InnerErrorMessage { get; set; }
        public string StackTrace { get; set; }
    }
}