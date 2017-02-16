using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.ErrorLog;
using Eventer.Model.QueryString.Pagination;
using Eventer.Service.ErrorLog.Interface;
using Eventer.Utility.CustomException;
using Eventer.Web.Api.Utility.Filter;
using Eventer.Web.Api.Utility.Versioning;

namespace Eventer.Web.Api.Controllers.V1
{
    /// <summary>
    /// Controller with actions to manage error logs.
    /// </summary>
    [ApiVersion1RoutePrefix("errorlogs")]
    [MyAuthorize(Roles = "Administrators")]
    public class ErrorLogsController : ApiControllerBase
    {
        /// <summary>
        /// Error logs Controller
        /// </summary>
        /// <param name="customException"></param>
        /// <param name="errorLogService"></param>
        /// <param name="userManager"></param>
        public ErrorLogsController(IErrorLogService errorLogService, ApplicationUserManager userManager, ICustomException customException)
        {
            ErrorLogService = errorLogService;
            UserManager = userManager;
            CustomException = customException;
        }


        /// <summary>
        /// Get list of error logs. Administrators only.
        /// </summary>
        /// <remarks>
        /// Get list of error logs. Administrators only.
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <returns>Paged list of error logs.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("", Name = "GetErrorLogs")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<ErrorLogDto>))]
        public async Task<IHttpActionResult> GetErrorLogs([FromUri] Pagination paginationModel)
            => Ok(await ErrorLogService.GetAllAsync(paginationModel.Page, paginationModel.PageSize));


        /// <summary>
        /// Get single error log specified by id.  Administrators only.
        /// </summary>
        /// <remarks>
        /// Get single error log specified by id.  Administrators only.
        /// </remarks>
        /// <param name="id">If of error log.</param>
        /// <returns>Single error log.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Log not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{id:long}", Name = "GetErrorLog")]
        [HttpGet]
        [ResponseType(typeof(ErrorLogDto))]
        public async Task<IHttpActionResult> GetErrorLog(long id)
            => Ok(await ErrorLogService.GetByIdAsync(id));


        /// <summary>
        /// Get list of error los for single user specified by username. Administrators only.
        /// </summary>
        /// <remarks>
        /// Get list of error los for single user specified by username. Administrators only.
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <param name="userName">Name of the user</param>
        /// <returns>Paged list of error logs.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Log/User not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{userName}", Name = "GetErrorLogsByUser")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<ErrorLogDto>))]
        public async Task<IHttpActionResult> GetErrorLogsByUser(string userName, [FromUri] Pagination paginationModel)
        {
            if (await UserManager.FindByEmailAsync(userName) == null)
                CustomException.ThrowNotFoundException($"There is no user {userName}.");

            return Ok(await ErrorLogService.GetAllByUserAsync(userName, paginationModel.Page, paginationModel.PageSize));
        }


        /// <summary>
        /// Delete error log. Administrators only.
        /// </summary>
        /// <remarks>
        /// Delete error log. Administrators only.
        /// </remarks>
        /// <param name="id">Id of error log.</param>
        /// <returns>Http status code.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Log not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{id:long}", Name = "DeleteErrorLog")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteErrorLog(long id)
        {
            await ErrorLogService.RemoveAsync(id);
            return Ok();
        }


        /// <summary>
        /// Delete all error logs. Administrators only.
        /// </summary>
        /// <remarks>
        /// Delete all error logs. Administrators only.
        /// </remarks>
        /// <returns></returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("DeleteErrorLogs", Name = "DeleteErrorLogs")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteErrorLogs()
        {
            await ErrorLogService.RemoveAllAsync();
            return Ok();
        }
    }
}