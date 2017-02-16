using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Eventer.Domain.Entity.EventerEntity;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.Event;
using Eventer.Model.QueryString.Event;
using Eventer.Repository.UoW;
using Eventer.Service.Common;
using Eventer.Service.EventerService.Interface;
using Eventer.Utility.CustomException;
using Eventer.Utility.EventQueryStringPatternMatch;
using Ninject;

namespace Eventer.Service.EventerService
{
    public class EventService : ServiceBase<EventDto, EventPostDto, EventPutDto>, IEventService
    {
        private readonly IPatternMatching _patternMatching;
        private readonly IPatternMatchingCount _patternMatchingCount;
        public EventService(IUnitOfWork unitOfWork, [Named("EventFactory")]IModelFactory modelFactory, IPatternMatching patternMatching, 
            IPatternMatchingCount patternMatchingCount, ICustomException customException, HttpRequestMessage request) 
            : base(unitOfWork, modelFactory, customException, request)
        {
            _patternMatching = patternMatching;
            _patternMatchingCount = patternMatchingCount;
        }

        public override async Task<PagedItems<EventDto>> GetAllAsync(string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var events = await UnitOfWork.EventRepository.GetAllAsync(x => x.OrderBy(y => y.EventDate), skipAmount, intTake);
            var eventsDto = events.Select(ModelFactory.GetModel<EventDto>).ToList();
            var eventsCount = await UnitOfWork.ClientRepository.Count();

            return CreatePagedItems(eventsDto, "EventRoute", intSkip, intTake, eventsCount);
        }

        public override async Task<EventDto> GetByIdAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no event with id = {id}.");

            var @event = await UnitOfWork.EventRepository.FindAsync(id);
            return ModelFactory.GetModel<EventDto>(@event);
        }

        public override async Task<EventDto> AddAsync(EventPostDto dtoModel)
        {
            if (!await UnitOfWork.CityRepository.AnyAsync(x=>x.Id == dtoModel.CityId))
                CustomException.ThrowNotFoundException($"City with id = {dtoModel.CityId} doesn't exist.");

            var eventDomain = ModelFactory.GetModel<Event>(dtoModel);
            eventDomain.Categories = await CheckCategories(dtoModel.Categories);
            var newEntity = await UnitOfWork.EventRepository.AddAsync(eventDomain);
            return await GetByIdAsync(newEntity.Id);
        }

        public override async Task<EventDto> Update(EventPutDto dtoModel)
        {
            if (!await UnitOfWork.CityRepository.AnyAsync(x => x.Id == dtoModel.CityId))
                CustomException.ThrowNotFoundException($"City with id = {dtoModel.CityId} doesn't exist.");

            var eventDomain = ModelFactory.GetModel<Event>(dtoModel);
            eventDomain.Categories = await CheckCategories(dtoModel.Categories);
            await UnitOfWork.EventRepository.Update(eventDomain);

            return await GetByIdAsync(dtoModel.Id);
        }

        public override async Task RemoveAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no event with id = {id}.");

            var eventDomain = await UnitOfWork.EventRepository.FindAsync(id);
            await UnitOfWork.EventRepository.RemoveAsync(eventDomain);
        }

        public async Task<PagedItems<EventDto>> GetEventsByParamAsync(EventQueryModel queryModel)
        {
            var events = await _patternMatching.PatternMatchingQueryStringEventAsync(queryModel);
            var eventsCount = await _patternMatchingCount.PatternMatchingQueryStringEventCount(queryModel);

            return CreatePagedItems(events, eventsCount, queryModel);
        }

        public async Task<ICollection<EventDto>> AddListAsync(EventPostAsListDto eventList)
        {
            var newEventlist = new List<EventDto>();

            foreach (var @event in eventList.Events)
            {
                var eventDomain = ModelFactory.GetModel<Event>(@event);
                eventDomain.Categories = await CheckCategories(@event.Categories);
                var newEntity = await UnitOfWork.EventRepository.AddAsync(eventDomain);
                var newEntityToAdd = ModelFactory.GetModel<EventDto>(newEntity);
                newEventlist.Add(newEntityToAdd);
            }

            return newEventlist;
        }

        public async Task<string> CheckCityExistance(IEnumerable<EventPostDto> events)
        {
            var cityExistance = string.Empty;
            foreach (var @event in events)
            {
                if (!await UnitOfWork.CityRepository.AnyAsync(c=>c.Id == @event.CityId))
                    cityExistance += @event.CityId + " ";
            }

            return cityExistance;
        }


        #region Helpers

        protected override async Task<bool> ExistsAsync(object id)
            => await UnitOfWork.EventRepository.AnyAsync(x => x.Id == (long)id);

        private PagedItems<EventDto> CreatePagedItems(IEnumerable<EventDto> query, long totalNumberOfRecords, EventQueryModel queryModel)
        {
            var page = int.Parse(queryModel.Page);
            var pageSize = int.Parse(queryModel.PageSize);

            var mod = totalNumberOfRecords % pageSize;
            var totalPageCount = totalNumberOfRecords / pageSize + (mod == 0 ? 0 : 1);

            string nextPageUrl;
            if (page == totalPageCount || page > totalPageCount)
                nextPageUrl = null;
            else
            {
                nextPageUrl = Url?.Link("EventRoute", new
                {
                    page = page + 1,
                    queryModel.PageSize,
                    queryModel.CityId,
                    queryModel.CategoryId,
                    queryModel.CategoryName,
                    queryModel.CityName,
                    queryModel.Date,
                    queryModel.DateFrom,
                    queryModel.DateTo,
                    queryModel.EventName
                });
            }

            string prevPageUrl;
            if (page < 2)
            {
                prevPageUrl = null;
            }
            else
            {
                prevPageUrl = Url?.Link("EventRoute", new
                {
                    page = page - 1,
                    queryModel.PageSize,
                    queryModel.CityId,
                    queryModel.CategoryId,
                    queryModel.CategoryName,
                    queryModel.CityName,
                    queryModel.Date,
                    queryModel.DateFrom,
                    queryModel.DateTo,
                    queryModel.EventName
                });
            }

            return new PagedItems<EventDto>
            {
                Items = query,
                PageNumber = page,
                PageSize = pageSize,
                TotalNumberOfPages = totalPageCount,
                TotalNumberOfRecords = totalNumberOfRecords,
                NextPageUrl = nextPageUrl,
                PreviousPageUrl = prevPageUrl
            };
        }

        private async Task<List<Category>> CheckCategories(IEnumerable<string> categoryNames)
        {
            var categoryList = new List<Category>();

            foreach (var catName in categoryNames)
            {
                Category category;
                var categoryExists =
                    await UnitOfWork.CategoryRepository.AnyAsync(c => c.CategoryName.Equals(catName));

                if (categoryExists)
                    category =
                        await UnitOfWork.CategoryRepository.SingleOrDefaultAsync(c => c.CategoryName.Equals(catName));
                else
                    category = await UnitOfWork.CategoryRepository.AddAsync(new Category { CategoryName = catName });

                categoryList.Add(category);
            }

            return categoryList;
        }

        #endregion
    }
}