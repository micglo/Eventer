using System;
using System.Web.Http;
using Eventer.Web.Api.Providers;
using Eventer.Web.Api.Utility.ApiActivity;
using Microsoft.Owin;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Ninject.Web.Common.OwinHost;
using Ninject.Web.WebApi.OwinHost;
using Owin;

[assembly: OwinStartup(typeof(Eventer.Web.Api.Startup))]

namespace Eventer.Web.Api
{
    public class Startup
    {
        internal static IDataProtectionProvider DataProtectionProvider { get; private set; }

        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration httpConfig = new HttpConfiguration();
            DataProtectionProvider = app.GetDataProtectionProvider();

            app.Use(typeof(RequestTokenLogHandler));
            app.UseOAuthBearerTokens(ConfigureAuth());
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            WebApiConfig.Register(httpConfig);
            SwaggerConfig.Register(httpConfig);

            app.UseNinjectMiddleware(NinjectConfigurator.CreateKernel).UseNinjectWebApi(httpConfig);
        }

        private OAuthAuthorizationServerOptions ConfigureAuth()
        {
            return new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/token"),
                Provider = new ApplicationOAuthProvider(),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(60),
                // In production mode set AllowInsecureHttp = false
                AllowInsecureHttp = true,
                RefreshTokenProvider = new RefreshTokenProvider()
            };
        }
    }
}
