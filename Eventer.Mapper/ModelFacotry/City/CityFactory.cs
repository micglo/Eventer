using System.Net.Http;
using Eventer.Domain.Entity.Common;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.Dto.City;
using Eventer.Model.Dto.Common;
using Eventer.Model.Dto.State;
using Ninject;

namespace Eventer.Mapper.ModelFacotry.City
{
    public class CityFactory : ModelFactory
    {
        private readonly IModelFactory _stateFactory;

        public CityFactory(HttpRequestMessage request, [Named("StateFactory")]IModelFactory stateFactory) : base(request)
        {
            _stateFactory = stateFactory;
        }

        public override TDto GetModel<TDto>(EntityBase domainEntity)
        {
            if (TypesEqual<TDto, CityDto>())
            {
                var cityEntity = (Domain.Entity.EventerEntity.City)domainEntity;
                var state = _stateFactory.GetModel<StateForCityDto>(cityEntity.State);


                var model = new CityDto
                {
                    Id = cityEntity.Id,
                    CityName = cityEntity.CityName,
                    State = state
                };

                AddLinks(model, "CityRoute", "city");
                model.Links.Add(new Link
                {
                    Rel = "self by cityName",
                    Href = Url.Link("CityByNameRoute", new { cityName = model.CityName }),
                    Method = "GET"
                });
                return model as TDto;
            }
            return null;
        }

        public override TDomainEntity GetModel<TDomainEntity>(DtoBase dtoModel)
        {
            if (TypesEqual<CityPostDto>(dtoModel))
            {
                var cityDto = (CityPostDto)dtoModel;
                return new Domain.Entity.EventerEntity.City
                {
                    CityName = cityDto.CityName,
                    StateId = cityDto.StateId
                } as TDomainEntity;
            }
            if (TypesEqual<CityDto>(dtoModel))
            {
                var cityDto = (CityDto)dtoModel;
                return new Domain.Entity.EventerEntity.City
                {
                    Id = cityDto.Id,
                    CityName = cityDto.CityName,
                    StateId = cityDto.State.Id
                } as TDomainEntity;
            }
            if (TypesEqual<CityPutDto>(dtoModel))
            {
                var cityDto = (CityPutDto)dtoModel;
                return new Domain.Entity.EventerEntity.City
                {
                    Id = cityDto.Id,
                    CityName = cityDto.CityName,
                    StateId = cityDto.StateId
                } as TDomainEntity;
            }
            return null;
        }
    }
}