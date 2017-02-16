using System.Collections.Generic;
using Eventer.Model.Dto.City;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.State
{
    public class StateDto : CommonDto<int>
    {
        public string StateName { get; set; }

        public virtual ICollection<CityForStateDto> Cities { get; set; }
    }
}