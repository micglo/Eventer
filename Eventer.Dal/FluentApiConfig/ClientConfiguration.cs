using System.Data.Entity.ModelConfiguration;
using Eventer.Domain.Entity.Client;

namespace Eventer.Dal.FluentApiConfig
{
    public class ClientConfiguration : EntityTypeConfiguration<Client>
    {
        public ClientConfiguration()
        {
            Property(x => x.AllowedOrigin)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.Username)
                .HasMaxLength(100)
                .IsRequired();

            Property(x => x.ClientSecret)
                .IsMaxLength();

            Property(x => x.Active)
                .IsRequired();

            Property(x => x.ApplicationType)
                .IsRequired();

            Property(x => x.RefreshTokenLifeTime)
                .IsRequired();
        }
    }
}