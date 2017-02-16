using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.State
{
    public class StatePostDto : DtoBase
    {
        [Required]
        public string StateName { get; set; }
    }
}