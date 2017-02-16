using System.Collections.Generic;
using System.Threading.Tasks;
using Eventer.Model.Dto.Category;
using Eventer.Service.Common;

namespace Eventer.Service.EventerService.Interface
{
    public interface ICategoryService : IServiceBase<CategoryDto, CategoryPostDto, CategoryDto>
    {
        Task<CategoryDto> GetByNameAsync(string categoryName);
        Task<ICollection<CategoryDto>> AddListAsync(CategoryPostAsListDto categoryList);
    }
}