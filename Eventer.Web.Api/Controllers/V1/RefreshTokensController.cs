using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.RefreshToken;
using Eventer.Model.QueryString.Pagination;
using Eventer.Service.RefreshToken.Interface;
using Eventer.Utility.CustomException;
using Eventer.Web.Api.Utility.Filter;
using Eventer.Web.Api.Utility.Versioning;

namespace Eventer.Web.Api.Controllers.V1
{
    /// <summary>
    /// Controller with actions to manage refresh tokens.
    /// </summary>
    [ApiVersion1RoutePrefix("refreshtokens")]
    [MyAuthorize(Roles = "Administrators")]
    public class RefreshTokensController : ApiControllerBase
    {
        /// <summary>
        /// Refresh tokens controller
        /// </summary>
        /// <param name="customException"></param>
        /// <param name="refreshTokenService"></param>
        public RefreshTokensController(IRefreshTokenService refreshTokenService, ICustomException customException)
        {
            RefreshTokenService = refreshTokenService;
            CustomException = customException;
        }


        /// <summary>
        /// Get list of refresh tokens. Administrators only.
        /// </summary>
        /// <remarks>
        /// Get list of refresh tokens. Administrators only.
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <returns>Paged list of refresh tokens.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [Route("", Name = "GetRefreshTokens")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<RefreshTokenDto>))]
        public async Task<IHttpActionResult> GetRefreshTokens([FromUri] Pagination paginationModel)
            => Ok(await RefreshTokenService.GetAllAsync(paginationModel.Page, paginationModel.PageSize));


        /// <summary>
        /// Get single refresh token specified by clientId. Administrators only.
        /// </summary>
        /// <remarks>
        /// Get single refresh token specified by clientId. Administrators only.
        /// </remarks>
        /// <param name="clientId">Id of client.</param>
        /// <returns>Single refresh token.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Client not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{clientId:guid}", Name = "GetRefreshToken")]
        [HttpGet]
        [ResponseType(typeof(RefreshTokenDto))]
        public async Task<IHttpActionResult> GetRefreshToken(string clientId)
            => Ok(await RefreshTokenService.GetByClientIdAsync(clientId));


        /// <summary>
        /// Delete refresh token. Administrators only.
        /// </summary>
        /// <remarks>
        /// Delete refresh token. Administrators only.
        /// </remarks>
        /// <param name="clientId">Id of client.</param>
        /// <returns>Http status code.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Client not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{clientId:guid}", Name = "DeleteRefreshToken")]
        [HttpDelete]
        [ResponseType(typeof(RefreshTokenDto))]
        public async Task<IHttpActionResult> DeleteRefreshToken(string clientId)
        {
            var refreshToken = await RefreshTokenService.GetByClientIdAsync(clientId);

            await RefreshTokenService.RemoveAsync(refreshToken.Id);
            return Ok();
        }
    }
}