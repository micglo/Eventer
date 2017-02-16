using System.Threading.Tasks;
using Eventer.WindowsService.Service.ClientModel.EventerApi.Token;

namespace Eventer.WindowsService.Service.Client.Interface
{
    public interface IWroclawGoApiService
    {
        Task TakeEventsAndPostAsList(Token token);
    }
}