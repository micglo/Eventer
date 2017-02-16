using System.Data.Entity.ModelConfiguration;
using Eventer.Domain.Entity.RefreshToken;

namespace Eventer.Dal.FluentApiConfig
{
    public class RefreshTokenConfiguration : EntityTypeConfiguration<RefreshToken>
    {
        public RefreshTokenConfiguration()
        {
            Property(x => x.ClientId)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.ProtectedTicket)
                .IsRequired();

            Property(x => x.Subject)
                .HasMaxLength(100)
                .IsRequired();
        }
    }
}