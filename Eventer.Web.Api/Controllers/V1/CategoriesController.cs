using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.Category;
using Eventer.Model.QueryString.Pagination;
using Eventer.Service.EventerService.Interface;
using Eventer.Utility.CustomException;
using Eventer.Web.Api.Utility.Filter;
using Eventer.Web.Api.Utility.Versioning;

namespace Eventer.Web.Api.Controllers.V1
{
    /// <summary>
    /// Controller with actions to manage categories.
    /// Administrators only.
    /// </summary>
    [ApiVersion1RoutePrefix("categories")]
    public class CategoriesController : ApiControllerBase
    {
        /// <summary>
        /// Categories Controller
        /// </summary>
        /// <param name="customException"></param>
        /// <param name="categoryService"></param>
        public CategoriesController(ICategoryService categoryService, ICustomException customException)
        {
            CategoryService = categoryService;
            CustomException = customException;
        }


        /// <summary>
        /// Get all categories
        /// </summary>
        /// <remarks>
        /// Get all categories
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <returns>Paged list with categories</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [Route("", Name = "GetCategories")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<CategoryDto>))]
        public async Task<IHttpActionResult> GetCategories([FromUri] Pagination paginationModel)
            => Ok(await CategoryService.GetAllAsync(paginationModel.Page, paginationModel.PageSize));


        /// <summary>
        /// Get single category specified by id
        /// </summary>
        /// <remarks>
        /// Get single category specified by id
        /// </remarks>
        /// <param name="id">Id of category</param>
        /// <returns>Single category</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{id:int}", Name = "GetCategory")]
        [HttpGet]
        [ResponseType(typeof(CategoryDto))]
        public async Task<IHttpActionResult> GetCategory(int id)
            => Ok(await CategoryService.GetByIdAsync(id));

        /// <summary>
        /// Get single category specified by categoryName
        /// </summary>
        /// <remarks>
        /// Get single category specified by categoryName
        /// </remarks>
        /// <param name="categoryName">Name of category</param>
        /// <returns>Single category</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("{categoryName}", Name = "GetCategoryByName")]
        [HttpGet]
        [ResponseType(typeof(CategoryDto))]
        public async Task<IHttpActionResult> GetCategoryByName(string categoryName)
            => Ok(await CategoryService.GetByNameAsync(categoryName));

        /// <summary>
        /// Edit category. Administrators only.
        /// </summary>
        /// <remarks>
        /// Edit category. Administrators only.
        /// </remarks>
        /// <param name="id">Id of category</param>
        /// <param name="category">Category model with data to edit</param>
        /// <returns>Edited category</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:int}", Name = "PutCategory")]
        [HttpPut]
        [ResponseType(typeof(CategoryDto))]
        public async Task<IHttpActionResult> PutCategory(int id, CategoryDto category)
        {
            if (id != category.Id)
                CustomException.ThrowBadRequestException($"Id: {id} doesn't match.");

            return Ok(await CategoryService.Update(category));
        }

        /// <summary>
        /// Add new category. Administrators only.
        /// </summary>
        /// <remarks>
        /// Add new category. Administrators only.
        /// </remarks>
        /// <param name="category">Category model with data to insert</param>
        /// <returns>Created category</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("", Name = "PostCategory")]
        [HttpPost]
        [ResponseType(typeof(CategoryDto))]
        public async Task<IHttpActionResult> PostCategory(CategoryPostDto category)
        {
            var newCategory = await CategoryService.AddAsync(category);

            return CreatedAtRoute("CategoryRoute", new { id = newCategory.Id }, newCategory);
        }

        /// <summary>
        /// Add range of categories. Administrators only.
        /// </summary>
        /// <remarks>
        /// Add range of categories. Administrators only.
        /// </remarks>
        /// <param name="categoryList">Model with list of categories to insert</param>
        /// <returns>List of created categories</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("PostCategoryList", Name = "PostCategoryList")]
        [HttpPost]
        [ResponseType(typeof(ICollection<CategoryDto>))]
        public async Task<IHttpActionResult> PostCategoryAsList(CategoryPostAsListDto categoryList)
        {
            var newCategoryList = await CategoryService.AddListAsync(categoryList);

            return Created("CategoryRoute", newCategoryList);
        }

        /// <summary>
        /// Delete category by specified id. Administrators only.
        /// </summary>
        /// <remarks>
        /// Delete category by specified id. Administrators only.
        /// </remarks>
        /// <param name="id">Id of category</param>
        /// <returns>Deleted category</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Category not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:int}", Name = "DeleteCategory")]
        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCategory(int id)
        {
            await CategoryService.RemoveAsync(id);
            return Ok();
        }
    }
}