using System.Data.Entity.ModelConfiguration;
using Eventer.Domain.Entity.EventerEntity;

namespace Eventer.Dal.FluentApiConfig
{
    public class CategoryConfiguration : EntityTypeConfiguration<Category>
    {
        public CategoryConfiguration()
        {
            Property(x => x.CategoryName)
                .IsRequired();

            HasMany(x => x.Events)
                .WithMany(x => x.Categories)
                .Map(x => x.MapLeftKey("CategoryId").MapRightKey("EventId").ToTable("CategoryEvent"));
        }
    }
}