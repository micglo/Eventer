using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Eventer.Model.Dto.Common;

namespace Eventer.Model.Dto.Event
{
    public class EventPostDto : DtoBase
    {
        public string EventName { get; set; }
        public DateTime? EventDate { get; set; }
        public string EventLocalization { get; set; }
        public string EventImage { get; set; }
        public string EventUrl { get; set; }
        public string EventDescription { get; set; }

        [Required]
        public int CityId { get; set; }
        public ICollection<string> Categories { get; set; }
    }
}