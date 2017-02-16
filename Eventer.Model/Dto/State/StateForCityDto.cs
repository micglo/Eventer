using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.State
{
    public class StateForCityDto : CommonDto<int>
    {
        public string StateName { get; set; }
    }
}