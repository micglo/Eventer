using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Eventer.Domain.Entity.EventerEntity;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.State;
using Eventer.Repository.UoW;
using Eventer.Service.Common;
using Eventer.Service.EventerService.Interface;
using Eventer.Utility.CustomException;
using Ninject;

namespace Eventer.Service.EventerService
{
    public class StateService : ServiceBase<StateDto, StatePostDto, StatePutDto>, IStateService
    {
        public StateService(IUnitOfWork unitOfWork, [Named("StateFactory")] IModelFactory modelFactory, ICustomException customException, 
            HttpRequestMessage request) 
            : base(unitOfWork, modelFactory, customException, request)
        {
        }

        public override async Task<PagedItems<StateDto>> GetAllAsync(string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var states = await UnitOfWork.StateRepository.GetAllAsync(x => x.OrderBy(y => y.StateName), skipAmount, intTake);
            var statesDto = states.Select(ModelFactory.GetModel<StateDto>).ToList();
            var statesCount = await UnitOfWork.StateRepository.Count();

            return CreatePagedItems(statesDto, "StateRoute", intSkip, intTake, statesCount);
        }

        public override async Task<StateDto> GetByIdAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no state with id = {id}.");

            var state = await UnitOfWork.StateRepository.FindAsync(id);
            return ModelFactory.GetModel<StateDto>(state);
        }

        public override async Task<StateDto> AddAsync(StatePostDto dtoModel)
        {
            var stateDomain = ModelFactory.GetModel<State>(dtoModel);
            var newEntity = await UnitOfWork.StateRepository.AddAsync(stateDomain);
            return ModelFactory.GetModel<StateDto>(newEntity);
        }

        public override async Task<StateDto> Update(StatePutDto dtoModel)
        {
            if (!await ExistsAsync(dtoModel.Id))
                CustomException.ThrowNotFoundException($"State with id: {dtoModel.Id} doesn't exist.");

            var stateDomain = ModelFactory.GetModel<State>(dtoModel);
            await UnitOfWork.StateRepository.Update(stateDomain);

            return await GetByIdAsync(dtoModel.Id);
        }

        public override async Task RemoveAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no state with id = {id}.");

            var stateDomain = await UnitOfWork.StateRepository.FindAsync(id);
            await UnitOfWork.StateRepository.RemoveAsync(stateDomain);
        }

        public async Task<StateDto> GetByNameAsync(string stateName)
        {
            if (!await ExistsByNameAsync(stateName))
                CustomException.ThrowNotFoundException($"There is no state with name {stateName}.");

            var state = await UnitOfWork.StateRepository.SingleOrDefaultAsync(x=>x.StateName.Equals(stateName));
            return ModelFactory.GetModel<StateDto>(state);
        }

        #region Helpers

        protected override async Task<bool> ExistsAsync(object id)
            => await UnitOfWork.StateRepository.AnyAsync(x => x.Id.Equals((int)id));

        private async Task<bool> ExistsByNameAsync(string stateName)
            => await UnitOfWork.StateRepository.AnyAsync(x => x.StateName.Equals(stateName));

        #endregion
    }
}