using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Eventer.Model.Dto.Common
{
    public abstract class CommonDto<T> : DtoBase
    {
        [Required]
        public T Id { get; set; }

        public ICollection<Link> Links { get; set; }
    }
}