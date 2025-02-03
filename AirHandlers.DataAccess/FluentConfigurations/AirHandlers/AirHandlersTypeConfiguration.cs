using AirHandlers.DataAccess.FluentConfigurations.Common;
using AirHandlers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirHandlers.DataAccess.FluentConfigurations.AirHandlers
{
    public class AirHandlerTypeConfiguration : EntityTypeConfigurationBase<AirHandlerEntity>
    {
        public override void Configure(EntityTypeBuilder<AirHandlerEntity> builder)
        {
            builder.ToTable("AirHandlers");
            builder.HasKey(a => a.Id); // Cambie de "ID" a "Id" pq elimine la propiedad ID en las clases base
            builder.Property(a => a.IdentifierCode).IsRequired().HasMaxLength(50);
            builder.Property(a => a.IsOperating).IsRequired();
            builder.Property(a => a.FilterChangeDate).IsRequired();
            builder.Property(a => a.ReferenceTemperature).IsRequired();
            builder.Property(a => a.ReferenceHumidity).IsRequired();
        }
    }
}