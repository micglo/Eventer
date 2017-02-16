using System.Collections.Generic;
using Eventer.Domain.Entity.Common;

namespace Eventer.Domain.Entity.EventerEntity
{
    public class Category : CommonEntity<int>
    {
        public string CategoryName { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}