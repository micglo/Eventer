using System.Xml.Serialization;

namespace Eventer.WindowsService.Service.ClientModel.PoznanApi
{
    public class Version
    {
        [XmlElement("evtml_name")]
        public string EventName { get; set; }

        [XmlElement("evtml_desc")]
        public string EventDescription { get; set; }
    }
}