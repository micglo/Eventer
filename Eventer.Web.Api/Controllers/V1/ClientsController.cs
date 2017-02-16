using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Eventer.Domain.Entity.Client;
using Eventer.Model.ApiPagination.Common;
using Eventer.Model.BindingModel.Client;
using Eventer.Model.Dto.Client;
using Eventer.Model.QueryString.Pagination;
using Eventer.Service.Client.Interface;
using Eventer.Web.Api.Utility.Filter;
using Eventer.Web.Api.Utility.Versioning;
using Eventer.Service.RoleManager;
using Eventer.Utility.CustomException;

namespace Eventer.Web.Api.Controllers.V1
{
    /// <summary>
    /// Controller with actions to manage clients.
    /// </summary>
    [ApiVersion1RoutePrefix("clients")]
    public class ClientsController : ApiControllerBase
    {
        /// <summary>
        /// Clients Controller
        /// </summary>
        /// <param name="customException"></param>
        /// <param name="clientService"></param>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        public ClientsController(IClientService clientService, ApplicationUserManager userManager, ApplicationRoleManager roleManager, 
            ICustomException customException)
        {
            ClientService = clientService;
            UserManager = userManager;
            RoleManager = roleManager;
            CustomException = customException;
        }


        /// <summary>
        /// Get list of clients. Administratos only.
        /// </summary>
        /// <remarks>
        /// Get list of clients. Administratos only.
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <returns>Paged list fo clients</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("", Name = "GetClients")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<ClientDto>))]
        public async Task<IHttpActionResult> GetClients([FromUri] Pagination paginationModel)
            => Ok(await ClientService.GetAllAsync(paginationModel.Page, paginationModel.PageSize));


        /// <summary>
        /// Get single lcients specified by id. Administratos only.
        /// </summary>
        /// <remarks>
        /// Get single lcients specified by id. Administratos only.
        /// </remarks>
        /// <param name="id">Id of client</param>
        /// <returns>Single client</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Client not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:guid}", Name = "GetClient")]
        [HttpGet]
        [ResponseType(typeof(ClientDto))]
        public async Task<IHttpActionResult> GetClient(string id)
            => Ok(await ClientService.GetByIdAsync(id));


        /// <summary>
        /// Edit client specified by id. Administratos only.
        /// </summary>
        /// <remarks>
        /// Edit client specified by id. Administratos only.
        /// </remarks>
        /// <param name="id">Id of client</param>
        /// <param name="client">Client data model</param>
        /// <returns>Edited client</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Client not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:guid}", Name = "PutClient")]
        [HttpPut]
        [ResponseType(typeof(ClientDto))]
        public async Task<IHttpActionResult> PutClient(string id, ClientDto client)
        {
            if (id != client.Id)
                CustomException.ThrowBadRequestException($"Client id: {id} doesn't match.");

            return Ok(await ClientService.Update(client));
        }

        /// <summary>
        /// Edit client without secret change. Administrators only.
        /// </summary>
        /// <remarks>
        /// Edit client without secret change. Administrators only.
        /// </remarks>
        /// <param name="id">Id of client</param>
        /// <param name="client">Data client model</param>
        /// <returns>Edited client</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Client not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("PutClientNoSecret/{id:guid}", Name = "PutClientNoSecret")]
        [HttpPut]
        [ResponseType(typeof(ClientNoSecretDto))]
        public async Task<IHttpActionResult> PutClientNoSecret(string id, ClientNoSecretDto client)
        {
            if (id != client.Id)
                CustomException.ThrowBadRequestException($"Client id: {id} doesn't match.");

            return Ok(await ClientService.UpdateNoSecret(client));
        }


        /// <summary>
        /// Create new client. Administrators only.
        /// </summary>
        /// <remarks>
        /// Create new client. Administrators only.
        /// </remarks>
        /// <param name="client">Client data model</param>
        /// <returns>Created client</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("", Name = "PostClient")]
        [HttpPost]
        [ResponseType(typeof(ClientDto))]
        public async Task<IHttpActionResult> PostClient(ClientPostDto client)
        {
            var user = await UserManager.FindByEmailAsync(client.Username);
            if (user == null)
                CustomException.ThrowNotFoundException($"User: {client.Username} doesn't exist.");

            var messageToSend = "Username: " + user?.UserName;
            ClientDto newClient;

            if (client.ApplicationType == 0)
            {
                if (string.IsNullOrEmpty(client.AllowedOrigin))
                    CustomException.ThrowBadRequestException("Please provide origin for JavaScript web application.");

                if (client.AllowedOrigin.Equals("*"))
                    CustomException.ThrowBadRequestException("Sorry we cannot allow unlimited origin. Please provide direct domain address.");

                newClient = await ClientService.AddAsync(client);
                messageToSend += "<br>" + "client_id: " + newClient.Id;
            }
            else
            {
                var clientSecret = ClientService.GenerateClientSecret();

                client.ClientSecret = clientSecret;
                client.AllowedOrigin = "*";

                newClient = await ClientService.AddAsync(client);
                messageToSend += "<br>" + "client_id: " + newClient.Id + "<br>" + "client_secret: " + clientSecret;
                newClient.ClientSecret = clientSecret;
            }

            
            await UserManager.SendEmailAsync(user?.Id, "New client", $"{messageToSend}");

            return CreatedAtRoute("ClientRoute", new { id = newClient.Id }, newClient);
        }


        /// <summary>
        /// Delete client. Administraors only.
        /// </summary>
        /// <remarks>
        /// Delete client. Administraors only.
        /// </remarks>
        /// <param name="id">Id of client.</param>
        /// <returns>Http status code</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Client not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{id:guid}", Name = "DeleteClient")]
        [HttpDelete]
        [ResponseType(typeof(ClientDto))]
        public async Task<IHttpActionResult> DeleteClient(string id)
        {
            await ClientService.RemoveAsync(id);
            return Ok();
        }


        /// <summary>
        /// Get all user clients. Administraors only.
        /// </summary>
        /// <remarks>
        /// Get all user clients. Administraors only.
        /// </remarks>
        /// <param name="userName">Name of the user</param>
        /// <param name="paginationModel">Model representing query string</param>
        /// <returns>Paged client list</returns>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">User not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("{userName}", Name = "GetUserClients")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<ClientDto>))]
        public async Task<IHttpActionResult> GetUserClients(string userName, [FromUri] Pagination paginationModel)
        {
            var user = await UserManager.FindByEmailAsync(userName);
            if(user == null)
                CustomException.ThrowNotFoundException($"User {userName} doesn't exists.");

            return Ok(await ClientService.GetUserClients(userName, paginationModel.Page, paginationModel.PageSize));
        }


        /// <summary>
        /// Reset client secret. Administrators only.
        /// </summary>
        /// <remarks>
        /// Reset client secret. Administrators only.
        /// </remarks>
        /// <param name="model">Client data model.</param>
        /// <returns>Single client.</returns>
        /// <response code="200">Success</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="403">Forbidden</response>
        /// <response code="404">Client not found</response>
        /// <response code="500">Internal Server Error</response>
        [MyAuthorize(Roles = "Administrators")]
        [Route("ResetClientSecret", Name = "ResetClientSecret")]
        [HttpPost]
        [ResponseType(typeof(ClientDto))]
        public async Task<IHttpActionResult> ResetClientSecret([FromBody] ResetClientSecretBindingModel model)
        {
            var client = await ClientService.GetByIdAsync(model.Id);
            var user = await UserManager.FindByEmailAsync(client.Username);
            var newClientSecret = ClientService.GenerateClientSecret();

            var clientToUpdate = new ClientDto
            {
                Id = model.Id,
                ClientSecret = newClientSecret,
                Username = client.Username,
                Active = client.Active,
                RefreshTokenLifeTime = client.RefreshTokenLifeTime,
                ApplicationType = ApplicationTypes.NativeConfidential,
                AllowedOrigin = client.AllowedOrigin
            };

            var updatedClient = await ClientService.Update(clientToUpdate);

            var messageToSend = "Username: " + client.Username + "<br>" + "client_id: " + client.Id +
                                         "<br>" + "client_secret: " + newClientSecret;

            await UserManager.SendEmailAsync(user.Id, "Client secret changed", $"{messageToSend}");

            return Ok(updatedClient);
        }


        /// <summary>
        /// Get list of my clients.
        /// </summary>
        /// <remarks>
        /// Get list of my clients.
        /// </remarks>
        /// <param name="paginationModel">Model representing query string</param>
        /// <returns>Paged list of my clients.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetMyClients", Name = "GetMyClients")]
        [HttpGet]
        [ResponseType(typeof(PagedItems<ClientByUserNameDto>))]
        public async Task<IHttpActionResult> GetMyClients([FromUri] Pagination paginationModel)
        {
            var userName = User.Identity.Name;

            return Ok(await ClientService.GetAllByUserName(userName, paginationModel.Page, paginationModel.PageSize));
        }


        /// <summary>
        /// Get my client specified by id.
        /// </summary>
        /// <remarks>
        /// Get my client specified by id.
        /// </remarks>
        /// <param name="id">Id of client.</param>
        /// <returns>Single client.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Client not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("GetMyClients/{id:guid}", Name = "GetMyClient")]
        [HttpGet]
        [ResponseType(typeof(ClientByUserNameDto))]
        public async Task<IHttpActionResult> GetMyClient(string id)
        {
            var userName = User.Identity.Name;
            if (!await ClientService.CheckUserClient(userName, id))
                CustomException.ThrowBadRequestException($"There is no client with id = {id} associated with user: {userName}.");

            return Ok(await ClientService.GetMyClientAsync(userName, id));
        }


        /// <summary>
        /// Add new client.
        /// </summary>
        /// <remarks>
        /// Add new client.
        /// </remarks>
        /// <param name="addClientModel">Client data model.</param>
        /// <returns>Created client.</returns>
        /// <response code="201">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="500">Internal Server Error</response>
        [Route("AddClient", Name = "AddClient")]
        [HttpPost]
        [ResponseType(typeof(ClientDto))]
        public async Task<IHttpActionResult> AddClient(AddClientDto addClientModel)
        {
            var username = User.Identity.Name;
            var user = await UserManager.FindByEmailAsync(username);

            var adminRoleId = RoleManager.Roles.SingleOrDefault(x => x.Name.Equals("Administrators"))?.Id;

            if (!user.Roles.Any(x => x.RoleId.Equals(adminRoleId)))
            {
                var jsClientCount = await ClientService.GetActiveJsClientCountByUserName(username);
                var nativeClientCount = await ClientService.GetActiveNativeClientCountByUserName(username);

                if (jsClientCount > 5)
                    CustomException.ThrowBadRequestException("Only 5 JavaScript clients per user.");

                if (nativeClientCount > 5)
                    CustomException.ThrowBadRequestException("Only 5 native clients per user.");
            }
            
            var client = new ClientPostDto
            {
                Username = user.UserName,
                Active = true,
                RefreshTokenLifeTime = 10080
            };

            var messageToSend = "Username: " + user.UserName;
            ClientDto newClient;

            if (addClientModel.IsJavaScriptClient)
            {
                if (string.IsNullOrEmpty(addClientModel.AllowedOrigin))
                    CustomException.ThrowBadRequestException("Please provide origin for JavaScript web application.");

                if (addClientModel.AllowedOrigin.Equals("*"))
                    CustomException.ThrowBadRequestException("Sorry we cannot allow unlimited origin. Please provide direct domain address.");

                client.ApplicationType = 0;
                client.AllowedOrigin = addClientModel.AllowedOrigin;

                newClient = await ClientService.AddAsync(client);
                messageToSend += "<br>" + "client_id: " + newClient.Id;
            }
            else
            {
                var clientSecret = ClientService.GenerateClientSecret();

                client.ClientSecret = clientSecret;
                client.ApplicationType = 1;
                client.AllowedOrigin = "*";

                newClient = await ClientService.AddAsync(client);
                newClient.ClientSecret = clientSecret;
                messageToSend += "<br>" + "client_id: " + newClient.Id + "<br>" + "client_secret: " + clientSecret;
            }

            await UserManager.SendEmailAsync(user.Id, "New client", $"{messageToSend}");

            return CreatedAtRoute("GetMyClientsRoute", new { id = newClient.Id }, newClient);
        }


        /// <summary>
        /// Reset my client secret.
        /// </summary>
        /// <remarks>
        /// Reset my client secret.
        /// </remarks>
        /// <param name="model">Client data model.</param>
        /// <returns>Single client.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Client not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("ResetMyClientSecret", Name = "ResetMyClientSecret")]
        [HttpPost]
        [ResponseType(typeof(ClientDto))]
        public async Task<IHttpActionResult> ResetMyClientSecret([FromBody] ResetClientSecretBindingModel model)
        {
            var userName = User.Identity.Name;
            if (!await ClientService.CheckUserClient(userName, model.Id))
                CustomException.ThrowBadRequestException($"There is no client with id = {model.Id} associated with user: {userName}.");

            var user = await UserManager.FindByEmailAsync(userName);

            var client = await ClientService.GetMyClientAsync(userName, model.Id);

            if (!client.Active)
                CustomException.ThrowBadRequestException($"Client with id = {client.Id} is no more valid.");

            var newClientSecret = ClientService.GenerateClientSecret();

            var clientToUpdate = new ClientDto
            {
                Id = model.Id,
                ClientSecret = newClientSecret,
                Username = userName,
                Active = client.Active,
                RefreshTokenLifeTime = client.RefreshTokenLifeTime,
                ApplicationType = ApplicationTypes.NativeConfidential,
                AllowedOrigin = client.AllowedOrigin
            };

            var updatedClient = await ClientService.Update(clientToUpdate);

            var messageToSend = "Username: " + userName + "<br>" + "client_id: " + client.Id +
                                         "<br>" + "client_secret: " + newClientSecret;

            await UserManager.SendEmailAsync(user.Id, "Client secret changed", $"{messageToSend}");

            return Ok(updatedClient);
        }


        /// <summary>
        /// Edit my client.
        /// </summary>
        /// <remarks>
        /// Edit my client.
        /// </remarks>
        /// <param name="id">Id of client.</param>
        /// <param name="client">Client data model</param>
        /// <returns>Single client.</returns>
        /// <response code="200">Success</response>
        /// <response code="400">Bad request</response>
        /// <response code="401">Unauthorized</response>
        /// <response code="404">Client not found</response>
        /// <response code="500">Internal Server Error</response>
        [Route("PutMyClient/{id:guid}", Name = "PutMyClient")]
        [HttpPut]
        [ResponseType(typeof(ClientNoSecretDto))]
        public async Task<IHttpActionResult> PutMyClient(string id, ClientNoSecretDto client)
        {
            var userName = User.Identity.Name;
            if (!await ClientService.CheckUserClient(userName, id))
                CustomException.ThrowBadRequestException($"There is no client with id = {id} associated with user: {userName}.");

            if (id != client.Id)
                CustomException.ThrowBadRequestException($"Client id: {id} doesn't match.");

            return Ok(await ClientService.UpdateNoSecret(client));
        }
    }
}