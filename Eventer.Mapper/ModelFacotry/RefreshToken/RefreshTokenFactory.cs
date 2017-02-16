using System.Collections.Generic;
using System.Net.Http;
using Eventer.Domain.Entity.Common;
using Eventer.Model.Dto.Common;
using Eventer.Model.Dto.RefreshToken;

namespace Eventer.Mapper.ModelFacotry.RefreshToken
{
    public class RefreshTokenFactory : Common.ModelFactory
    {
        public RefreshTokenFactory()
        {

        }

        public RefreshTokenFactory(HttpRequestMessage request) : base(request)
        {
        }

        public override TDto GetModel<TDto>(EntityBase domainEntity)
        {
            if (TypesEqual<TDto, RefreshTokenDto>())
            {
                var refreshTokenEntity = (Domain.Entity.RefreshToken.RefreshToken)domainEntity;

                var model = new RefreshTokenDto
                {
                    Id = refreshTokenEntity.Id,
                    ProtectedTicket = refreshTokenEntity.ProtectedTicket,
                    ClientId = refreshTokenEntity.ClientId,
                    Subject = refreshTokenEntity.Subject,
                    ExpiresUtc = refreshTokenEntity.ExpiresUtc,
                    IssuedUtc = refreshTokenEntity.IssuedUtc
                };

                if (RequestMessage != null)
                {
                    model.Links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "self - Administrators only",
                            Href = Url.Link("RefreshTokenRoute", new {id = model.ClientId}),
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "delete refresh token - Administrators only",
                            Href = Url.Link("RefreshTokenRoute", new {id = model.ClientId}),
                            Method = "DELETE"
                        }
                    };
                }

                return model as TDto;
            }
            return null;
        }

        public override TDomainEntity GetModel<TDomainEntity>(DtoBase dtoModel)
        {
            if (TypesEqual<RefreshTokenDto>(dtoModel))
            {
                var refreshTokenDto = (RefreshTokenDto)dtoModel;
                return new Domain.Entity.RefreshToken.RefreshToken
                {
                    Id = refreshTokenDto.Id,
                    ProtectedTicket = refreshTokenDto.ProtectedTicket,
                    ClientId = refreshTokenDto.ClientId,
                    Subject = refreshTokenDto.Subject,
                    ExpiresUtc = refreshTokenDto.ExpiresUtc,
                    IssuedUtc = refreshTokenDto.IssuedUtc
                } as TDomainEntity;
            }
            return null;
        }
    }
}