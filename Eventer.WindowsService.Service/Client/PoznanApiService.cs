using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Eventer.Utility.CustomLogger;
using Eventer.WindowsService.Service.Client.Interface;
using Eventer.WindowsService.Service.ClientModel.EventerApi;
using Eventer.WindowsService.Service.ClientModel.EventerApi.Token;
using Eventer.WindowsService.Service.ClientModel.PoznanApi;
using Eventer.WindowsService.Service.Infrasturcture.Interface;

namespace Eventer.WindowsService.Service.Client
{
    public class PoznanApiService : ServiceBase, IPoznanApiService
    {
        public PoznanApiService(ICustomLogger customLogger, IEventerApiService eventerApiService, IDateHelper dateHelper) : base(customLogger)
        {
            EventerApiService = eventerApiService;
            DateHelper = dateHelper;
        }

        public async Task TakeEventsAndPostAsList(Token token)
        {
            CustomLogger.Log("infoLog", "Rozpoczęto zadanie z Poznania.");
            var dateFrom = DateHelper.GetEventDateFromString(DateFrom);
            var dateTo = DateHelper.GetEventDateFromString(DateTo);
            
            var cityId = await EventerApiService.GetCityId(token, "Poznań");
            var allEventerEvents = await EventerApiService.GetEvents(token, cityId);

            var poznanEventsToAdd = new List<PoznanApiEvent>();
            var poznanEventsToPost = new List<EventPostItem>();

            var poznanEvents = await GetEventsFromPoznanApi();

            foreach (var @event in poznanEvents.Event)
            {
                var latestEventVersion = @event.EventVersion.Last();
                var latestVersion = latestEventVersion.Version.Last();
                var latestAddress = @event.EventAddress.Last();

                var poznanEventDate = DateHelper.GetEventDateFromString(@event.EventDate);

                if (!(poznanEventDate >= dateFrom && poznanEventDate <= dateTo)) continue;

                if (allEventerEvents.Any(x => x.EventName.Equals(latestVersion.EventName)
                && x.EventDate == poznanEventDate
                && x.EventLocalization.Equals(latestAddress.EventLocalization))) continue;

                if (poznanEventsToAdd.Contains(@event)) continue;

                poznanEventsToAdd.Add(@event);
            }

            foreach (var @event in poznanEventsToAdd)
            {
                var latestEventVersion = @event.EventVersion.Last();
                var latestVersion = latestEventVersion.Version.Last();
                var latestAddress = @event.EventAddress.Last();

                var eventName = latestVersion.EventName;
                var eventDate = DateHelper.GetEventDateFromString(@event.EventDate);
                var eventLocalization = latestAddress.EventLocalization;
                var eventUrl = @event.EventUrl;
                var eventDescription = latestVersion.EventDescription;

                var categories = new List<string>
                {
                    @event.EventCategory
                };

                var newEvent = new EventPostItem
                {
                    EventName = eventName,
                    EventDate = eventDate,
                    EventLocalization = eventLocalization,
                    EventUrl = eventUrl,
                    EventDescription = eventDescription,
                    Categories = categories,
                    CityId = cityId
                };

                poznanEventsToPost.Add(newEvent);
            }

            var eventItemPostList = new EventItemPostList { Events = poznanEventsToPost };
            var postEventsResult = await EventerApiService.PostEventAsList(token, eventItemPostList, "poznEventLog");

            CustomLogger.Log("infoLog",
                postEventsResult
                    ? $"Zakończono zadanie z Poznania. Dodano {poznanEventsToPost.Count} eventów."
                    : "Zakończono zadanie z Poznania. Nie dodano eventów.");
        }

        private async Task<PoznanApiModel> GetEventsFromPoznanApi()
        {
            using (var client = new HttpClient())
            {
                var poznanEvents = new PoznanApiModel();

                client.BaseAddress = new Uri("http://www.poznan.pl/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));

                var response = await client.GetAsync($"mim/public/ws-information/?co=getEvents&dateTo={DateTo}&dateFrom={DateFrom}");

                if (response.IsSuccessStatusCode)
                {
                    var xmlContent = response.Content.ReadAsStreamAsync().Result;
                    var deserializer = new XmlSerializer(typeof(PoznanApiModel));
                    var obj = deserializer.Deserialize(xmlContent);
                    poznanEvents = (PoznanApiModel)obj;
                }
                else
                    CustomLogger.Log("errorLog", "Wystąpił błąd przy próbie połączenia z serwisem PoznanApi.");

                return poznanEvents;
            }
        }
    }
}