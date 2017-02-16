using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Eventer.Domain.Entity.Common;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.Dto.City;
using Eventer.Model.Dto.Common;
using Eventer.Model.Dto.State;

namespace Eventer.Mapper.ModelFacotry.State
{
    public class StateFactory : ModelFactory
    {
        public StateFactory(HttpRequestMessage request) : base(request)
        {
        }

        public override TDto GetModel<TDto>(EntityBase domainEntity)
        {
            if (TypesEqual<TDto, StateDto>())
            {
                var stateEntity = (Domain.Entity.EventerEntity.State)domainEntity;

                var model = new StateDto
                {
                    Id = stateEntity.Id,
                    StateName = stateEntity.StateName
                };

                if (stateEntity.Cities == null)
                {
                    AddLinks(model, "StateRoute", "state");
                    model.Links.Add(new Link
                    {
                        Rel = "self by stateName",
                        Href = Url.Link("StateByNameRoute", new { stateName = model.StateName }),
                        Method = "GET"
                    });

                    return model as TDto;
                }

                var cities = stateEntity.Cities.Select(c=> new CityForStateDto
                {
                    Id = c.Id,
                    CityName = c.CityName,
                    Links = new List<Link>
                    {
                        new Link
                        {
                            Rel = "self",
                            Href = Url.Link("CityRoute", new {id = c.Id}),
                            Method = "GET"
                        },
                        new Link
                        {
                            Rel = "put city - Administrators only",
                            Href = Url.Link("CityRoute", new {id = c.Id}),
                            Method = "PUT"
                        },
                        new Link
                        {
                            Rel = "delete city - Administrators only",
                            Href = Url.Link("CityRoute", new {id = c.Id}),
                            Method = "DELETE"
                        },
                        new Link
                        {
                            Rel = "self by cityName",
                            Href = Url.Link("CityByNameRoute", new {cityName = c.CityName}),
                            Method = "GET"
                        }
                    }
                }).ToList();
                model.Cities = cities;
                
                AddLinks(model, "StateRoute", "state");
                model.Links.Add(new Link
                {
                    Rel = "self by stateName",
                    Href = Url.Link("StateByNameRoute", new { stateName = model.StateName }),
                    Method = "GET"
                });

                return model as TDto;
            }
            if (TypesEqual<TDto, StateForCityDto>())
            {
                var stateEntity = (Domain.Entity.EventerEntity.State)domainEntity;

                var model = new StateForCityDto
                {
                    Id = stateEntity.Id,
                    StateName = stateEntity.StateName
                };

                AddLinks(model, "StateRoute", "state");
                model.Links.Add(new Link
                {
                    Rel = "self by stateName",
                    Href = Url.Link("StateRoute", new { stateName = model.StateName }),
                    Method = "GET"
                });

                return model as TDto;
            }
            return null;
        }

        public override TDomainEntity GetModel<TDomainEntity>(DtoBase dtoModel)
        {
            if (TypesEqual<StatePostDto>(dtoModel))
            {
                var stateDto = (StatePostDto)dtoModel;
                return new Domain.Entity.EventerEntity.State
                {
                    StateName = stateDto.StateName
                } as TDomainEntity;
            }
            if (TypesEqual<StateDto>(dtoModel))
            {
                var stateDto = (StateDto)dtoModel;
                return new Domain.Entity.EventerEntity.State
                {
                    Id = stateDto.Id,
                    StateName = stateDto.StateName
                } as TDomainEntity;
            }
            if (TypesEqual<StatePutDto>(dtoModel))
            {
                var stateDto = (StatePutDto)dtoModel;
                return new Domain.Entity.EventerEntity.State
                {
                    Id = stateDto.Id,
                    StateName = stateDto.StateName
                } as TDomainEntity;
            }
            return null;
        }
    }
}