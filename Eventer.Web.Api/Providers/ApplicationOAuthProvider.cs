using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Eventer.Dal.Context;
using Eventer.Domain.Entity.Client;
using Eventer.Domain.Entity;
using Eventer.Service.Client;
using Eventer.Service.Client.Interface;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;

namespace Eventer.Web.Api.Providers
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        private readonly IClientService _clientService;

        public ApplicationOAuthProvider()
        {
            _clientService = new ClientService();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin") ?? "*";
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var userManager = new ApplicationUserManager(new UserStore<User>(new EventerDbContext()));
            User user = await userManager.FindAsync(context.UserName, context.Password);

            if (user == null)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            if (!user.EmailConfirmed)
            {
                context.SetError("email_confirmation", "The user email is not confirmed.");
                return;
            }

            ClaimsIdentity oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
               OAuthDefaults.AuthenticationType);

            AuthenticationProperties properties = new AuthenticationProperties(new Dictionary<string, string>
                {
                    {
                        "as:client_id", context.ClientId ?? string.Empty
                    },
                    {
                        "userName", context.UserName
                    }
                });

            AuthenticationTicket ticket = new AuthenticationTicket(oAuthIdentity, properties);
            context.Validated(ticket);
        }

        public override Task TokenEndpoint(OAuthTokenEndpointContext context)
        {
            foreach (KeyValuePair<string, string> property in context.Properties.Dictionary)
            {
                context.AdditionalResponseParameters.Add(property.Key, property.Value);
            }

            return Task.FromResult<object>(null);
        }

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId;
            string clientSecret;
            if (!context.TryGetBasicCredentials(out clientId, out clientSecret))
            {
                context.TryGetFormCredentials(out clientId, out clientSecret);
            }

            if (context.ClientId == null)
            {
                //context.SetError("invalid_clientId", "client_id should be sent.");
                context.Validated();
                return Task.FromResult<object>(null);
            }

            if (!_clientService.Exists(clientId))
            {
                context.SetError("invalid_client_id", $"client_id '{context.ClientId}' is not registered in the system.");
                return Task.FromResult<object>(null);
            }

            var client = _clientService.GetById(clientId);

            if (client.ApplicationType == ApplicationTypes.NativeConfidential)
            {
                if (string.IsNullOrWhiteSpace(clientSecret))
                {
                    context.SetError("invalid_client_secret", "client_secret should be sent.");
                    return Task.FromResult<object>(null);
                }
                if (client.ClientSecret != _clientService.GetHash(clientSecret))
                {
                    context.SetError("invalid_client_secret", "client_secret is invalid.");
                    return Task.FromResult<object>(null);
                }
            }

            if (!client.Active)
            {
                context.SetError("invalid_clientId", "Client is inactive.");
                return Task.FromResult<object>(null);
            }

            context.OwinContext.Set("as:clientAllowedOrigin", client.AllowedOrigin);
            context.OwinContext.Set("as:clientRefreshTokenLifeTime", client.RefreshTokenLifeTime.ToString());

            context.Validated();
            return Task.FromResult<object>(null);
        }

        public static AuthenticationProperties CreateProperties(string userName)
        {
            IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "userName", userName }
            };
            return new AuthenticationProperties(data);
        }

        public override Task GrantRefreshToken(OAuthGrantRefreshTokenContext context)
        {
            var originalClient = context.Ticket.Properties.Dictionary["as:client_id"];
            var currentClient = context.ClientId;

            if (originalClient != currentClient)
            {
                context.SetError("invalid_client_id", "Refresh token is issued to a different client_id.");
                return Task.FromResult<object>(null);
            }

            var newIdentity = new ClaimsIdentity(context.Ticket.Identity);

            var newTicket = new AuthenticationTicket(newIdentity, context.Ticket.Properties);
            context.Validated(newTicket);

            return Task.FromResult<object>(null);
        }
    }
}