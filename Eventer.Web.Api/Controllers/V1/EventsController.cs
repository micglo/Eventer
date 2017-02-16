using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.Event;
using Eventer.Model.QueryString.Event;
using Eventer.Service.EventerService.Interface;
using Eventer.Utility.CustomException;
using Eventer.Web.Api.Utility.Filter;
using Eventer.Web.Api.Utility.Versioning;

namespace Eventer.Web.Api.Controllers.V1
{
    /// <summary>
    /// Controller with actions to manage events.
    /// </summary>
    [ApiVersion1RoutePrefix("events")]
    public class EventsController : ApiControllerBase
    {
        /// <summary>
        /// Events Controller
        /// </summary>
        /// <param name="customException"></param>
        /// <param name="eventService"></param>
        /// <param name="cityService"></param>
        /// <param name="categoryService"></param>
        public EventsController(IEventService eventService, ICityService cityService, ICategoryService categoryService, ICustomException customException)
        {
            EventService = eventService;
            CityService = cityService;
            CategoryService = categoryService;
            CustomException = customException;
        }


        /// <summary>
        /// Get list of events specified by query strings.
        /// </summary>
        /// <remarks>
        /// Get list of events specified by query strings.
        /// </remarks>
        /// <param name="queryModel">Model representing query string</param>
        /// <returns>Paged list fo events.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [Route("", Name = "GetEvents")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<EventDto>))]
        public async Task<IHttpActionResult> GetEvents([FromUri] EventQueryModel queryModel)
            => Ok(await EventService.GetEventsByParamAsync(queryModel));


        /// <summary>
        /// Get single event specified by id.
        /// </summary>
        /// <remarks>
        /// Get single event specified by id.
        /// </remarks>
        /// <param name="id">Id of event.</param>
        /// <returns>Single event.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Event not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{id:long}", Name = "GetEvent")]
        [HttpGet]
        [ResponseType(typeof(EventDto))]
        public async Task<IHttpActionResult> GetEvent(long id)
            => Ok(await EventService.GetByIdAsync(id));


        /// <summary>
        /// Edit event specified by id. Administrators only.
        /// </summary>
        /// <remarks>
        /// Edit event specified by id. Administrators only.
        /// </remarks>
        /// <param name="id">Id of event.</param>
        /// <param name="event">Event data model.</param>
        /// <returns>Single event.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Event/Category/City not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:long}", Name = "PutEvent")]
        [HttpPut]
        [ResponseType(typeof(EventDto))]
        public async Task<IHttpActionResult> PutEvent(long id, EventPutDto @event)
        {
            if (id != @event.Id)
                CustomException.ThrowBadRequestException($"Id: {id} doesn't match.");

            return Ok(await EventService.Update(@event));
        }


        /// <summary>
        /// Create new event. Administrators only.
        /// </summary>
        /// <remarks>
        /// Create new event. Administrators only.
        /// </remarks>
        /// <param name="event">Event data model.</param>
        /// <returns>Created event.</returns>
        /// <response code="201">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Category/City not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("", Name = "PostEvent")]
        [HttpPost]
        [ResponseType(typeof(EventDto))]
        public async Task<IHttpActionResult> PostEvent(EventPostDto @event)
        {
            var newEvent = await EventService.AddAsync(@event);

            return CreatedAtRoute("EventRoute", new { id = newEvent.Id }, newEvent);
        }


        /// <summary>
        /// Add range of events. Administraors only.
        /// </summary>
        /// <remarks>
        /// Add range of events. Administraors only.
        /// </remarks>
        /// <param name="eventList">Model representing list of event data.</param>
        /// <returns>Created events.</returns>
        /// <response code="201">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Category/City not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("PostEventList", Name = "PostEventList")]
        [HttpPost]
        [ResponseType(typeof(EventDto))]
        public async Task<IHttpActionResult> PostEventAsList(EventPostAsListDto eventList)
        {
            var cityExistance = await EventService.CheckCityExistance(eventList.Events);

            if (!string.IsNullOrEmpty(cityExistance))
                CustomException.ThrowNotFoundException($"Cities with id = {cityExistance} don't exist.");

            var newEventList = await EventService.AddListAsync(eventList);

            return Created("EventRoute", newEventList);
        }


        /// <summary>
        /// Delete event specified by id. Administrators only.
        /// </summary>
        /// <remarks>
        /// Delete event specified by id. Administrators only.
        /// </remarks>
        /// <param name="id">Id of event.</param>
        /// <returns>Http status code.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Event not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:long}", Name = "DeleteEvent")]
        [HttpDelete]
        [ResponseType(typeof(EventDto))]
        public async Task<IHttpActionResult> DeleteEvent(long id)
        {
            await EventService.RemoveAsync(id);
            return Ok();
        }
    }
}