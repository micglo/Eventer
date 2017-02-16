using System.Data.Entity.ModelConfiguration;
using Eventer.Domain.Entity.EventerEntity;

namespace Eventer.Dal.FluentApiConfig
{
    public class EventConfiguration : EntityTypeConfiguration<Event>
    {
        public EventConfiguration()
        {
            Property(e => e.EventName)
            .IsOptional();

            Property(e => e.EventDate)
                .IsOptional();

            Property(e => e.EventLocalization)
                .IsOptional();

            Property(e => e.EventImage)
                .IsOptional();

            Property(e => e.EventUrl)
                .IsOptional();

            Property(e => e.EventDescription)
                .IsOptional();
        }
    }
}