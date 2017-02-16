using System;
using Eventer.Domain.Entity.Common;

namespace Eventer.Domain.Entity.ErrorLog
{
    public class ErrorLog : CommonEntity<long>
    {
        public DateTime ErrorDateTime { get; set; }
        public string ErrorLevel { get; set; }
        public string UserName { get; set; }
        public string ErrorMessage { get; set; }
        public string InnerErrorMessage { get; set; }
        public string StackTrace { get; set; }
    }
}