using AirHandlers.DataAccess.FluentConfigurations.Common;
using AirHandlers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirHandlers.DataAccess.FluentConfigurations.AirHandlers
{
    public class AirHandlerTypeConfiguration : EntityTypeConfigurationBase<AirHandler>
    {
        public override void Configure(EntityTypeBuilder<AirHandler> builder)
        {
            builder.ToTable("AirHandlers");
            builder.HasKey(a => a.ID);
            builder.Property(a => a.IdentifierCode).IsRequired().HasMaxLength(50);
            builder.Property(a => a.IsOperating).IsRequired();
            builder.Property(a => a.FilterChangeDate).IsRequired();
            builder.Property(a => a.ReferenceTemperature).IsRequired();
            builder.Property(a => a.ReferenceHumidity).IsRequired();
        }
    }
}