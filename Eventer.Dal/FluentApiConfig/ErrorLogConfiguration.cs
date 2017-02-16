using System.Data.Entity.ModelConfiguration;
using Eventer.Domain.Entity.ErrorLog;

namespace Eventer.Dal.FluentApiConfig
{
    public class ErrorLogConfiguration : EntityTypeConfiguration<ErrorLog>
    {
        public ErrorLogConfiguration()
        {
            Property(x => x.ErrorDateTime)
                .IsOptional();

            Property(x => x.ErrorLevel)
                .IsOptional();

            Property(x => x.UserName)
                .IsOptional();

            Property(x => x.ErrorMessage)
                .IsOptional();

            Property(x => x.InnerErrorMessage)
                .IsOptional();

            Property(x => x.StackTrace)
                .IsOptional();
        }
    }
}