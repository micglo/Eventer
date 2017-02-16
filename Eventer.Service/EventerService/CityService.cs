using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Eventer.Domain.Entity.EventerEntity;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.City;
using Eventer.Repository.UoW;
using Eventer.Service.Common;
using Eventer.Service.EventerService.Interface;
using Eventer.Utility.CustomException;
using Ninject;

namespace Eventer.Service.EventerService
{
    public class CityService : ServiceBase<CityDto, CityPostDto, CityPutDto>, ICityService
    {
        public CityService(IUnitOfWork unitOfWork, [Named("CityFactory")]IModelFactory modelFactory, ICustomException customException, HttpRequestMessage request)
            : base(unitOfWork, modelFactory, customException, request)
        {
            
        }

        public override async Task<PagedItems<CityDto>> GetAllAsync(string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake*(intSkip - 1);

            var cities = await UnitOfWork.CityRepository.GetAllAsync(x => x.OrderBy(y => y.CityName), skipAmount, intTake);
            var citiesDto = cities.Select(ModelFactory.GetModel<CityDto>).ToList();
            var citiesCount = await UnitOfWork.CityRepository.Count();

            return CreatePagedItems(citiesDto, "CityRoute", intSkip, intTake, citiesCount);
        }

        public override async Task<CityDto> GetByIdAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no city with id = {id}.");

            var city = await UnitOfWork.CityRepository.FindAsync(id);
            return ModelFactory.GetModel<CityDto>(city);
        }

        public override async Task<CityDto> AddAsync(CityPostDto dtoModel)
        {
            if (!await UnitOfWork.StateRepository.AnyAsync(x => x.Id == dtoModel.StateId))
                CustomException.ThrowNotFoundException($"State with id = {dtoModel.StateId} doesn't exist.");

            var cityDomain = ModelFactory.GetModel<City>(dtoModel);
            var newEntity = await UnitOfWork.CityRepository.AddAsync(cityDomain);
            return await GetByIdAsync(newEntity.Id);
        }

        public override async Task<CityDto> Update(CityPutDto dtoModel)
        {
            if (!await ExistsAsync(dtoModel.Id))
                CustomException.ThrowNotFoundException($"There is no city with id = {dtoModel.Id}.");

            if (!await UnitOfWork.StateRepository.AnyAsync(x=>x.Id == dtoModel.StateId))
                CustomException.ThrowNotFoundException($"State with id = {dtoModel.StateId} doesn't exist.");

            var cityDomain = ModelFactory.GetModel<City>(dtoModel);
            await UnitOfWork.CityRepository.Update(cityDomain);

            return await GetByIdAsync(dtoModel.Id);
        }

        public override async Task RemoveAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no city with id = {id}.");

            var cityDomain = await UnitOfWork.CityRepository.FindAsync(id);
            await UnitOfWork.CityRepository.RemoveAsync(cityDomain);
        }

        public async Task<CityDto> GetByCityNameAsync(string cityName)
        {
            if (!await ExistsAsync(cityName))
                CustomException.ThrowNotFoundException($"There is no city with cityName = {cityName}.");

            var city = await UnitOfWork.CityRepository.SingleOrDefaultAsync(x=>x.CityName.Equals(cityName));
            return ModelFactory.GetModel<CityDto>(city);
        }

        #region Helpers

        protected override async Task<bool> ExistsAsync(object id)
            => await UnitOfWork.CityRepository.AnyAsync(x => x.Id.Equals((int)id));

        private async Task<bool> ExistsAsync(string cityName)
            => await UnitOfWork.CityRepository.AnyAsync(x => x.CityName.Equals(cityName));

        #endregion
    }
}