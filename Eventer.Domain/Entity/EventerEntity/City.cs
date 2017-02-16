using System.Collections.Generic;
using Eventer.Domain.Entity.Common;

namespace Eventer.Domain.Entity.EventerEntity
{
    public class City : CommonEntity<int>
    {
        public string CityName { get; set; }
        public int StateId { get; set; }

        public virtual State State { get; set; }
        public virtual ICollection<Event> Events { get; set; }
    }
}