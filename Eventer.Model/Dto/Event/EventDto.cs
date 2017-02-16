using System;
using System.Collections.Generic;
using Eventer.Model.Dto.Category;
using Eventer.Model.Dto.City;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.Event
{
    public class EventDto : CommonDto<long>
    {
        public string EventName { get; set; }
        public DateTime? EventDate { get; set; }
        public string EventLocalization { get; set; }
        public string EventImage { get; set; }
        public string EventUrl { get; set; }
        public string EventDescription { get; set; }
        public CityDto City { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }
    }
}