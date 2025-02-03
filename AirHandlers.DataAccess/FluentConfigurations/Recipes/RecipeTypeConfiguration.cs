using AirHandlers.DataAccess.FluentConfigurations.Common;
using AirHandlers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirHandlers.DataAccess.FluentConfigurations.Recipes
{
    public class RecipeTypeConfiguration : EntityTypeConfigurationBase<Recipe>
    {
        public override void Configure(EntityTypeBuilder<Recipe> builder)
        {
            builder.ToTable("Recipes");
            builder.HasKey(r => r.Id); // Cambiado de "ID" a "Id"
            builder.Property(r => r.Name).IsRequired().HasMaxLength(100);
            builder.Property(r => r.ReferenceTemperature).IsRequired();
            builder.Property(r => r.ReferenceHumidity).IsRequired();
            builder.Property(r => r.StartDate).IsRequired();
            builder.Property(r => r.EndDate).IsRequired();
        }
    }
}