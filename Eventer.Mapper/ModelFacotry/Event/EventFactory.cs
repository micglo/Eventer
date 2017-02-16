using System.Linq;
using System.Net.Http;
using Eventer.Domain.Entity.Common;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.Dto.Category;
using Eventer.Model.Dto.City;
using Eventer.Model.Dto.Common;
using Eventer.Model.Dto.Event;
using Ninject;

namespace Eventer.Mapper.ModelFacotry.Event
{
    public class EventFactory : ModelFactory
    {
        private readonly IModelFactory _cityEventFactory;
        private readonly IModelFactory _categoryFactory;

        public EventFactory(HttpRequestMessage request, [Named("CityFactory")]IModelFactory cityEventFactory, [Named("CategoryFactory")]IModelFactory categoryFactory) : base(request)
        {
            _cityEventFactory = cityEventFactory;
            _categoryFactory = categoryFactory;
        }

        public override TDto GetModel<TDto>(EntityBase domainEntity)
        {
            if (TypesEqual<TDto, EventDto>())
            {
                var eventEntity = (Domain.Entity.EventerEntity.Event)domainEntity;

                var city = _cityEventFactory.GetModel<CityDto>(eventEntity.City);
                var categories = eventEntity.Categories.Select(_categoryFactory.GetModel<CategoryDto>).ToList();


                var model = new EventDto
                {
                    Id = eventEntity.Id,
                    EventName = eventEntity.EventName,
                    EventDate = eventEntity.EventDate,
                    EventLocalization = eventEntity.EventLocalization,
                    EventUrl = eventEntity.EventUrl,
                    EventImage = eventEntity.EventImage,
                    EventDescription = eventEntity.EventDescription,
                    City = city,
                    Categories = categories
                };

                AddLinks(model, "EventRoute", "event");

                return model as TDto;
            }
            return null;
        }

        public override TDomainEntity GetModel<TDomainEntity>(DtoBase dtoModel)
        {
            if (TypesEqual<EventPostDto>(dtoModel))
            {
                var eventDto = (EventPostDto)dtoModel;
                
                return new Domain.Entity.EventerEntity.Event
                {
                    EventName = eventDto.EventName,
                    EventDate = eventDto.EventDate,
                    EventLocalization = eventDto.EventLocalization,
                    EventUrl = eventDto.EventUrl,
                    EventImage = eventDto.EventImage,
                    EventDescription = eventDto.EventDescription,
                    CityId = eventDto.CityId
                } as TDomainEntity;
            }
            if (TypesEqual<EventPutDto>(dtoModel))
            {
                var eventDto = (EventPutDto)dtoModel;

                return new Domain.Entity.EventerEntity.Event
                {
                    Id = eventDto.Id,
                    EventName = eventDto.EventName,
                    EventDate = eventDto.EventDate,
                    EventLocalization = eventDto.EventLocalization,
                    EventUrl = eventDto.EventUrl,
                    EventImage = eventDto.EventImage,
                    EventDescription = eventDto.EventDescription,
                    CityId = eventDto.CityId
                } as TDomainEntity;
            }
            if (TypesEqual<EventDto>(dtoModel))
            {
                var eventDto = (EventDto)dtoModel;

                return new Domain.Entity.EventerEntity.Event
                {
                    Id = eventDto.Id,
                    EventName = eventDto.EventName,
                    EventDate = eventDto.EventDate,
                    EventLocalization = eventDto.EventLocalization,
                    EventUrl = eventDto.EventUrl,
                    EventImage = eventDto.EventImage,
                    EventDescription = eventDto.EventDescription,
                    CityId = eventDto.City.Id
                } as TDomainEntity;
            }
            return null;
        }
    }
}