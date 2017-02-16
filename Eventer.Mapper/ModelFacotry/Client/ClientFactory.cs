using System.Collections.Generic;
using System.Net.Http;
using Eventer.Domain.Entity.Client;
using Eventer.Domain.Entity.Common;
using Eventer.Model.Dto.Client;
using Eventer.Model.Dto.Common;

namespace Eventer.Mapper.ModelFacotry.Client
{
    public class ClientFactory : Common.ModelFactory
    {
        public ClientFactory()
        {

        }

        public ClientFactory(HttpRequestMessage request) : base(request)
        {
        }

        public override TDto GetModel<TDto>(EntityBase domainEntity)
        {
            if (TypesEqual<TDto, ClientDto>())
            {
                var clientEntity = (Domain.Entity.Client.Client)domainEntity;

                var model = new ClientDto
                {
                    Active = clientEntity.Active,
                    ClientSecret = clientEntity.ClientSecret,
                    AllowedOrigin = clientEntity.AllowedOrigin,
                    Username = clientEntity.Username,
                    ApplicationType = clientEntity.ApplicationType,
                    Id = clientEntity.Id,
                    RefreshTokenLifeTime = clientEntity.RefreshTokenLifeTime
                };

                if (RequestMessage != null)
                {
                    AddLinks(model, "ClientRoute", "client", " - Administrators only");
                    model.Links.Add(new Link
                    {
                        Rel = "user clients - Administrators only",
                        Href = Url.Link("UserClientsRoute", new { userName = model.Username }),
                        Method = "GET"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "put client no secret change - Administrators only",
                        Href = Url.Link("PutClientNoSecretRoute", new { id = model.Id }),
                        Method = "PUT"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "Reset client secret - Administrators only",
                        Href = Url.Link("ResetClientSecretRoute", null),
                        Method = "POST"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "my clients",
                        Href = Url.Link("GetMyClientsRoute", null),
                        Method = "GET"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "my client",
                        Href = Url.Link("GetMyClientsRoute", new { id = model.Id }),
                        Method = "GET"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "add new client",
                        Href = Url.Link("AddClientRoute", null),
                        Method = "POST"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "reset my client secret",
                        Href = Url.Link("ResetMyClientSecretRoute", null),
                        Method = "POST"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "edit my client",
                        Href = Url.Link("PutMyClientRoute", new { id = model.Id }),
                        Method = "PUT"
                    });
                }

                return model as TDto;
            }
            if (TypesEqual<TDto, ClientByUserNameDto>())
            {
                var clientEntity = (Domain.Entity.Client.Client)domainEntity;

                var model = new ClientByUserNameDto
                {
                    Active = clientEntity.Active,
                    AllowedOrigin = clientEntity.AllowedOrigin,
                    ApplicationType = clientEntity.ApplicationType.ToString(),
                    Id = clientEntity.Id,
                    RefreshTokenLifeTime = clientEntity.RefreshTokenLifeTime,
                    Links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "my client",
                            Href = Url.Link("GetMyClientsRoute", new { id = clientEntity.Id }),
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "reset my client secret",
                            Href = Url.Link("ResetMyClientSecretRoute", null),
                            Method = "POST"
                        },
                        new Link
                        {
                            Rel = "edit my client",
                            Href = Url.Link("PutMyClientRoute", new { id = clientEntity.Id }),
                            Method = "PUT"
                        },
                        new Link
                        {
                            Rel = "add new client",
                            Href = Url.Link("AddClientRoute", null),
                            Method = "POST"
                        }
                    }
                };

                return model as TDto;
            }
            if (TypesEqual<TDto, ClientNoSecretDto>())
            {
                var clientEntity = (Domain.Entity.Client.Client)domainEntity;

                var model = new ClientNoSecretDto
                {
                    Active = clientEntity.Active,
                    AllowedOrigin = clientEntity.AllowedOrigin,
                    Username = clientEntity.Username,
                    ApplicationType = clientEntity.ApplicationType,
                    Id = clientEntity.Id,
                    RefreshTokenLifeTime = clientEntity.RefreshTokenLifeTime
                };

                if (RequestMessage != null)
                {
                    AddLinks(model, "ClientRoute", "client", " - Administrators only");
                    model.Links.Add(new Link
                    {
                        Rel = "user clients - Administrators only",
                        Href = Url.Link("UserClientsRoute", new { userName = model.Username }),
                        Method = "GET"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "put client no secret change - Administrators only",
                        Href = Url.Link("PutClientNoSecretRoute", new { id = model.Id }),
                        Method = "PUT"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "Reset client secret - Administrators only",
                        Href = Url.Link("ResetClientSecretRoute", null),
                        Method = "POST"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "my clients",
                        Href = Url.Link("GetMyClientsRoute", null),
                        Method = "GET"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "my client",
                        Href = Url.Link("GetMyClientsRoute", new { id = model.Id }),
                        Method = "GET"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "add new client",
                        Href = Url.Link("AddClientRoute", null),
                        Method = "POST"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "reset my client secret",
                        Href = Url.Link("ResetMyClientSecretRoute", null),
                        Method = "POST"
                    });
                    model.Links.Add(new Link
                    {
                        Rel = "edit my client",
                        Href = Url.Link("PutMyClientRoute", new { id = model.Id }),
                        Method = "PUT"
                    });
                }

                return model as TDto;
            }
            return null;
        }

        public override TDomainEntity GetModel<TDomainEntity>(DtoBase dtoModel)
        {
            if (TypesEqual<ClientDto>(dtoModel))
            {
                var clientDto = (ClientDto)dtoModel;
                return new Domain.Entity.Client.Client
                {
                    Active = clientDto.Active,
                    ClientSecret = clientDto.ClientSecret,
                    AllowedOrigin = clientDto.AllowedOrigin,
                    Username = clientDto.Username,
                    ApplicationType = clientDto.ApplicationType,
                    Id = clientDto.Id,
                    RefreshTokenLifeTime = clientDto.RefreshTokenLifeTime
                } as TDomainEntity;
            }
            if (TypesEqual<ClientNoSecretDto>(dtoModel))
            {
                var clientDto = (ClientNoSecretDto)dtoModel;
                return new Domain.Entity.Client.Client
                {
                    Active = clientDto.Active,
                    AllowedOrigin = clientDto.AllowedOrigin,
                    Username = clientDto.Username,
                    ApplicationType = clientDto.ApplicationType,
                    Id = clientDto.Id,
                    RefreshTokenLifeTime = clientDto.RefreshTokenLifeTime
                } as TDomainEntity;
            }
            if (TypesEqual<ClientPostDto>(dtoModel))
            {
                var clientDto = (ClientPostDto)dtoModel;

                ApplicationTypes applicationType = ApplicationTypes.NativeConfidential;
                if (clientDto.ApplicationType == 0)
                    applicationType = ApplicationTypes.JavaScript;

                return new Domain.Entity.Client.Client
                {
                    Active = clientDto.Active,
                    AllowedOrigin = clientDto.AllowedOrigin,
                    Username = clientDto.Username,
                    ApplicationType = applicationType,
                    RefreshTokenLifeTime = clientDto.RefreshTokenLifeTime
                } as TDomainEntity;
            }
            return null;
        }
    }
}