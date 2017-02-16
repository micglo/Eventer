using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Eventer.Domain.Entity.ApiActivity;
using Eventer.Mapper.ModelFacotry.ApiActivity;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.ApiActivity;
using Eventer.Repository.UoW;
using Eventer.Service.ApiActivity.Interface;
using Eventer.Service.Common;
using Eventer.Utility.CustomException;
using Eventer.Utility.CustomLogger;
using Ninject;

namespace Eventer.Service.ApiActivity
{
    public class ApiActivityLogService : ServiceBase<ApiActivityLogDto, ApiActivityLogDto, ApiActivityLogDto>, IApiActivityLogService
    {
        private readonly ICustomLogger _customLogger;
        public ApiActivityLogService()
        {
            UnitOfWork = new UnitOfWork();
            ModelFactory = new ApiActivityLogFactory();
            _customLogger = new CustomLogger();
        }

        public ApiActivityLogService(IUnitOfWork unitOfWork, [Named("ApiActivityLogFactory")] IModelFactory modelFactory, 
            ICustomException customException, HttpRequestMessage request) 
            : base(unitOfWork, modelFactory, customException, request)
        {
        }

        public override async Task<PagedItems<ApiActivityLogDto>> GetAllAsync(string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var logs = await UnitOfWork.ActivityLogRepository.GetAllAsync(x => x.OrderBy(y => y.Id), skipAmount, intTake);
            var logsDto = logs.Select(ModelFactory.GetModel<ApiActivityLogDto>).ToList();
            var logsCount = await UnitOfWork.ActivityLogRepository.Count();

            return CreatePagedItems(logsDto, "ApiActivityLogsRoute", intSkip, intTake, logsCount);
        }

        public override async Task<ApiActivityLogDto> GetByIdAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no log with id = {id}.");

            var apiLogEntry = await UnitOfWork.ActivityLogRepository.FindAsync((long)id);
            return ModelFactory.GetModel<ApiActivityLogDto>(apiLogEntry);
        }

        //Poki co za duzo danych jak na darmowa bazkę na azure
        //public ApiActivityLogDto Add(ApiActivityLogDto dtoModel)
        //{
        //    var apiLogEntryDomain = ModelFactory.GetModel<ApiActivityLog>(dtoModel);
        //    var newEntity = UnitOfWork.ActivityLogRepository.Add(apiLogEntryDomain);
        //    _customLogger.Log(dtoModel);
        //    return ModelFactory.GetModel<ApiActivityLogDto>(newEntity);
        //}
        public void Add(ApiActivityLogDto dtoModel)
            => _customLogger.Log(dtoModel);

        public override async Task<ApiActivityLogDto> AddAsync(ApiActivityLogDto dtoModel)
        {
            var apiLogEntryDomain = ModelFactory.GetModel<ApiActivityLog>(dtoModel);
            var newEntity = await UnitOfWork.ActivityLogRepository.AddAsync(apiLogEntryDomain);
            return ModelFactory.GetModel<ApiActivityLogDto>(newEntity);
        }

        public override async Task<ApiActivityLogDto> Update(ApiActivityLogDto dtoModel)
        {
            var apiLogEntryDomain = ModelFactory.GetModel<ApiActivityLog>(dtoModel);
            await UnitOfWork.ActivityLogRepository.Update(apiLogEntryDomain);

            return await GetByIdAsync(dtoModel.Id);
        }

        public override async Task RemoveAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no log with id = {id}.");

            var apiLogEntryDomain = await UnitOfWork.ActivityLogRepository.FindAsync(id);
            await UnitOfWork.ActivityLogRepository.RemoveAsync(apiLogEntryDomain);
        }

        public async Task<PagedItems<ApiActivityLogDto>> GetAllByUserAsync(string userName, string skip, string take)
        {
            if (!await ExistsByUserAsync(userName))
                CustomException.ThrowNotFoundException($"There are no logs asociated with user {userName}.");

            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var logs = await UnitOfWork.ActivityLogRepository.FindAllAsync(x => x.User.Equals(userName), x => x.OrderBy(y => y.RequestTimestamp), skipAmount, intTake);
            var logsDto = logs.Select(ModelFactory.GetModel<ApiActivityLogDto>).ToList();
            var logsCount = await UnitOfWork.ActivityLogRepository.Count();

            return CreatePagedItems(logsDto, "ApiActivityLogsByUserRoute", intSkip, intTake, logsCount);
        }

        public async Task<PagedItems<ApiActivityLogDto>> GetAllByHostAddressAsync(string hostAddress, string skip, string take)
        {
            if (!await ExistsByHostAddressAsync(hostAddress))
                CustomException.ThrowNotFoundException($"There are no logs asociated with host address {hostAddress}.");

            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var logs = await UnitOfWork.ActivityLogRepository.FindAllAsync(x => x.UserHostAddress.Equals(hostAddress), x => x.OrderBy(y => y.RequestTimestamp), skipAmount, intTake);
            var logsDto = logs.Select(ModelFactory.GetModel<ApiActivityLogDto>).ToList();
            var logsCount = await UnitOfWork.ActivityLogRepository.Count();

            return CreatePagedItems(logsDto, "ApiActivityLogsByHostRoute", intSkip, intTake, logsCount);
        }

        public async Task<long> GetCountByUserAsync(string userName)
        {
            var logs = await UnitOfWork.ActivityLogRepository.FindAllAsync(x => x.User.Equals(userName));
            return logs.Count();
        }

        public async Task<long> GetCountByHostAddressAsync(string hostAddress)
        {
            var logs = await UnitOfWork.ActivityLogRepository.FindAllAsync(x => x.UserHostAddress.Equals(hostAddress));
            return logs.Count();
        }

        public async Task RemoveAllAsync()
        {
            var logs = await UnitOfWork.ActivityLogRepository.GetAllAsync();

            foreach (var log in logs)
            {
                await UnitOfWork.ActivityLogRepository.RemoveAsync(log);
            }
        }


        #region Helpers

        protected override async Task<bool> ExistsAsync(object id)
            => await UnitOfWork.ActivityLogRepository.AnyAsync(x => x.Id.Equals((long)id));

        private async Task<bool> ExistsByUserAsync(string userName)
            => await UnitOfWork.ActivityLogRepository.AnyAsync(x => x.User.Equals(userName));

        private async Task<bool> ExistsByHostAddressAsync(string hostAddress)
            => await UnitOfWork.ActivityLogRepository.AnyAsync(x => x.UserHostAddress.Equals(hostAddress));

        #endregion
    }
}