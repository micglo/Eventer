using System.Threading.Tasks;
using Eventer.WindowsService.Service.ClientModel.EventerApi.Token;

namespace Eventer.WindowsService.Service.Client.Interface
{
    public interface IPoznanApiService
    {
        Task TakeEventsAndPostAsList(Token token);
    }
}