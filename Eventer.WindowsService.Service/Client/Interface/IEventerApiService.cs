using System.Collections.Generic;
using System.Threading.Tasks;
using Eventer.WindowsService.Service.ClientModel.EventerApi;
using Eventer.WindowsService.Service.ClientModel.EventerApi.Token;

namespace Eventer.WindowsService.Service.Client.Interface
{
    public interface IEventerApiService
    {
        Task<Token> GetToken();
        Task<Token> GetTokenByRefreshToken(string refreshToken);
        Task<int> GetCityId(Token token, string cityName);
        Task<IList<EventItem>> GetEvents(Token token, int cityId);
        Task<bool> PostEventAsList(Token token, EventItemPostList events, string loggerName);
    }
}