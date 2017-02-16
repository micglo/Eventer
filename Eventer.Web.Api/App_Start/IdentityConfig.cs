using System;
using System.Configuration;
using System.Threading.Tasks;
using Eventer.Domain.Entity;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Eventer.Web.Api
{
    public class ApplicationUserManager : UserManager<User>
    {
        public ApplicationUserManager(IUserStore<User> store): base(store)
        {
            UserValidator = new UserValidator<User>(this)
            {
                AllowOnlyAlphanumericUserNames = false,
                RequireUniqueEmail = true
            };

            PasswordValidator = new PasswordValidator
            {
                RequiredLength = 6,
                RequireNonLetterOrDigit = false,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };

            EmailService = new EmailService();
            var dataProtectionProvider = Startup.DataProtectionProvider;
            if (dataProtectionProvider != null)
            {
                UserTokenProvider = new DataProtectorTokenProvider<User>(dataProtectionProvider.Create("ASP.NET Identity"))
                {
                    TokenLifespan = TimeSpan.FromDays(1)
                };
            }
        }
    }

    public class EmailService : IIdentityMessageService
    {
        public Task SendAsync(IdentityMessage message)
        {
            var sg = new SendGridAPIClient(ConfigurationManager.AppSettings["sendGridApiKey"]);
            var from = new Email("eventer@eventer.com");
            var subject = message.Subject;
            var to = new Email(message.Destination);
            var content = new Content("text/html", message.Body);
            var mail = new Mail(from, subject, to, content);

            sg.client.mail.send.post(requestBody: mail.Get());
            return Task.FromResult(0);
        }
    }
}
