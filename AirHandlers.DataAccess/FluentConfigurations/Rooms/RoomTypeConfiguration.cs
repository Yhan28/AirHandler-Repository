using AirHandlers.DataAccess.FluentConfigurations.Common;
using AirHandlers.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AirHandlers.DataAccess.FluentConfigurations.Rooms
{
    public class RoomTypeConfiguration : EntityTypeConfigurationBase<Room>
    {
        public override void Configure(EntityTypeBuilder<Room> builder)
        {
            builder.ToTable("Rooms");
            builder.HasKey(r => r.ID);
            builder.Property(r => r.Number).IsRequired();
            builder.Property(r => r.Volume).IsRequired();

            // Configuración de la relación con AirHandler
            builder.HasOne(r => r.AssociatedHandler)
                   .WithMany(a => a.ServedRooms)
                   .HasForeignKey(r => r.AssociatedHandlerId);
        }
    }
}