using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.City
{
    public class CityPostDto : DtoBase
    {
        [Required]
        public string CityName { get; set; }

        [Required]
        public int StateId { get; set; }
    }
}