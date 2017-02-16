using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Mapper.ModelFacotry.RefreshToken;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.RefreshToken;
using Eventer.Repository.UoW;
using Eventer.Service.Common;
using Eventer.Service.RefreshToken.Interface;
using Eventer.Utility.CustomException;
using Eventer.Utility.HashGenerator;
using Ninject;

namespace Eventer.Service.RefreshToken
{
    public class RefreshTokenService : ServiceBase<RefreshTokenDto, RefreshTokenDto, RefreshTokenDto>, IRefreshTokenService
    {
        public RefreshTokenService()
        {
            UnitOfWork = new UnitOfWork();
            ModelFactory = new RefreshTokenFactory();    
            Generator = new Generator();
        }

        public RefreshTokenService(IUnitOfWork unitOfWork, [Named("RefreshTokenFactory")] IModelFactory modelFactory, IGenerator generator, 
            ICustomException customException, HttpRequestMessage request) 
            : base(unitOfWork, modelFactory, customException, request)
        {
            Generator = generator;
        }

        public override async Task<PagedItems<RefreshTokenDto>> GetAllAsync(string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var tokens = await UnitOfWork.RefreshTokenRepository.GetAllAsync(x => x.OrderBy(y => y.Id), skipAmount, intTake);
            var tokensDto = tokens.Select(ModelFactory.GetModel<RefreshTokenDto>).ToList();
            var tokensCount = await UnitOfWork.RefreshTokenRepository.Count();

            return CreatePagedItems(tokensDto, "RefreshTokenRoute", intSkip, intTake, tokensCount);
        }

        public override async Task<RefreshTokenDto> GetByIdAsync(object id)
        {
            var hashedTokenId = GetHash((string)id);
            var token = await UnitOfWork.RefreshTokenRepository.FindAsync(hashedTokenId);
            return ModelFactory.GetModel<RefreshTokenDto>(token);
        }

        public override async Task<RefreshTokenDto> AddAsync(RefreshTokenDto dtoModel)
        {
            var hashedTokenId = GetHash(dtoModel.Id);
            var tokenDomain = ModelFactory.GetModel<Domain.Entity.RefreshToken.RefreshToken>(dtoModel);
            var existingToken = await UnitOfWork.RefreshTokenRepository
                .SingleOrDefaultAsync(x => x.Subject == tokenDomain.Subject && x.ClientId == tokenDomain.ClientId);

            if (existingToken != null)
            {
                await UnitOfWork.RefreshTokenRepository.RemoveAsync(existingToken);
            }

            tokenDomain.Id = hashedTokenId;
            var newEntity = await UnitOfWork.RefreshTokenRepository.AddAsync(tokenDomain);

            return ModelFactory.GetModel<RefreshTokenDto>(newEntity);
        }

        public override async Task<RefreshTokenDto> Update(RefreshTokenDto dtoModel)
        {
            var hashedTokenId = GetHash(dtoModel.Id);
            var tokenDomain = ModelFactory.GetModel<Domain.Entity.RefreshToken.RefreshToken>(dtoModel);
            tokenDomain.Id = hashedTokenId;
            await UnitOfWork.RefreshTokenRepository.Update(tokenDomain);

            return await GetByIdAsync(dtoModel.Id);
        }

        public override async Task RemoveAsync(object id)
        {
            var tokenDomain = await UnitOfWork.RefreshTokenRepository.SingleOrDefaultAsync(t=>t.Id == (string)id);
            await UnitOfWork.RefreshTokenRepository.RemoveAsync(tokenDomain);
        }

        public string GenerateRefreshTokenId() => Generator.GenerateGuid();

        public async Task<RefreshTokenDto> GetByClientIdAsync(object clientId)
        {
            if (!await ExistsByClientIdAsync(clientId))
                CustomException.ThrowNotFoundException($"There is no refresh token with clientId = {clientId}.");

            var token = await UnitOfWork.RefreshTokenRepository.SingleOrDefaultAsync(x => x.ClientId == (string)clientId);
            return ModelFactory.GetModel<RefreshTokenDto>(token);
        }

        public async Task<bool> Exists(string id)
            => await UnitOfWork.RefreshTokenRepository.AnyAsync(x => x.Id.Equals(id));


        #region Helpers

        protected override async Task<bool> ExistsAsync(object id)
            => await UnitOfWork.RefreshTokenRepository.AnyAsync(x => x.Id == (string)id);

        private async Task<bool> ExistsByClientIdAsync(object clientId)
            => await UnitOfWork.RefreshTokenRepository.AnyAsync(x => x.ClientId == (string)clientId);

        private string GetHash(string input) => Generator.GetHash(input);

        #endregion

    }
}