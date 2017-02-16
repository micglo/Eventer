using System.Threading.Tasks;
using Eventer.Model.Dto.RefreshToken;
using Eventer.Service.Common;

namespace Eventer.Service.RefreshToken.Interface
{
    public interface IRefreshTokenService : IServiceBase<RefreshTokenDto, RefreshTokenDto, RefreshTokenDto>
    {
        Task<RefreshTokenDto> GetByClientIdAsync(object clientId);
        string GenerateRefreshTokenId();
        Task<bool> Exists(string id);
    }
}