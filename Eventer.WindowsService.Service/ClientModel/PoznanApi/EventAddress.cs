using System.Xml.Serialization;

namespace Eventer.WindowsService.Service.ClientModel.PoznanApi
{
    public class EventAddress
    {
        [XmlElement("street")]
        public string EventLocalization { get; set; }
    }
}