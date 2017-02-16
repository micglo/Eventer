using System.Threading.Tasks;
using Eventer.Model.Dto.City;
using Eventer.Service.Common;

namespace Eventer.Service.EventerService.Interface
{
    public interface ICityService : IServiceBase<CityDto, CityPostDto, CityPutDto>
    {
        Task<CityDto> GetByCityNameAsync(string cityName);
    }
}