using System.Collections.Generic;
using Eventer.Domain.Entity.Common;

namespace Eventer.Domain.Entity.EventerEntity
{
    public class State : CommonEntity<int>
    {
        public string StateName { get; set; }

        public virtual ICollection<City> Cities { get; set; }
    }
}