using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Eventer.Utility.CustomLogger;
using Eventer.WindowsService.Service.Client.Interface;
using Eventer.WindowsService.Service.ClientModel.EventerApi;
using Eventer.WindowsService.Service.ClientModel.EventerApi.Token;
using Eventer.WindowsService.Service.ClientModel.WroclawGoApi;
using Eventer.WindowsService.Service.Infrasturcture.Interface;

namespace Eventer.WindowsService.Service.Client
{
    public class WroclawGoApiService : ServiceBase, IWroclawGoApiService
    {
        public WroclawGoApiService(IEventerApiService eventerApiService, IDateHelper dateHelper, ICustomLogger customLogger) 
            : base(customLogger)
        {
            EventerApiService = eventerApiService;
            DateHelper = dateHelper;
            CustomLogger = customLogger;
        }

        public async Task TakeEventsAndPostAsList(Token token)
        {
            CustomLogger.Log("infoLog", "Rozpoczęto zadanie z Wrocławia.");
            var dateFrom = DateHelper.GetEventDateFromString(DateFrom);
            var dateTo = DateHelper.GetEventDateFromString(DateTo);

            var cityId = await EventerApiService.GetCityId(token, "Wrocław");
            var allEventerEvents = await EventerApiService.GetEvents(token, cityId);

            var wroclawEventsToPost = new List<EventPostItem>();

            var wrocEvents = await GetEventsFromWroclawGoApi();

            var wroclawEventsToAdd = wrocEvents.Where(w => !allEventerEvents.Any(e => e.EventName.Equals(w.Offer.Title)
            && e.EventLocalization.Equals(w.PlaceName)
            && e.EventDate == DateHelper.GetEventDateFromString(w.StartDate)) && (DateHelper.GetEventDateFromString(w.StartDate) >= dateFrom 
            && DateHelper.GetEventDateFromString(w.StartDate) <= dateTo)).ToList();

            foreach (var @event in wroclawEventsToAdd)
            {
                var eventName = @event.Offer.Title;
                var eventDate = DateHelper.GetEventDateFromString(@event.StartDate);
                var eventLocalization = @event.PlaceName;
                var eventImage = @event.Offer.MainImage?.Standard;
                var eventUrl = @event.Offer.PageLink;
                var eventDescription = @event.Offer.LongDescription;
                var eventCategories = @event.Offer.Categories.Select(c => c.Name).ToList();

                var newEvent = new EventPostItem
                {
                    EventName = eventName,
                    EventDate = eventDate,
                    EventLocalization = eventLocalization,
                    EventImage = eventImage,
                    EventUrl = eventUrl,
                    EventDescription = eventDescription,
                    Categories = eventCategories,
                    CityId = cityId
                };

                wroclawEventsToPost.Add(newEvent);
            }

            var eventItemPostList = new EventItemPostList { Events = wroclawEventsToPost };
            var postEventsResult = await EventerApiService.PostEventAsList(token, eventItemPostList, "wrocEventLog");

            CustomLogger.Log("infoLog",
                postEventsResult
                    ? $"Zakończono zadanie z Wrocławia. Dodano {wroclawEventsToPost.Count} eventów."
                    : "Zakończono zadanie z Wrocławia. Nie dodano eventów.");
        }

        private async Task<IList<WroclawGoApiItem>> GetEventsFromWroclawGoApi()
        {
            var totalPage = 0;
            var events = new List<WroclawGoApiModel>();
            var eventsToAdd = new List<WroclawGoApiItem>();

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://go.wroclaw.pl/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string wroclawGoApiKey = ConfigurationManager.AppSettings["wroclawGoApiKey"];
                var response =
                    client.GetAsync(
                        $"api/v1.0/events?key={wroclawGoApiKey}&time-from={DateFrom}&time-to={DateTo}")
                        .Result;

                if (response.IsSuccessStatusCode)
                {
                    var @event = await response.Content.ReadAsAsync<WroclawGoApiModel>();
                    totalPage = @event.ListSize / @event.PageSize;
                }
                else
                    CustomLogger.Log("errorLog", "Wystąpił błąd przy próbie połączenia z serwisem WroclawGoApi.");

                for (var i = 0; i <= totalPage; i++)
                {
                    var response2 =
                        client.GetAsync(
                            $"api/v1.0/events?key={wroclawGoApiKey}&time-from={DateFrom}&time-to={DateTo}&page={i}")
                            .Result;

                    if (response2.IsSuccessStatusCode)
                    {
                        events.Add(await response2.Content.ReadAsAsync<WroclawGoApiModel>());
                    }
                }

                foreach (var @event in events)
                {
                    eventsToAdd.AddRange(@event.WroclawGoApiItems.Where(item => !item.Offer.Type.Name.Contains("Kino")));
                }
            }

            return eventsToAdd;
        }
    }
}