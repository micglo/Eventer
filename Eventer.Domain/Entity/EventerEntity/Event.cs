using System;
using System.Collections.Generic;
using Eventer.Domain.Entity.Common;

namespace Eventer.Domain.Entity.EventerEntity
{
    public class Event : CommonEntity<long>
    {
        public int CityId { get; set; }
        public string EventName { get; set; }
        public DateTime? EventDate { get; set; }
        public string EventLocalization { get; set; }
        public string EventImage { get; set; }
        public string EventUrl { get; set; }
        public string EventDescription { get; set; }

        public virtual City City { get; set; }
        public virtual ICollection<Category> Categories { get; set; }
    }
}