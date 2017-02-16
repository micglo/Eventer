using System.Data.Entity.ModelConfiguration;
using Eventer.Domain.Entity.EventerEntity;

namespace Eventer.Dal.FluentApiConfig
{
    public class CityConfiguration : EntityTypeConfiguration<City>
    {
        public CityConfiguration()
        {
            HasRequired(x => x.State)
                .WithMany(x => x.Cities)
                .HasForeignKey(x=>x.StateId);

            HasMany(x => x.Events)
                .WithRequired(x => x.City).HasForeignKey(x=>x.CityId);

            Property(x => x.CityName)
                .IsRequired();
        }
    }
}