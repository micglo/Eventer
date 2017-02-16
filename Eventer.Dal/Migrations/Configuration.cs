using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Migrations;
using System.Linq;

namespace Eventer.Dal.Migrations
{
    

    internal sealed class Configuration : DbMigrationsConfiguration<Eventer.Dal.Context.EventerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Eventer.Dal.Context.EventerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            SeedRoles(context);
        }

        private void SeedRoles(Context.EventerDbContext context)
        {
            var store = new RoleStore<IdentityRole>(context);
            var manager = new RoleManager<IdentityRole>(store);

            if (!context.Roles.Any(r => r.Name == "Administrators"))
            {
                var role = new IdentityRole { Name = "Administrators" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Users"))
            {
                var role = new IdentityRole { Name = "Users" };
                manager.Create(role);
            }
        }
    }
}
