using System.Threading.Tasks;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.Client;
using Eventer.Service.Common;

namespace Eventer.Service.Client.Interface
{
    public interface IClientService : IServiceBase<ClientDto, ClientPostDto, ClientDto>
    {
        Task<PagedItems<ClientDto>> GetUserClients(string userName, string skip, string take);
        ClientDto GetById(object id);
        bool Exists(object id);
        Task<ClientNoSecretDto> UpdateNoSecret(ClientNoSecretDto dtoModel);
        string GenerateClientId();
        string GenerateClientSecret();
        string GetHash(string input);
        Task<long> MyClientsCount(string userName);
        Task<PagedItems<ClientByUserNameDto>> GetAllByUserName(string userName, string skip, string take);
        Task<bool> CheckUserClient(string userName, string clientId);
        Task<ClientByUserNameDto> GetMyClientAsync(string userName, string clientId);
        Task<long> GetActiveJsClientCountByUserName(string userName);
        Task<long> GetActiveNativeClientCountByUserName(string userName);
    }
}