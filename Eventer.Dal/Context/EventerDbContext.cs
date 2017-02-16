using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Eventer.Dal.FluentApiConfig;
using Eventer.Domain.Entity.EventerEntity;
using Eventer.Domain.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Eventer.Dal.Context
{
    public class EventerDbContext : IdentityDbContext<User>
    {
        public EventerDbContext()
            : base("EventerDb", throwIfV1Schema: false)
        {
            Database.CommandTimeout = 180;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new ApiActivityLogConfiguration());
            modelBuilder.Configurations.Add(new CategoryConfiguration());
            modelBuilder.Configurations.Add(new CityConfiguration());
            modelBuilder.Configurations.Add(new ClientConfiguration());
            modelBuilder.Configurations.Add(new ErrorLogConfiguration());
            modelBuilder.Configurations.Add(new EventConfiguration());
            modelBuilder.Configurations.Add(new RefreshTokenConfiguration());
            modelBuilder.Configurations.Add(new StateConfiguration());

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Category> Category { get; set; }
    }
}