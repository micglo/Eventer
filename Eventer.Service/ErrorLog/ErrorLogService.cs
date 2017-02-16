using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.ErrorLog;
using Eventer.Repository.UoW;
using Eventer.Service.Common;
using Eventer.Service.ErrorLog.Interface;
using Eventer.Utility.CustomException;
using Ninject;

namespace Eventer.Service.ErrorLog
{
    public class ErrorLogService : ServiceBase<ErrorLogDto, ErrorLogDto, ErrorLogDto>, IErrorLogService
    {
        public ErrorLogService(IUnitOfWork unitOfWork, [Named("ErrorLogFactory")] IModelFactory modelFactory, 
            ICustomException customException, HttpRequestMessage request) 
            : base(unitOfWork, modelFactory, customException, request)
        {
        }

        public override async Task<PagedItems<ErrorLogDto>> GetAllAsync(string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);
            
            var logs = await UnitOfWork.ErrorLogRepository.GetAllAsync(x => x.OrderBy(y => y.Id), skipAmount, intTake);
            var logsDto = logs.Select(ModelFactory.GetModel<ErrorLogDto>).ToList();
            var logsCount = await UnitOfWork.ErrorLogRepository.Count();

            return CreatePagedItems(logsDto, "ErrorLogRoute", intSkip, intTake, logsCount);
        }

        public override async Task<ErrorLogDto> GetByIdAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no log with id = {id}.");

            var log = await UnitOfWork.ErrorLogRepository.FindAsync((long)id);
            return ModelFactory.GetModel<ErrorLogDto>(log);
        }

        public override async Task<ErrorLogDto> AddAsync(ErrorLogDto dtoModel)
        {
            var logDomain = ModelFactory.GetModel<Domain.Entity.ErrorLog.ErrorLog>(dtoModel);
            var newEntity = await UnitOfWork.ErrorLogRepository.AddAsync(logDomain);
            return ModelFactory.GetModel<ErrorLogDto>(newEntity);
        }

        public override async Task<ErrorLogDto> Update(ErrorLogDto dtoModel)
        {
            var logDomain = ModelFactory.GetModel<Domain.Entity.ErrorLog.ErrorLog>(dtoModel);
            await UnitOfWork.ErrorLogRepository.Update(logDomain);

            return await GetByIdAsync(dtoModel.Id);
        }

        public override async Task RemoveAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no log with id = {id}.");

            var logDomain = await UnitOfWork.ErrorLogRepository.FindAsync(id);
            await UnitOfWork.ErrorLogRepository.RemoveAsync(logDomain);
        }

        public async Task<long> GetCountByUserAsync(string userName)
        {
            var logs = await UnitOfWork.ErrorLogRepository.FindAllAsync(x => x.UserName.Equals(userName));
            return logs.Count();
        }

        public async Task<PagedItems<ErrorLogDto>> GetAllByUserAsync(string userName, string skip, string take)
        {
            if (!await ExistsByUserAsync(userName))
                CustomException.ThrowNotFoundException($"There are no logs asociated with user {userName}.");

            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var logs = await UnitOfWork.ErrorLogRepository.FindAllAsync(x => x.UserName.Equals(userName), x => x.OrderBy(y => y.ErrorDateTime), skipAmount, intTake);
            var logsDto = logs.Select(ModelFactory.GetModel<ErrorLogDto>).ToList();
            var logsCount = await UnitOfWork.ErrorLogRepository.Count(x => x.UserName.Equals(userName));

            return CreatePagedItems(logsDto, "UserErrorLogsRoute", intSkip, intTake, logsCount);
        }

        public async Task RemoveAllAsync()
        {
            var logs = await UnitOfWork.ErrorLogRepository.GetAllAsync();

            foreach (var log in logs)
            {
                await UnitOfWork.ErrorLogRepository.RemoveAsync(log);
            }
        }

        #region Helpers

        protected override async Task<bool> ExistsAsync(object id)
            => await UnitOfWork.ErrorLogRepository.AnyAsync(x => x.Id == (long)id);

        private async Task<bool> ExistsByUserAsync(string userName)
            => await UnitOfWork.ErrorLogRepository.AnyAsync(x => x.UserName.Equals(userName));

        #endregion
    }
}