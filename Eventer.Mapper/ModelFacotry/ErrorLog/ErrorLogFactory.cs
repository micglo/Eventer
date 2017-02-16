using System.Net.Http;
using Eventer.Domain.Entity.Common;
using Eventer.Model.Dto.Common;
using Eventer.Model.Dto.ErrorLog;

namespace Eventer.Mapper.ModelFacotry.ErrorLog
{
    public class ErrorLogFactory : Common.ModelFactory
    {
        public ErrorLogFactory(HttpRequestMessage request) : base(request)
        {
        }

        public override TDto GetModel<TDto>(EntityBase domainEntity)
        {
            if (TypesEqual<TDto, ErrorLogDto>())
            {
                var logEntity = (Domain.Entity.ErrorLog.ErrorLog)domainEntity;

                var model = new ErrorLogDto
                {
                    Id = logEntity.Id,
                    UserName = logEntity.UserName,
                    ErrorDateTime = logEntity.ErrorDateTime,
                    ErrorLevel = logEntity.ErrorLevel,
                    ErrorMessage = logEntity.ErrorMessage,
                    InnerErrorMessage = logEntity.InnerErrorMessage,
                    StackTrace = logEntity.StackTrace
                };

                AddLinks(model, "ErrorLogRoute", "log");
                model.Links.Add(new Link
                {
                    Rel = "User error logs - Administrators only",
                    Href = Url.Link("UserErrorLogsRoute", new { userName = model.UserName }),
                    Method = "GET"
                });

                return model as TDto;
            }
            return null;
        }

        public override TDomainEntity GetModel<TDomainEntity>(DtoBase dtoModel)
        {
            if (TypesEqual<TDomainEntity, Domain.Entity.ErrorLog.ErrorLog>())
            {
                var logDto = (ErrorLogDto)dtoModel;
                return new Domain.Entity.ErrorLog.ErrorLog
                {
                    Id = logDto.Id,
                    UserName = logDto.UserName,
                    ErrorDateTime = logDto.ErrorDateTime,
                    ErrorLevel = logDto.ErrorLevel,
                    ErrorMessage = logDto.ErrorMessage,
                    InnerErrorMessage = logDto.InnerErrorMessage,
                    StackTrace = logDto.StackTrace
                } as TDomainEntity;
            }
            return null;
        }
    }
}