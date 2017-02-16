using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.State;
using Eventer.Model.QueryString.Pagination;
using Eventer.Service.EventerService.Interface;
using Eventer.Utility.CustomException;
using Eventer.Web.Api.Utility.Filter;
using Eventer.Web.Api.Utility.Versioning;

namespace Eventer.Web.Api.Controllers.V1
{
    /// <summary>
    /// Controller with actions to manage states.
    /// </summary>
    [ApiVersion1RoutePrefix("states")]
    public class StatesController : ApiControllerBase
    {
        /// <summary>
        /// States controller
        /// </summary>
        /// <param name="customException"></param>
        /// <param name="stateService"></param>
        public StatesController(IStateService stateService, ICustomException customException)
        {
            StateService = stateService;
            CustomException = customException;
        }


        /// <summary>
        /// Get list of states.
        /// </summary>
        /// <remarks>
        /// Get list of states.
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <returns>Paged list of states.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [Route("", Name = "GetStates")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<StateDto>))]
        public async Task<IHttpActionResult> GetStates([FromUri] Pagination paginationModel)
            => Ok(await StateService.GetAllAsync(paginationModel.Page, paginationModel.PageSize));


        /// <summary>
        /// Get single state specified by id.
        /// </summary>
        /// <remarks>
        /// Get single state specified by id.
        /// </remarks>
        /// <param name="id">Id of state.</param>
        /// <returns>Single state.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">State not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{id:int}", Name = "GetState")]
        [HttpGet]
        [ResponseType(typeof(StateDto))]
        public async Task<IHttpActionResult> GetState(int id)
            => Ok(await StateService.GetByIdAsync(id));


        /// <summary>
        /// Get single state specified by name.
        /// </summary>
        /// <remarks>
        /// Get single state specified by name.
        /// </remarks>
        /// <param name="stateName">Name of the state.</param>
        /// <returns>Single state.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">State not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{stateName}", Name = "GetStateByName")]
        [HttpGet]
        [ResponseType(typeof(StateDto))]
        public async Task<IHttpActionResult> GetStateByName(string stateName)
            => Ok(await StateService.GetByNameAsync(stateName));


        /// <summary>
        /// Edit state. Administrators only.
        /// </summary>
        /// <remarks>
        /// Edit state. Administrators only.
        /// </remarks>
        /// <param name="id">Id of state.</param>
        /// <param name="state">State data model.</param>
        /// <returns>Edited state.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">State not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:int}", Name = "PutState")]
        [HttpPut]
        [ResponseType(typeof(StateDto))]
        public async Task<IHttpActionResult> PutState(int id, StatePutDto state)
        {
            if (id != state.Id)
                CustomException.ThrowBadRequestException($"Id: {id} doesn't match.");

            return Ok(await StateService.Update(state));
        }


        /// <summary>
        /// Create new state. Administrators only.
        /// </summary>
        /// <remarks>
        /// Create new state. Administrators only.
        /// </remarks>
        /// <param name="state">State data model.</param>
        /// <returns>Created state.</returns>
        /// <response code="201">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("", Name = "PostState")]
        [HttpPost]
        [ResponseType(typeof(StateDto))]
        public async Task<IHttpActionResult> PostState(StatePostDto state)
        {
            var newState = await StateService.AddAsync(state);

            return CreatedAtRoute("StateRoute", new { id = newState.Id }, newState);
        }


        /// <summary>
        /// Delete state. Administrators only.
        /// </summary>
        /// <remarks>
        /// Delete state. Administrators only.
        /// </remarks>
        /// <param name="id">Id of state.</param>
        /// <returns>Http status code.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">State not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:int}", Name = "DeleteState")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteState(int id)
        {
            await StateService.RemoveAsync(id);
            return Ok();
        }
    }
}