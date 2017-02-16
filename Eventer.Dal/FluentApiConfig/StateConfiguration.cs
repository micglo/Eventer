using System.Data.Entity.ModelConfiguration;
using Eventer.Domain.Entity.EventerEntity;

namespace Eventer.Dal.FluentApiConfig
{
    public class StateConfiguration : EntityTypeConfiguration<State>
    {
        public StateConfiguration()
        {
            Property(s => s.StateName)
                .IsRequired();
        }
    }
}