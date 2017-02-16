using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eventer.Domain.Entity.EventerEntity;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.Dto.Event;
using Eventer.Model.QueryString.Event;
using Eventer.Repository.UoW;
using Ninject;

namespace Eventer.Utility.EventQueryStringPatternMatch
{
    public class PatternMatching : IPatternMatching
    {
        private static IUnitOfWork _unitOfWork;
        private static IModelFactory _modelFactory;

        public List<PatternMatch> PatternMatchQueryStringEventAsync { get; } =
            new List<PatternMatch>
            {
                new PatternMatch(Pattern1, Match1Async),
                new PatternMatch(Pattern2, Match2Async),
                new PatternMatch(Pattern3, Match3Async),
                new PatternMatch(Pattern4, Match4Async),
                new PatternMatch(Pattern5, Match5Async),
                new PatternMatch(Pattern6, Match6Async),
                new PatternMatch(Pattern7, Match7Async),
                new PatternMatch(Pattern8, Match8Async),
                new PatternMatch(Pattern9, Match9Async),
                new PatternMatch(Pattern10, Match10Async),
                new PatternMatch(Pattern11, Match11Async),
                new PatternMatch(Pattern12, Match12Async),
                new PatternMatch(Pattern13, Match13Async),
                new PatternMatch(Pattern14, Match14Async),
                new PatternMatch(Pattern15, Match15Async),
                new PatternMatch(Pattern16, Match16Async),
                new PatternMatch(Pattern17, Match17Async),
                new PatternMatch(Pattern18, Match18Async),
                new PatternMatch(Pattern19, Match19Async),
                new PatternMatch(Pattern20, Match20Async),
                new PatternMatch(Pattern21, Match21Async),
                new PatternMatch(Pattern22, Match22Async),
                new PatternMatch(Pattern23, Match23Async),
                new PatternMatch(Pattern24, Match24Async),
                new PatternMatch(Pattern25, Match25Async),
                new PatternMatch(Pattern26, Match26Async),
                new PatternMatch(Pattern27, Match27Async),
                new PatternMatch(Pattern28, Match28Async),
                new PatternMatch(Pattern29, Match29Async),
                new PatternMatch(Pattern30, Match30Async),
                new PatternMatch(Pattern31, Match31Async),
                new PatternMatch(Pattern32, Match32Async),
                new PatternMatch(Pattern33, Match33Async),
                new PatternMatch(Pattern34, Match34Async),
                new PatternMatch(Pattern35, Match35Async),
                new PatternMatch(Pattern36, Match36Async),
                new PatternMatch(Pattern37, Match37Async),
                new PatternMatch(Pattern38, Match38Async),
                new PatternMatch(Pattern39, Match39Async),
                new PatternMatch(Pattern40, Match40Async),
                new PatternMatch(Pattern41, Match41Async),
                new PatternMatch(Pattern42, Match42Async),
                new PatternMatch(Pattern43, Match43Async),
                new PatternMatch(Pattern44, Match44Async),
                new PatternMatch(Pattern45, Match45Async),
                new PatternMatch(Pattern46, Match46Async),
                new PatternMatch(Pattern47, Match47Async),
                new PatternMatch(Pattern48, Match48Async),
                new PatternMatch(Pattern49, Match49Async),
                new PatternMatch(Pattern50, Match50Async),
                new PatternMatch(Pattern51, Match51Async),
                new PatternMatch(Pattern52, Match52Async),
                new PatternMatch(Pattern53, Match53Async),
                new PatternMatch(Pattern54, Match54Async)
            };

        public PatternMatching(IUnitOfWork unitOfWork, [Named("EventFactory")] IModelFactory modelFactory)
        {
            _unitOfWork = unitOfWork;
            _modelFactory = modelFactory;
        }

        public async Task<ICollection<EventDto>> PatternMatchingQueryStringEventAsync(EventQueryModel queryModel)
        {
            var match = PatternMatchQueryStringEventAsync.FirstOrDefault(d => d.CheckPatternQueryStringEvent(queryModel));
            if (match != null)
            {
                var result = await match.UseMatchQueryStringEventAsync(queryModel);
                return result.ToList();
            }

            var skip = int.Parse(queryModel.Page);
            var take = int.Parse(queryModel.PageSize);
            var skipAmount = take * (skip - 1);
            var allEvents = await _unitOfWork.EventRepository.GetAllAsync(x => x.OrderBy(q => q.EventDate), skipAmount, take);
            return allEvents.Select(_modelFactory.GetModel<EventDto>).ToList();
        }

        private static async Task<IEnumerable<EventDto>> FindAllAsync(Expression<Func<Event, bool>> filter, string page, string pageSize)
        {
            var skip = int.Parse(page);
            var take = int.Parse(pageSize);
            var skipAmount = take * (skip - 1);
            var events = await _unitOfWork.EventRepository.FindAllAsync(filter, x => x.OrderBy(q => q.EventDate), skipAmount, take);

            return events.Select(_modelFactory.GetModel<EventDto>);
        }

        private static bool Pattern1(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.EventName != null && queryModel.CategoryId != null &&
                queryModel.DateFrom != null && queryModel.DateTo != null;

        private static bool Pattern2(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.EventName != null && queryModel.CategoryName != null &&
               queryModel.DateFrom != null && queryModel.DateTo != null;

        private static bool Pattern3(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.EventName != null && queryModel.DateFrom != null && queryModel.DateTo != null;

        private static bool Pattern4(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.DateFrom != null && queryModel.DateTo != null;

        private static bool Pattern5(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.EventName != null && queryModel.CategoryId != null && queryModel.DateFrom != null;

        private static bool Pattern6(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.EventName != null && queryModel.CategoryName != null && queryModel.DateFrom != null;

        private static bool Pattern7(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.EventName != null && queryModel.DateFrom != null;

        private static bool Pattern8(EventQueryModel queryModel) => queryModel.CityId != null && queryModel.DateFrom != null;

        private static bool Pattern9(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.EventName != null && queryModel.CategoryId != null && queryModel.DateTo != null;

        private static bool Pattern10(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.EventName != null && queryModel.CategoryName != null && queryModel.DateTo != null;

        private static bool Pattern11(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.EventName != null && queryModel.DateTo != null;

        private static bool Pattern12(EventQueryModel queryModel) => queryModel.CityId != null && queryModel.DateTo != null;

        private static bool Pattern13(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.EventName != null && queryModel.CategoryId != null && queryModel.Date != null;

        private static bool Pattern14(EventQueryModel queryModel)
            => queryModel.CityId != null && queryModel.EventName != null && queryModel.CategoryName != null && queryModel.Date != null;

        private static bool Pattern15(EventQueryModel queryModel) => queryModel.CityId != null && queryModel.EventName != null && queryModel.Date != null;

        private static bool Pattern16(EventQueryModel queryModel) => queryModel.CityId != null && queryModel.Date != null;

        private static bool Pattern17(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.CategoryId != null && queryModel.DateFrom != null &&
               queryModel.DateTo != null;

        private static bool Pattern18(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.CategoryName != null && queryModel.DateFrom != null &&
               queryModel.DateTo != null;

        private static bool Pattern19(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.DateFrom != null && queryModel.DateTo != null;

        private static bool Pattern20(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.DateFrom != null && queryModel.DateTo != null;

        private static bool Pattern21(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.CategoryId != null && queryModel.DateFrom != null;

        private static bool Pattern22(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.CategoryName != null && queryModel.DateFrom != null;

        private static bool Pattern23(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.DateFrom != null;

        private static bool Pattern24(EventQueryModel queryModel) => queryModel.CityName != null && queryModel.DateFrom != null;

        private static bool Pattern25(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.CategoryId != null && queryModel.DateTo != null;

        private static bool Pattern26(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.CategoryName != null && queryModel.DateTo != null;

        private static bool Pattern27(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.DateTo != null;

        private static bool Pattern28(EventQueryModel queryModel) => queryModel.CityName != null && queryModel.DateTo != null;

        private static bool Pattern29(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.CategoryId != null && queryModel.Date != null;

        private static bool Pattern30(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.CategoryName != null && queryModel.Date != null;

        private static bool Pattern31(EventQueryModel queryModel)
            => queryModel.CityName != null && queryModel.EventName != null && queryModel.Date != null;

        private static bool Pattern32(EventQueryModel queryModel) => queryModel.CityName != null && queryModel.Date != null;

        private static bool Pattern33(EventQueryModel queryModel) => queryModel.CityId != null && queryModel.CategoryId != null;

        private static bool Pattern34(EventQueryModel queryModel) => queryModel.CityId != null && queryModel.CategoryName != null;

        private static bool Pattern35(EventQueryModel queryModel) => queryModel.CityName != null && queryModel.CategoryId != null;

        private static bool Pattern36(EventQueryModel queryModel) => queryModel.CityName != null && queryModel.CategoryName != null;

        private static bool Pattern37(EventQueryModel queryModel) => queryModel.CityName != null && queryModel.EventName != null;

        private static bool Pattern38(EventQueryModel queryModel)
            => queryModel.CategoryId != null && queryModel.DateFrom != null && queryModel.DateTo != null;

        private static bool Pattern39(EventQueryModel queryModel) => queryModel.CategoryId != null && queryModel.DateFrom != null;

        private static bool Pattern40(EventQueryModel queryModel) => queryModel.CategoryId != null && queryModel.DateTo != null;

        private static bool Pattern41(EventQueryModel queryModel) => queryModel.CategoryId != null && queryModel.Date != null;

        private static bool Pattern42(EventQueryModel queryModel)
            => queryModel.CategoryName != null && queryModel.DateFrom != null && queryModel.DateTo != null;

        private static bool Pattern43(EventQueryModel queryModel) => queryModel.CategoryName != null && queryModel.DateFrom != null;

        private static bool Pattern44(EventQueryModel queryModel) => queryModel.CategoryName != null && queryModel.DateTo != null;

        private static bool Pattern45(EventQueryModel queryModel) => queryModel.CategoryName != null && queryModel.Date != null;

        private static bool Pattern46(EventQueryModel queryModel) => queryModel.DateFrom != null && queryModel.DateTo != null;

        private static bool Pattern47(EventQueryModel queryModel) => queryModel.DateFrom != null;

        private static bool Pattern48(EventQueryModel queryModel) => queryModel.DateTo != null;

        private static bool Pattern49(EventQueryModel queryModel) => queryModel.Date != null;

        private static bool Pattern50(EventQueryModel queryModel) => queryModel.CategoryId != null;

        private static bool Pattern51(EventQueryModel queryModel) => queryModel.CategoryName != null;

        private static bool Pattern52(EventQueryModel queryModel) => queryModel.EventName != null;

        private static bool Pattern53(EventQueryModel queryModel) => queryModel.CityId != null;

        private static bool Pattern54(EventQueryModel queryModel) => queryModel.CityName != null;

        



        private static async Task<IEnumerable<EventDto>> Match1Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Equals(queryModel.EventName.ToUpper()) && e.Categories.Any(x=>x.Id==catId) &&
                                e.EventDate >= dateFrom && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match2Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper())
                                      && e.Categories.Any(x=>x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) &&
                                      e.EventDate >= dateFrom && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match3Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.EventDate >= dateFrom && 
            e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match4Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.CityId == cityId && e.EventDate >= dateFrom && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match5Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.Categories.Any(x => x.Id == catId) && 
            e.EventDate >= dateFrom, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match6Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper())
                                      && e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) &&
                                      e.EventDate >= dateFrom, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match7Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.EventDate >= dateFrom, 
                queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match8Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await FindAllAsync(e => e.CityId == cityId && e.EventDate >= dateFrom, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match9Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var catId = int.Parse(queryModel.CategoryId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.Categories.Any(x => x.Id == catId) && 
            e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match10Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper())
                                      && e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) &&
                                      e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match11Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.EventDate <= dateTo, 
                queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match12Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.CityId == cityId && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match13Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var catId = int.Parse(queryModel.CategoryId);
            var date = DateTime.Parse(queryModel.Date);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.Categories.Any(x => x.Id == catId) && 
            e.EventDate == date, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match14Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var date = DateTime.Parse(queryModel.Date);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) 
            && e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) &&
                                      e.EventDate == date, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match15Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var date = DateTime.Parse(queryModel.Date);
            return await FindAllAsync(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.EventDate == date, 
                queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match16Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var date = DateTime.Parse(queryModel.Date);
            return await FindAllAsync(e => e.CityId == cityId && e.EventDate == date, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match17Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.Id == catId) && e.EventDate >= dateFrom && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match18Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate >= dateFrom &&
                                      e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match19Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.EventDate >= dateFrom && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match20Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventDate >= dateFrom && e.EventDate <= dateTo, 
                queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match21Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.Id == catId) && e.EventDate >= dateFrom, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match22Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate >= dateFrom, 
                                      queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match23Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.EventDate >= dateFrom, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match24Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventDate >= dateFrom, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match25Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.Id == catId) && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match26Async(EventQueryModel queryModel)
        {
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate <= dateTo, 
                                      queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match27Async(EventQueryModel queryModel)
        {
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match28Async(EventQueryModel queryModel)
        {
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match29Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var date = DateTime.Parse(queryModel.Date);//.ToString("yyyy-M-d dddd");
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.Id == catId) && e.EventDate == date, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match30Async(EventQueryModel queryModel)
        {
            var date = DateTime.Parse(queryModel.Date);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate == date, 
                                      queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match31Async(EventQueryModel queryModel)
        {
            var date = DateTime.Parse(queryModel.Date);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && 
            e.EventDate == date, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match32Async(EventQueryModel queryModel)
        {
            var date = DateTime.Parse(queryModel.Date);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventDate == date, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match33Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var catId = int.Parse(queryModel.CategoryId);
            return await FindAllAsync(e => e.CityId == cityId && e.Categories.Any(x => x.Id == catId), queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match34Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            return await FindAllAsync(e => e.CityId == cityId && e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())), 
                queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match35Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            return await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.Categories.Any(x => x.Id == catId), queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match36Async(EventQueryModel queryModel)
            => await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && 
            e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())), queryModel.Page, queryModel.PageSize);

        private static async Task<IEnumerable<EventDto>> Match37Async(EventQueryModel queryModel)
            => await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.Contains(queryModel.EventName),
                queryModel.Page, queryModel.PageSize);

        private static async Task<IEnumerable<EventDto>> Match38Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.Categories.Any(x => x.Id == catId) && e.EventDate >= dateFrom && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match39Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await FindAllAsync(e => e.Categories.Any(x => x.Id == catId) && e.EventDate >= dateFrom, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match40Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.Categories.Any(x => x.Id == catId) && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match41Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var date = DateTime.Parse(queryModel.Date);
            return await FindAllAsync(e => e.Categories.Any(x => x.Id == catId) && e.EventDate == date, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match42Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && 
            e.EventDate >= dateFrom && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match43Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await FindAllAsync(e => e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate >= dateFrom, 
                queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match44Async(EventQueryModel queryModel)
        {
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate <= dateTo, 
                queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match45Async(EventQueryModel queryModel)
        {
            var date = DateTime.Parse(queryModel.Date);
            return await FindAllAsync(e => e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate == date, 
                queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match46Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.EventDate >= dateFrom && e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match47Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await FindAllAsync(e => e.EventDate >= dateFrom, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match48Async(EventQueryModel queryModel)
        {
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await FindAllAsync(e => e.EventDate <= dateTo, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match49Async(EventQueryModel queryModel)
        {
            var date = DateTime.Parse(queryModel.Date);
            return await FindAllAsync(e => e.EventDate == date, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match50Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            return await FindAllAsync(e => e.Categories.Any(x => x.Id == catId), queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match51Async(EventQueryModel queryModel)
            => await FindAllAsync(e => e.Categories.Any(x=>x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())), queryModel.Page, queryModel.PageSize);

        private static async Task<IEnumerable<EventDto>> Match52Async(EventQueryModel queryModel)
            => await FindAllAsync(e => e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()), queryModel.Page, queryModel.PageSize);

        private static async Task<IEnumerable<EventDto>> Match53Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            return await FindAllAsync(e => e.CityId == cityId, queryModel.Page, queryModel.PageSize);
        }

        private static async Task<IEnumerable<EventDto>> Match54Async(EventQueryModel queryModel)
            => await FindAllAsync(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()), queryModel.Page, queryModel.PageSize);
    }
}