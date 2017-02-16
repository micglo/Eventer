using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Eventer.Utility.CustomLogger;
using Eventer.WindowsService.Service.Client.Interface;
using Eventer.WindowsService.Service.ClientModel.EventerApi;
using Eventer.WindowsService.Service.ClientModel.EventerApi.Token;

namespace Eventer.WindowsService.Service.Client
{
    public class EventerApiService : ServiceBase, IEventerApiService
    {
        public EventerApiService(ICustomLogger customLogger) : base(customLogger)
        {
            CustomLogger = customLogger;
        }

        public async Task<bool> PostEventAsList(Token token, EventItemPostList events, string loggerName)
        {
            var client = ConnectionToEventerApi();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType,
                token.AccessToken);

            var result = await client.PostAsJsonAsync("api/v1/events/PostEventList", events);
            var statusCode = result.StatusCode;
            var content = result.Content;
            var test = result.IsSuccessStatusCode;
            client.Dispose();

            if (test)
            {
                foreach (var @event in events.Events)
                {
                    var msg = $"Dodano event: {@event.EventName}.";
                    CustomLogger.Log(loggerName, msg);
                }
            }
            else
                CustomLogger.Log("errorLog", $"Wystąpił błąd przy próbie wysłania eventów do Eventera: {statusCode}.");

            return test;
        }

        public async Task<IList<EventItem>> GetEvents(Token token, int cityId)
        {
            int totalNumberOfPages = 1;
            var events = new List<Event>();
            var eventItems = new List<EventItem>();
            var client = ConnectionToEventerApi();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType,
                token.AccessToken);

            var response = await client.GetAsync($"api/v1/events?cityId={cityId}&datefrom={DateFrom}&dateto={DateTo}");

            if (response.IsSuccessStatusCode)
            {
                var @event = await response.Content.ReadAsAsync<Event>();
                totalNumberOfPages = @event.TotalNumberOfPages;
            }
            else
                CustomLogger.Log("errorLog", "Wystąpił błąd przy próbie pobrania eventów z Eventera.");

            for (var i = 1; i <= totalNumberOfPages; i++)
            {
                var response2 = await client.GetAsync($"api/v1/events?cityId={cityId}&datefrom={DateFrom}&dateto={DateTo}&page={i}");
                if (response2.IsSuccessStatusCode)
                {
                    events.Add(await response2.Content.ReadAsAsync<Event>());
                }
            }

            client.Dispose();

            foreach (var @event in events)
            {
                eventItems.AddRange(@event.EventItems);
            }

            return eventItems;
        }

        public async Task<int> GetCityId(Token token, string cityName)
        {
            var city = new City();
            var client = ConnectionToEventerApi();

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType,
                token.AccessToken);
            var response = await client.GetAsync($"api/v1/cities/{cityName}");

            if (response.IsSuccessStatusCode)
            {
                city = await response.Content.ReadAsAsync<City>();
            }
            else
                CustomLogger.Log("errorLog", "Wystąpił błąd przy próbie pobrania cityId z Eventera.");

            client.Dispose();

            return city.Id;
        }

        public async Task<Token> GetTokenByRefreshToken(string refreshToken)
        {
            string grandType = ConfigurationManager.AppSettings["grandTypeRefreshToken"];
            string clientId = ConfigurationManager.AppSettings["clientId"];
            string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
            //string clientId = ConfigurationManager.AppSettings["clientIdLocal"];
            //string clientSecret = ConfigurationManager.AppSettings["clientSecretLocal"];

            var tokenCredential = new TokenCredential(grandType, clientId, clientSecret);
            var token = new Token();

            var formUrlEncodedContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", tokenCredential.GrantType),
                new KeyValuePair<string, string>("refresh_token", refreshToken),
                new KeyValuePair<string, string>("client_id", tokenCredential.ClientId),
                new KeyValuePair<string, string>("client_secret", tokenCredential.ClientSecret)
            });

            var client = ConnectionToEventerApi();
            var response = await client.PostAsync("token", formUrlEncodedContent);

            if (response.IsSuccessStatusCode)
            {
                token = await response.Content.ReadAsAsync<Token>();
            }
            else
            {
                token = await GetToken();
            }

            client.Dispose();

            return token;
        }

        public async Task<Token> GetToken()
        {
            string grandType = ConfigurationManager.AppSettings["grandTypePassword"];
            string userName = ConfigurationManager.AppSettings["userName"];
            string password = ConfigurationManager.AppSettings["password"];
            string clientId = ConfigurationManager.AppSettings["clientId"];
            string clientSecret = ConfigurationManager.AppSettings["clientSecret"];
            //string userName = ConfigurationManager.AppSettings["userNameLocal"];
            //string password = ConfigurationManager.AppSettings["passwordLocal"];
            //string clientId = ConfigurationManager.AppSettings["clientIdLocal"];
            //string clientSecret = ConfigurationManager.AppSettings["clientSecretLocal"];

            var tokenCredential = new TokenCredential(grandType, userName, password, clientId, clientSecret);
            var token = new Token();

            var formUrlEncodedContent = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", tokenCredential.GrantType),
                new KeyValuePair<string, string>("username", tokenCredential.Username),
                new KeyValuePair<string, string>("password", tokenCredential.Password),
                new KeyValuePair<string, string>("client_id", tokenCredential.ClientId),
                new KeyValuePair<string, string>("client_secret", tokenCredential.ClientSecret)
            });

            var client = ConnectionToEventerApi();
            var response = await client.PostAsync("token", formUrlEncodedContent);

            if (response.IsSuccessStatusCode)
            {
                token = await response.Content.ReadAsAsync<Token>();
            }
            else
                CustomLogger.Log("errorLog", "Wystąpił błąd przy próbie pobrania tokenu.");

            client.Dispose();

            return token;
        }

        private static HttpClient ConnectionToEventerApi()
        {
            var client = new HttpClient { BaseAddress = new Uri("https://eventerapi.azurewebsites.net/") };
            //var client = new HttpClient { BaseAddress = new Uri("https://localhost:44361/") };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}