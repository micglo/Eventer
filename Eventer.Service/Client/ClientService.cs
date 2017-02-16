using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Eventer.Domain.Entity.Client;
using Eventer.Mapper.ModelFacotry.Client;
using Eventer.Mapper.ModelFacotry.Common;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.Dto.Client;
using Eventer.Repository.UoW;
using Eventer.Service.Client.Interface;
using Eventer.Service.Common;
using Eventer.Utility.CustomException;
using Eventer.Utility.HashGenerator;
using Ninject;

namespace Eventer.Service.Client
{
    public class ClientService : ServiceBase<ClientDto, ClientPostDto, ClientDto>, IClientService
    {
        public ClientService()
        {
            UnitOfWork = new UnitOfWork();
            ModelFactory = new ClientFactory();
            Generator = new Generator();
        }

        public ClientService(IUnitOfWork unitOfWork, [Named("ClientFactory")] IModelFactory modelFactory, IGenerator generator, 
            ICustomException customException, HttpRequestMessage request) 
            : base(unitOfWork, modelFactory, customException, request)
        {
            Generator = generator;
        }

        public override async Task<PagedItems<ClientDto>> GetAllAsync(string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var clients = await UnitOfWork.ClientRepository.GetAllAsync(x => x.OrderBy(y => y.Id), skipAmount, intTake);
            var clientsDto = clients.Select(ModelFactory.GetModel<ClientDto>).ToList();
            var clientsCount = await UnitOfWork.ClientRepository.Count();

            return CreatePagedItems(clientsDto, "ClientRoute", intSkip, intTake, clientsCount);
        }

        public ClientDto GetById(object id)
        {
            var client = UnitOfWork.ClientRepository.Find(id);
            return ModelFactory.GetModel<ClientDto>(client);
        }

        public override async Task<ClientDto> GetByIdAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no client with clientId = {id}.");

            var client = await UnitOfWork.ClientRepository.FindAsync(id);
            return ModelFactory.GetModel<ClientDto>(client);
        }

        public bool Exists(object id)
            => UnitOfWork.ClientRepository.Any(x => x.Id.Equals((string)id));

        public override async Task<ClientDto> AddAsync(ClientPostDto dtoModel)
        {
            var clientDomain = ModelFactory.GetModel<Domain.Entity.Client.Client>(dtoModel);

            clientDomain.Id = GenerateClientId();

            //jesli zostal przypisany secret to jest js client i trzeba zhaszowac ten secret
            if (!string.IsNullOrEmpty(dtoModel.ClientSecret))
            {
                var hashedClientSecret = GetHash(dtoModel.ClientSecret);
                clientDomain.ClientSecret = hashedClientSecret;
            }
            
            var newEntity = await UnitOfWork.ClientRepository.AddAsync(clientDomain);
            return ModelFactory.GetModel<ClientDto>(newEntity);
        }

        public override async Task<ClientDto> Update(ClientDto dtoModel)
        {
            if (!await ExistsAsync(dtoModel.Id))
                CustomException.ThrowNotFoundException($"There is no client with clientId = {dtoModel.Id}.");

            var hashedClientSecret = GetHash(dtoModel.ClientSecret);
            var clientDomain = ModelFactory.GetModel<Domain.Entity.Client.Client>(dtoModel);
            clientDomain.ClientSecret = hashedClientSecret;
            await UnitOfWork.ClientRepository.Update(clientDomain);

            return await GetByIdAsync(dtoModel.Id);
        }

        public async Task<PagedItems<ClientDto>> GetUserClients(string userName, string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var clients = await UnitOfWork.ClientRepository.FindAllAsync(x=>x.Username.Equals(userName), x => x.OrderBy(y => y.Id), skipAmount, intTake);
            var clientsDto = clients.Select(ModelFactory.GetModel<ClientDto>).ToList();
            var clientsCount = await UnitOfWork.ClientRepository.Count(x => x.Username.Equals(userName));

            return CreatePagedItems(clientsDto, "UserClientsRoute", intSkip, intTake, clientsCount);
        }

        public async Task<ClientNoSecretDto> UpdateNoSecret(ClientNoSecretDto dtoModel)
        {
            if (!await ExistsAsync(dtoModel.Id))
                CustomException.ThrowNotFoundException($"There is no client with clientId = {dtoModel.Id}.");

            var client = await GetByIdAsync(dtoModel.Id);
            var clientSecret = client.ClientSecret;
            var clientDomain = ModelFactory.GetModel<Domain.Entity.Client.Client>(dtoModel);
            clientDomain.ClientSecret = clientSecret;
            await UnitOfWork.ClientRepository.Update(clientDomain);

            return ModelFactory.GetModel<ClientNoSecretDto>(clientDomain);
        }

        public override async Task RemoveAsync(object id)
        {
            if (!await ExistsAsync(id))
                CustomException.ThrowNotFoundException($"There is no client with clientId = {id}.");

            var clientDomain = await UnitOfWork.ClientRepository.FindAsync(id);
            await UnitOfWork.ClientRepository.RemoveAsync(clientDomain);
        }

        public string GenerateClientId() => Generator.GenerateGuid();

        public string GenerateClientSecret() => Generator.GenerateClientSecret();

        public string GetHash(string input) => Generator.GetHash(input);

        public async Task<long> MyClientsCount(string userName)
            => await UnitOfWork.ClientRepository.Count(x => x.Username.Equals(userName) && x.Active);

        public async Task<PagedItems<ClientByUserNameDto>> GetAllByUserName(string userName, string skip, string take)
        {
            var intSkip = int.Parse(skip);
            var intTake = int.Parse(take);
            var skipAmount = intTake * (intSkip - 1);

            var clients = await UnitOfWork.ClientRepository.FindAllAsync(x => x.Username.Equals(userName) && x.Active, x => x.OrderBy(y => y.Id), skipAmount, intTake);
            var clientsDto = clients.Select(ModelFactory.GetModel<ClientByUserNameDto>).ToList();
            var clientsCount = await UnitOfWork.ClientRepository.Count(x => x.Username.Equals(userName));

            return CreatePagedItems(clientsDto, "GetMyClientsRoute", intSkip, intTake, clientsCount);
        }

        public async Task<bool> CheckUserClient(string userName, string clientId)
        {
            return 
                await UnitOfWork.ClientRepository.AnyAsync(x => x.Username.Equals(userName) && x.Id.Equals(clientId));
        }

        public async Task<ClientByUserNameDto> GetMyClientAsync(string userName, string clientId)
        {
            if (!await ExistsAsync(clientId))
                CustomException.ThrowNotFoundException($"There is no client with id = {clientId}.");

            var client = await UnitOfWork.ClientRepository.SingleOrDefaultAsync(x => x.Username.Equals(userName) && x.Id.Equals(clientId));
            return ModelFactory.GetModel<ClientByUserNameDto>(client);
        }

        public async Task<long> GetActiveJsClientCountByUserName(string userName)
            => await UnitOfWork.ClientRepository.Count(x => x.Username.Equals(userName) && x.Active && x.ApplicationType == ApplicationTypes.JavaScript);

        public async Task<long> GetActiveNativeClientCountByUserName(string userName)
            => await UnitOfWork.ClientRepository.Count(x => x.Username.Equals(userName) && x.Active && x.ApplicationType == ApplicationTypes.NativeConfidential);

        #region Helpers

        protected override async Task<bool> ExistsAsync(object id)
        => await UnitOfWork.ClientRepository.AnyAsync(x => x.Id.Equals((string)id));

        #endregion
    }
}