using System.Data.Entity.ModelConfiguration;
using Eventer.Domain.Entity.ApiActivity;

namespace Eventer.Dal.FluentApiConfig
{
    public class ApiActivityLogConfiguration : EntityTypeConfiguration<ApiActivityLog>
    {
        public ApiActivityLogConfiguration()
        {
            Property(x => x.ResponseTimestamp)
                .IsOptional();

            Property(x => x.ResponseContentType)
                .IsOptional();

            Property(x => x.ResponseHeaders)
                .IsOptional();

            Property(x => x.ResponseStatusCode)
                .IsOptional();

            Property(x => x.ResponseContentBody)
                .IsOptional();

            Property(x => x.User)
                .IsOptional();

            Property(x => x.RequestMethod)
                .IsOptional();

            Property(x => x.RequestContentType)
                .IsOptional();

            Property(x => x.UserHostAddress)
                .IsOptional();

            Property(x => x.RequestContentBody)
                .IsOptional();

            Property(x => x.RequestHeaders)
                .IsOptional();

            Property(x => x.RequestRouteData)
                .IsOptional();

            Property(x => x.RequestRouteTemplate)
                .IsOptional();

            Property(x => x.RequestTimestamp)
                .IsOptional();

            Property(x => x.RequestUri)
                .IsOptional();
        }
    }
}