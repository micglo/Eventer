using System.Collections.Generic;
using System.Xml.Serialization;

namespace Eventer.WindowsService.Service.ClientModel.PoznanApi
{
    [XmlRoot("root")]
    public class PoznanApiModel
    {
        [XmlElement("event")]
        public List<PoznanApiEvent> Event { get; set; }
    }
}