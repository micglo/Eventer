using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Eventer.Domain.Entity.EventerEntity;
using Eventer.Model.QueryString.Event;
using Eventer.Repository.UoW;

namespace Eventer.Utility.EventQueryStringPatternMatch
{
    public class PatternMatchingCount : IPatternMatchingCount
    {
        private static IUnitOfWork _unitOfWork;

        public List<PatternMatchCount> PatternMatchQueryStringEventCount { get; } =
            new List<PatternMatchCount>
            {
                new PatternMatchCount(Pattern1, Match1Async),
                new PatternMatchCount(Pattern2, Match2Async),
                new PatternMatchCount(Pattern3, Match3Async),
                new PatternMatchCount(Pattern4, Match4Async),
                new PatternMatchCount(Pattern5, Match5Async),
                new PatternMatchCount(Pattern6, Match6Async),
                new PatternMatchCount(Pattern7, Match7Async),
                new PatternMatchCount(Pattern8, Match8Async),
                new PatternMatchCount(Pattern9, Match9Async),
                new PatternMatchCount(Pattern10, Match10Async),
                new PatternMatchCount(Pattern11, Match11Async),
                new PatternMatchCount(Pattern12, Match12Async),
                new PatternMatchCount(Pattern13, Match13Async),
                new PatternMatchCount(Pattern14, Match14Async),
                new PatternMatchCount(Pattern15, Match15Async),
                new PatternMatchCount(Pattern16, Match16Async),
                new PatternMatchCount(Pattern17, Match17Async),
                new PatternMatchCount(Pattern18, Match18Async),
                new PatternMatchCount(Pattern19, Match19Async),
                new PatternMatchCount(Pattern20, Match20Async),
                new PatternMatchCount(Pattern21, Match21Async),
                new PatternMatchCount(Pattern22, Match22Async),
                new PatternMatchCount(Pattern23, Match23Async),
                new PatternMatchCount(Pattern24, Match24Async),
                new PatternMatchCount(Pattern25, Match25Async),
                new PatternMatchCount(Pattern26, Match26Async),
                new PatternMatchCount(Pattern27, Match27Async),
                new PatternMatchCount(Pattern28, Match28Async),
                new PatternMatchCount(Pattern29, Match29Async),
                new PatternMatchCount(Pattern30, Match30Async),
                new PatternMatchCount(Pattern31, Match31Async),
                new PatternMatchCount(Pattern32, Match32Async),
                new PatternMatchCount(Pattern33, Match33Async),
                new PatternMatchCount(Pattern34, Match34Async),
                new PatternMatchCount(Pattern35, Match35Async),
                new PatternMatchCount(Pattern36, Match36Async),
                new PatternMatchCount(Pattern37, Match37Async),
                new PatternMatchCount(Pattern38, Match38Async),
                new PatternMatchCount(Pattern39, Match39Async),
                new PatternMatchCount(Pattern40, Match40Async),
                new PatternMatchCount(Pattern41, Match41Async),
                new PatternMatchCount(Pattern42, Match42Async),
                new PatternMatchCount(Pattern43, Match43Async),
                new PatternMatchCount(Pattern44, Match44Async),
                new PatternMatchCount(Pattern45, Match45Async),
                new PatternMatchCount(Pattern46, Match46Async),
                new PatternMatchCount(Pattern47, Match47Async),
                new PatternMatchCount(Pattern48, Match48Async),
                new PatternMatchCount(Pattern49, Match49Async),
                new PatternMatchCount(Pattern50, Match50Async),
                new PatternMatchCount(Pattern51, Match51Async),
                new PatternMatchCount(Pattern52, Match52Async),
                new PatternMatchCount(Pattern53, Match53Async),
                new PatternMatchCount(Pattern54, Match54Async)
            };

        public PatternMatchingCount(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<long> PatternMatchingQueryStringEventCount(EventQueryModel queryModel)
        {
            var match = PatternMatchQueryStringEventCount.FirstOrDefault(d => d.CheckPatternQueryStringEvent(queryModel));
            long eventsCount;
            if (match != null)
                eventsCount = await match.UseMatchQueryStringEventCount(queryModel);
            else
                eventsCount = await _unitOfWork.EventRepository.Count();
            
            return eventsCount;
        }

        private static async Task<long> CountAll(Expression<Func<Event, bool>> filter)
            => await _unitOfWork.EventRepository.Count(filter);

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




        private static async Task<long> Match1Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Equals(queryModel.EventName.ToUpper()) && e.Categories.Any(x => x.Id == catId) &&
                                e.EventDate >= dateFrom && e.EventDate <= dateTo);
        }

        private static async Task<long> Match2Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper())
                                      && e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) &&
                                      e.EventDate >= dateFrom && e.EventDate <= dateTo);
        }

        private static async Task<long> Match3Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.EventDate >= dateFrom &&
            e.EventDate <= dateTo);
        }

        private static async Task<long> Match4Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.CityId == cityId && e.EventDate >= dateFrom && e.EventDate <= dateTo);
        }

        private static async Task<long> Match5Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.Categories.Any(x => x.Id == catId) &&
            e.EventDate >= dateFrom);
        }

        private static async Task<long> Match6Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper())
                                      && e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) &&
                                      e.EventDate >= dateFrom);
        }

        private static async Task<long> Match7Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.EventDate >= dateFrom);
        }

        private static async Task<long> Match8Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await CountAll(e => e.CityId == cityId && e.EventDate >= dateFrom);
        }

        private static async Task<long> Match9Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var catId = int.Parse(queryModel.CategoryId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.Categories.Any(x => x.Id == catId) &&
            e.EventDate <= dateTo);
        }

        private static async Task<long> Match10Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper())
                                      && e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) &&
                                      e.EventDate <= dateTo);
        }

        private static async Task<long> Match11Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.EventDate <= dateTo);
        }

        private static async Task<long> Match12Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.CityId == cityId && e.EventDate <= dateTo);
        }

        private static async Task<long> Match13Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var catId = int.Parse(queryModel.CategoryId);
            var date = DateTime.Parse(queryModel.Date);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.Categories.Any(x => x.Id == catId) &&
            e.EventDate == date);
        }

        private static async Task<long> Match14Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var date = DateTime.Parse(queryModel.Date);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper())
            && e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) &&
                                      e.EventDate == date);
        }

        private static async Task<long> Match15Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var date = DateTime.Parse(queryModel.Date);
            return await CountAll(e => e.CityId == cityId && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) && e.EventDate == date);
        }

        private static async Task<long> Match16Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var date = DateTime.Parse(queryModel.Date);
            return await CountAll(e => e.CityId == cityId && e.EventDate == date);
        }

        private static async Task<long> Match17Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.Id == catId) && e.EventDate >= dateFrom && e.EventDate <= dateTo);
        }

        private static async Task<long> Match18Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate >= dateFrom &&
                                      e.EventDate <= dateTo);
        }

        private static async Task<long> Match19Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.EventDate >= dateFrom && e.EventDate <= dateTo);
        }

        private static async Task<long> Match20Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventDate >= dateFrom && e.EventDate <= dateTo);
        }

        private static async Task<long> Match21Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.Id == catId) && e.EventDate >= dateFrom);
        }

        private static async Task<long> Match22Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate >= dateFrom);
        }

        private static async Task<long> Match23Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.EventDate >= dateFrom);
        }

        private static async Task<long> Match24Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventDate >= dateFrom);
        }

        private static async Task<long> Match25Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.Id == catId) && e.EventDate <= dateTo);
        }

        private static async Task<long> Match26Async(EventQueryModel queryModel)
        {
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate <= dateTo);
        }

        private static async Task<long> Match27Async(EventQueryModel queryModel)
        {
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.EventDate <= dateTo);
        }

        private static async Task<long> Match28Async(EventQueryModel queryModel)
        {
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventDate <= dateTo);
        }

        private static async Task<long> Match29Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var date = DateTime.Parse(queryModel.Date);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.Id == catId) && e.EventDate == date);
        }

        private static async Task<long> Match30Async(EventQueryModel queryModel)
        {
            var date = DateTime.Parse(queryModel.Date);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
                                      e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate == date);
        }

        private static async Task<long> Match31Async(EventQueryModel queryModel)
        {
            var date = DateTime.Parse(queryModel.Date);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()) &&
            e.EventDate == date);
        }

        private static async Task<long> Match32Async(EventQueryModel queryModel)
        {
            var date = DateTime.Parse(queryModel.Date);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventDate == date);
        }

        private static async Task<long> Match33Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            var catId = int.Parse(queryModel.CategoryId);
            return await CountAll(e => e.CityId == cityId && e.Categories.Any(x => x.Id == catId));
        }

        private static async Task<long> Match34Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            return await CountAll(e => e.CityId == cityId && e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())));
        }

        private static async Task<long> Match35Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            return await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.Categories.Any(x => x.Id == catId));
        }

        private static async Task<long> Match36Async(EventQueryModel queryModel)
            => await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) &&
            e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())));

        private static async Task<long> Match37Async(EventQueryModel queryModel)
            => await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()) && e.EventName.Contains(queryModel.EventName));

        private static async Task<long> Match38Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.Categories.Any(x => x.Id == catId) && e.EventDate >= dateFrom && e.EventDate <= dateTo);
        }

        private static async Task<long> Match39Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await CountAll(e => e.Categories.Any(x => x.Id == catId) && e.EventDate >= dateFrom);
        }

        private static async Task<long> Match40Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.Categories.Any(x => x.Id == catId) && e.EventDate <= dateTo);
        }

        private static async Task<long> Match41Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            var date = DateTime.Parse(queryModel.Date);
            return await CountAll(e => e.Categories.Any(x => x.Id == catId) && e.EventDate == date);
        }

        private static async Task<long> Match42Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) &&
            e.EventDate >= dateFrom && e.EventDate <= dateTo);
        }

        private static async Task<long> Match43Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await CountAll(e => e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate >= dateFrom);
        }

        private static async Task<long> Match44Async(EventQueryModel queryModel)
        {
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate <= dateTo);
        }

        private static async Task<long> Match45Async(EventQueryModel queryModel)
        {
            var date = DateTime.Parse(queryModel.Date);
            return await CountAll(e => e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())) && e.EventDate == date);
        }

        private static async Task<long> Match46Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.EventDate >= dateFrom && e.EventDate <= dateTo);
        }

        private static async Task<long> Match47Async(EventQueryModel queryModel)
        {
            var dateFrom = DateTime.Parse(queryModel.DateFrom);
            return await CountAll(e => e.EventDate >= dateFrom);
        }

        private static async Task<long> Match48Async(EventQueryModel queryModel)
        {
            var dateTo = DateTime.Parse(queryModel.DateTo);
            return await CountAll(e => e.EventDate <= dateTo);
        }

        private static async Task<long> Match49Async(EventQueryModel queryModel)
        {
            var date = DateTime.Parse(queryModel.Date);
            return await CountAll(e => e.EventDate == date);
        }

        private static async Task<long> Match50Async(EventQueryModel queryModel)
        {
            var catId = int.Parse(queryModel.CategoryId);
            return await CountAll(e => e.Categories.Any(x => x.Id == catId));
        }

        private static async Task<long> Match51Async(EventQueryModel queryModel)
            => await CountAll(e => e.Categories.Any(x => x.CategoryName.ToUpper().Contains(queryModel.CategoryName.ToUpper())));

        private static async Task<long> Match52Async(EventQueryModel queryModel)
            => await CountAll(e => e.EventName.ToUpper().Contains(queryModel.EventName.ToUpper()));

        private static async Task<long> Match53Async(EventQueryModel queryModel)
        {
            var cityId = int.Parse(queryModel.CityId);
            return await CountAll(e => e.CityId == cityId);
        }

        private static async Task<long> Match54Async(EventQueryModel queryModel)
            => await CountAll(e => e.City.CityName.ToUpper().Contains(queryModel.CityName.ToUpper()));
    }
}