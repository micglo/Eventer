using System.Collections.Generic;
using System.Net.Http;
using Eventer.Domain.Entity.ApiActivity;
using Eventer.Domain.Entity.Common;
using Eventer.Model.Dto.ApiActivity;
using Eventer.Model.Dto.Common;

namespace Eventer.Mapper.ModelFacotry.ApiActivity
{
    public class ApiActivityLogFactory : Common.ModelFactory
    {
        public ApiActivityLogFactory()
        {

        }

        public ApiActivityLogFactory(HttpRequestMessage request) : base(request)
        {
        }

        public override TDto GetModel<TDto>(EntityBase domainEntity)
        {
            if (TypesEqual<TDto, ApiActivityLogDto>())
            {
                var clientEntity = (ApiActivityLog)domainEntity;

                var model = new ApiActivityLogDto
                {
                    Id = clientEntity.Id,
                    RequestContentBody = clientEntity.RequestContentBody,
                    RequestContentType = clientEntity.RequestContentType,
                    RequestHeaders = clientEntity.RequestHeaders,
                    UserHostAddress = clientEntity.UserHostAddress,
                    RequestMethod = clientEntity.RequestMethod,
                    RequestRouteData = clientEntity.RequestRouteData,
                    RequestRouteTemplate = clientEntity.RequestRouteTemplate,
                    RequestTimestamp = clientEntity.RequestTimestamp,
                    RequestUri = clientEntity.RequestUri,
                    ResponseContentBody = clientEntity.ResponseContentBody,
                    ResponseContentType = clientEntity.ResponseContentType,
                    ResponseHeaders = clientEntity.ResponseHeaders,
                    ResponseStatusCode = clientEntity.ResponseStatusCode,
                    ResponseTimestamp = clientEntity.ResponseTimestamp,
                    User = clientEntity.User,
                };

                if (RequestMessage != null)
                {
                    model.Links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "self - Administrators only",
                            Href = Url.Link("ApiActivityLogsRoute", new {id = clientEntity.Id}),
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "delete - Administrators only",
                            Href = Url.Link("ApiActivityLogsRoute", new {id = clientEntity.Id}),
                            Method = "DELETE"
                        },
                        new Link
                        {
                            Rel = "self by username - Administrators only",
                            Href = Url.Link("ApiActivityLogsByUserRoute", new { username = clientEntity.User }),
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "self by host address - Administrators only",
                            Href = Url.Link("ApiActivityLogsByHostRoute", new { HostAddress = clientEntity.UserHostAddress }),
                            Method = "GET"
                        }
                    };
                }

                return model as TDto;
            }
            return null;
        }

        public override TDomainEntity GetModel<TDomainEntity>(DtoBase dtoModel)
        {
            if (TypesEqual<TDomainEntity, ApiActivityLog>())
            {
                var clientDto = (ApiActivityLogDto)dtoModel;
                return new ApiActivityLog
                {
                    Id = clientDto.Id,
                    RequestContentBody = clientDto.RequestContentBody,
                    RequestContentType = clientDto.RequestContentType,
                    RequestHeaders = clientDto.RequestHeaders,
                    UserHostAddress = clientDto.UserHostAddress,
                    RequestMethod = clientDto.RequestMethod,
                    RequestRouteData = clientDto.RequestRouteData,
                    RequestRouteTemplate = clientDto.RequestRouteTemplate,
                    RequestTimestamp = clientDto.RequestTimestamp,
                    RequestUri = clientDto.RequestUri,
                    ResponseContentBody = clientDto.ResponseContentBody,
                    ResponseContentType = clientDto.ResponseContentType,
                    ResponseHeaders = clientDto.ResponseHeaders,
                    ResponseStatusCode = clientDto.ResponseStatusCode,
                    ResponseTimestamp = clientDto.ResponseTimestamp,
                    User = clientDto.User
                } as TDomainEntity;
            }
            return null;
        }
    }
}