using System.Threading.Tasks;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.Common;

namespace Eventer.Service.Common
{
    public interface IServiceBase<TDto, TPostDto, TPutDto> where TDto : DtoBase where TPostDto : DtoBase where TPutDto : DtoBase
    {
        Task<PagedItems<TDto>> GetAllAsync(string skip, string take);
        Task<TDto> GetByIdAsync(object id);
        Task<TDto> AddAsync(TPostDto dtoModel);
        Task<TDto> Update(TPutDto dtoModel);
        Task RemoveAsync(object id);

        Task<int> SaveChangesAsync();
    }
}