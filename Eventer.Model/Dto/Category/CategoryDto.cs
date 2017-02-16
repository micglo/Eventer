using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.Category
{
    public class CategoryDto : CommonDto<int>
    {
        [Required]
        public string CategoryName { get; set; }
    }
}