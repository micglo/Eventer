using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.Client
{
    public class ClientByUserNameDto : CommonDto<string>
    {        
        public string ApplicationType { get; set; }
        public bool Active { get; set; }
        public int RefreshTokenLifeTime { get; set; }
        public string AllowedOrigin { get; set; }
    }
}