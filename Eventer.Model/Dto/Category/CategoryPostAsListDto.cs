using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.Category
{
    public class CategoryPostAsListDto : DtoBase
    {
        [Required]
        public List<string> CategoryNames { get; set; }
    }
}