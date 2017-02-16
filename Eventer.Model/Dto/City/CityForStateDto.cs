using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.City
{
    public class CityForStateDto : CommonDto<int>
    {
        public string CityName { get; set; }
    }
}