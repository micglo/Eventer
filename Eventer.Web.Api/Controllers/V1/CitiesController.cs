using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.City;
using Eventer.Model.QueryString.Pagination;
using Eventer.Service.EventerService.Interface;
using Eventer.Utility.CustomException;
using Eventer.Web.Api.Utility.Filter;
using Eventer.Web.Api.Utility.Versioning;

namespace Eventer.Web.Api.Controllers.V1
{
    /// <summary>
    /// Controller with actions to manage cities.
    /// Administrators only.
    /// </summary>
    [ApiVersion1RoutePrefix("cities")]
    public class CitiesController : ApiControllerBase
    {
        /// <summary>
        /// Cities Controller
        /// </summary>
        /// <param name="customException"></param>
        /// <param name="cityService"></param>
        /// <param name="stateService"></param>
        public CitiesController(ICityService cityService, IStateService stateService, ICustomException customException)
        {
            CityService = cityService;
            StateService = stateService;
            CustomException = customException;
        }


        /// <summary>
        /// Get list of cities
        /// </summary>
        /// <remarks>
        /// Get list of cities
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <returns>Paged list of cities</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [Route("", Name = "GetCities")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<CityDto>))]
        public async Task<IHttpActionResult> GetCities([FromUri] Pagination paginationModel)
            => Ok(await CityService.GetAllAsync(paginationModel.Page, paginationModel.PageSize));


        /// <summary>
        /// Get single city specified by id
        /// </summary>
        /// <remarks>
        /// Get single city specified by id
        /// </remarks>
        /// <param name="id">Id of city</param>
        /// <returns>Single city</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">City not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{id:int}", Name = "GetCity")]
        [HttpGet]
        [ResponseType(typeof(CityDto))]
        public async Task<IHttpActionResult> GetCity(int id)
            => Ok(await CityService.GetByIdAsync(id));


        /// <summary>
        /// Get single city specified by name
        /// </summary>
        /// <remarks>
        /// Get single city specified by name
        /// </remarks>
        /// <param name="cityName">Name of the city</param>
        /// <returns>Single city</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">City not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{cityName}", Name = "GetCityByName")]
        [HttpGet]
        [ResponseType(typeof(CityDto))]
        public async Task<IHttpActionResult> GetCityByName(string cityName)
            => Ok(await CityService.GetByCityNameAsync(cityName));


        /// <summary>
        /// Edit particular city specified by id. Administrators only.
        /// </summary>
        /// <remarks>
        /// Edit particular city specified by id. Administratos only.
        /// </remarks>
        /// <param name="id">Id of city</param>
        /// <param name="city">City data model</param>
        /// <returns>Single city</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">City/State not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:int}", Name = "PutCity")]
        [HttpPut]
        [ResponseType(typeof(CityDto))]
        public async Task<IHttpActionResult> PutCity(int id, CityPutDto city)
        {
            if (id != city.Id)
            {
                CustomException.ThrowBadRequestException($"Id: {id} doesn't match.");
            }

            return Ok(await CityService.Update(city));
        }


        /// <summary>
        ///  Create new city. Administrators only.
        /// </summary>
        /// <remarks>
        /// Create new city. Administrators only.
        /// </remarks>
        /// <param name="city">City data model</param>
        /// <returns>Created city</returns>
        /// <response code="201">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">State Not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("", Name = "PostCity")]
        [HttpPost]
        [ResponseType(typeof(CityDto))]
        public async Task<IHttpActionResult> PostState(CityPostDto city)
        {
            var newCity = await CityService.AddAsync(city);

            return CreatedAtRoute("CityRoute", new { id = newCity.Id }, newCity);
        }


        /// <summary>
        /// Delete city specified by id. Administratos only.
        /// </summary>
        /// <remarks>
        /// Delete city specified by id. Administratos only.
        /// </remarks>
        /// <param name="id">Id of city</param>
        /// <returns>Http status code</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">City not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:int}", Name = "DeleteCity")]
        [HttpDelete]
        [ResponseType(typeof(CityDto))]
        public async Task<IHttpActionResult> DeleteCity(int id)
        {
            await CityService.RemoveAsync(id);
            return Ok();
        }
    }
}