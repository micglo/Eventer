using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.ApiActivity;
using Eventer.Model.QueryString.Pagination;
using Eventer.Service.ApiActivity.Interface;
using Eventer.Web.Api.Utility.Filter;
using Eventer.Web.Api.Utility.Versioning;

namespace Eventer.Web.Api.Controllers.V1
{
    /// <summary>
    /// Controller with actions to manage api activity logs.
    /// Administrators only.
    /// </summary>
    [ApiVersion1RoutePrefix("apiactivitylogs")]
    [MyAuthorize(Roles = "Administrators")]
    public class ApiActivityLogsController : ApiControllerBase
    {
        /// <summary>
        /// ApiActivityLogs Controller
        /// </summary>
        /// <param name="apiActivityLogService"></param>
        public ApiActivityLogsController(IApiActivityLogService apiActivityLogService)
        {
            ApiActivityLogService = apiActivityLogService;
        }


        /// <summary>
        /// Get all information about api activity. Administratos only.
        /// </summary>
        /// <remarks>
        /// Get all information about api activity. Administratos only.
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <returns>Paged list of api activity data</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("", Name = "GetApiActivityLogs")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<ApiActivityLogDto>))]
        public async Task<IHttpActionResult> GetApiActivityLogs([FromUri] Pagination paginationModel)
            => Ok(await ApiActivityLogService.GetAllAsync(paginationModel.Page, paginationModel.PageSize));


        /// <summary>
        /// Get single api activity log specified by id. Administratos only.
        /// </summary>
        /// <remarks>
        /// Get single api activity log specified by id. Administratos only.
        /// </remarks>
        /// <param name="id">Id of particular api activity log</param>
        /// <returns>Single api activity log</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Log not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{id:long}", Name = "GetApiActivityLog")]
        [HttpGet]
        [ResponseType(typeof(ApiActivityLogDto))]
        public async Task<IHttpActionResult> GetApiActivityLog(long id)
            => Ok(await ApiActivityLogService.GetByIdAsync(id));


        /// <summary>
        /// Get all information about api activity specified by username. Administratos only.
        /// </summary>
        /// <remarks>
        /// Get all information about api activity specified by username. Administratos only.
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <param name="userName">user's email</param>
        /// <returns>Paged list of api activity by single user</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route(
            "{userName:regex(^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&\'\\*\\+/=\\?\\^`\\{\\}\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$)}",
            Name = "GetApiActivityLogsByUser")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<ApiActivityLogDto>))]
        public async Task<IHttpActionResult> GetApiActivityLogsByUser(string userName,
            [FromUri] Pagination paginationModel)
            =>
                Ok(await ApiActivityLogService.GetAllByUserAsync(userName, paginationModel.Page,
                    paginationModel.PageSize));


        /// <summary>
        /// Get all information about api activity specified by hostAddress. Administratos only.
        /// </summary>
        /// <remarks>
        /// Get all information about api activity specified by hostAddress. Administratos only.
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <param name="hostAddress">Host address</param>
        /// <returns>Paged list of api activity by hostAddress</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Host not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{hostAddress}", Name = "GetApiActivityLogsByHost")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<ApiActivityLogDto>))]
        public async Task<IHttpActionResult> GetApiActivityLogsByHost(string hostAddress,
            [FromUri] Pagination paginationModel)
            =>
                Ok(await ApiActivityLogService.GetAllByHostAddressAsync(hostAddress, paginationModel.Page,
                    paginationModel.PageSize));


        /// <summary>
        /// Delete api activity log specified by id. Administratos only.
        /// </summary>
        /// <remarks>
        /// Delete api activity log specified by id. Administratos only.
        /// </remarks>
        /// <param name="id">Id of api activity log</param>
        /// <returns>Removed api activity log</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Log not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{id:long}", Name = "DeleteApiActivityLog")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteApiActivityLog(long id)
        {
            await ApiActivityLogService.RemoveAsync(id);
            return Ok();
        }


        /// <summary>
        /// Delete all api avtivity logs. Administratos only.
        /// </summary>
        /// <remarks>
        /// Delete all api avtivity logs. Administratos only.
        /// </remarks>
        /// <returns>Http status code</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("DeleteApiActivityLogs", Name = "DeleteApiActivityLogs")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteApiActivityLogs()
        {
            await ApiActivityLogService.RemoveAllAsync();
            return Ok();
        }
    }
}