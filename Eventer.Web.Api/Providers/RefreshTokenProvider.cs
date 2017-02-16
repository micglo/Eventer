using System;
using System.Threading.Tasks;
using Eventer.Model.Dto.RefreshToken;
using Eventer.Service.RefreshToken;
using Eventer.Service.RefreshToken.Interface;
using Microsoft.Owin.Security.Infrastructure;

namespace Eventer.Web.Api.Providers
{
    public class RefreshTokenProvider : IAuthenticationTokenProvider
    {
        private readonly IRefreshTokenService _refreshTokenService;

        public RefreshTokenProvider()
        {
            _refreshTokenService = new RefreshTokenService();
        }

        public void Create(AuthenticationTokenCreateContext context)
        {
            throw new System.NotImplementedException();
        }

        public async Task CreateAsync(AuthenticationTokenCreateContext context)
        {
            var clientid = context.Ticket.Properties.Dictionary["as:client_id"];

            if (string.IsNullOrEmpty(clientid))
            {
                return;
            }

            var refreshTokenId = _refreshTokenService.GenerateRefreshTokenId();
            var refreshTokenLifeTime = context.OwinContext.Get<string>("as:clientRefreshTokenLifeTime");

            var token = new RefreshTokenDto
            {
                Id = refreshTokenId,
                ClientId = clientid,
                Subject = context.Ticket.Identity.Name,
                IssuedUtc = DateTime.UtcNow,
                ExpiresUtc = DateTime.UtcNow.AddMinutes(Convert.ToDouble(refreshTokenLifeTime))
            };

            context.Ticket.Properties.IssuedUtc = token.IssuedUtc;
            context.Ticket.Properties.ExpiresUtc = token.ExpiresUtc;

            token.ProtectedTicket = context.SerializeTicket();

            var newToken = await _refreshTokenService.AddAsync(token);

            if (await _refreshTokenService.Exists(newToken.Id))
            {
                context.SetToken(refreshTokenId);
            }
        }

        public void Receive(AuthenticationTokenReceiveContext context)
        {
            throw new System.NotImplementedException();
        }

        public async Task ReceiveAsync(AuthenticationTokenReceiveContext context)
        {
            var allowedOrigin = context.OwinContext.Get<string>("as:clientAllowedOrigin");
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { allowedOrigin });

            var refreshToken = await _refreshTokenService.GetByIdAsync(context.Token);

            if (refreshToken != null)
            {
                context.DeserializeTicket(refreshToken.ProtectedTicket);
                await _refreshTokenService.RemoveAsync(refreshToken.Id);
            }
        }
    }
}