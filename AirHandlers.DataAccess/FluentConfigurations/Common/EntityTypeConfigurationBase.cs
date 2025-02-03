using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirHandlers.DataAccess.FluentConfigurations.Common
{
    public abstract class EntityTypeConfigurationBase<T> : IEntityTypeConfiguration<T> where T : class
    {
        public abstract void Configure(EntityTypeBuilder<T> builder);
    }
}