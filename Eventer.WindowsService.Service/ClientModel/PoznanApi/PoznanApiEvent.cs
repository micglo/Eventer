using System.Collections.Generic;
using System.Xml.Serialization;

namespace Eventer.WindowsService.Service.ClientModel.PoznanApi
{
    public class PoznanApiEvent
    {
        [XmlElement("event_version")]
        public List<EventVersion> EventVersion { get; set; }

        [XmlElement("event_address")]
        public List<EventAddress> EventAddress { get; set; }

        [XmlElement("event_start")]
        public string EventDate { get; set; }

        [XmlElement("category")]
        public string EventCategory { get; set; }

        [XmlElement("event_url")]
        public string EventUrl { get; set; }
    }
}