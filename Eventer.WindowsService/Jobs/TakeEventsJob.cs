using Eventer.WindowsService.Service.Client.Interface;
using Eventer.WindowsService.Service.ClientModel.EventerApi.Token;
using Quartz;

namespace Eventer.WindowsService.Jobs
{
    public class TakeEventsJob : IJob
    {
        private readonly IWroclawGoApiService _wroclawGoApiservice;
        private readonly IEventerApiService _eventerApiService;
        private readonly IPoznanApiService _poznanApiService;
        
        public TakeEventsJob(IWroclawGoApiService wroclawGoApiservice, IEventerApiService eventerApiService, IPoznanApiService poznanApiService)
        {
            _wroclawGoApiservice = wroclawGoApiservice;
            _eventerApiService = eventerApiService;
            _poznanApiService = poznanApiService;
        }

        public async void Execute(IJobExecutionContext context)
        {
            if (TokenPersister.Token == null)
            {
                TokenPersister.Token = await _eventerApiService.GetToken();
            }
            TokenPersister.Token = await _eventerApiService.GetTokenByRefreshToken(TokenPersister.Token.RefreshToken);
            await _wroclawGoApiservice.TakeEventsAndPostAsList(TokenPersister.Token);
            await _poznanApiService.TakeEventsAndPostAsList(TokenPersister.Token);
        }
    }
}