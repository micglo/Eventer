using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.Event
{
    public class EventPostAsListDto : DtoBase
    {
        [Required]
        public List<EventPostDto> Events { get; set; }
    }
}