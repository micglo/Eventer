using System.Collections.Generic;
using System.Xml.Serialization;

namespace Eventer.WindowsService.Service.ClientModel.PoznanApi
{
    public class EventVersion
    {
        [XmlElement("version")]
        public List<Version> Version { get; set; }
    }
}