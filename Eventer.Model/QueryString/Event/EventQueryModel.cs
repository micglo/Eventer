namespace Eventer.Model.QueryString.Event
{
    public class EventQueryModel : Pagination.Pagination
    {
        public string CityId { get; set; } = null;

        public string CityName { get; set; } = null;

        public string EventName { get; set; } = null;

        public string CategoryId { get; set; } = null;

        public string CategoryName { get; set; } = null;

        public string Date { get; set; } = null;

        public string DateFrom { get; set; } = null;

        public string DateTo { get; set; } = null;
    }
}