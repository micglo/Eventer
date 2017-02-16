using System.Threading.Tasks;
using Eventer.Model.Dto.State;
using Eventer.Service.Common;

namespace Eventer.Service.EventerService.Interface
{
    public interface IStateService : IServiceBase<StateDto, StatePostDto, StatePutDto>
    {
        Task<StateDto> GetByNameAsync(string stateName);
    }
}