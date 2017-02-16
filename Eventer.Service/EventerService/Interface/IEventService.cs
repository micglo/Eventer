using System.Collections.Generic;
using System.Threading.Tasks;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.Event;
using Eventer.Model.QueryString.Event;
using Eventer.Service.Common;

namespace Eventer.Service.EventerService.Interface
{
    public interface IEventService : IServiceBase<EventDto, EventPostDto, EventPutDto>
    {
        Task<PagedItems<EventDto>> GetEventsByParamAsync(EventQueryModel queryModel);
        Task<ICollection<EventDto>> AddListAsync(EventPostAsListDto eventList);
        Task<string> CheckCityExistance(IEnumerable<EventPostDto> events);
    }
}