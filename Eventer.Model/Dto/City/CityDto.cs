using Eventer.Model.Dto.Common;
using Eventer.Model.Dto.State;

namespace Eventer.Model.Dto.City
{
    public class CityDto : CommonDto<int>
    {
        public string CityName { get; set; }
        public StateForCityDto State { get; set; }
    }
}