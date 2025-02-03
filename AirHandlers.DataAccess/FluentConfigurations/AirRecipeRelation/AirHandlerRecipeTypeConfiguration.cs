using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AirHandlers.Domain.Relations;

namespace AirHandlers.DataAccess.FluentConfigurations
{
    public class AirHandlerRecipeTypeConfiguration : IEntityTypeConfiguration<AirHandlerRecipe>
    {
        public void Configure(EntityTypeBuilder<AirHandlerRecipe> builder)
        {
            builder.HasKey(ar => new { ar.AirHandlerID, ar.RecipeID }); // Clave compuesta

            builder.HasOne(ar => ar.AirHandler)
                .WithMany(a => a.AssociatedRecipes) // Asegúrate de que esta propiedad esté en AirHandler
                .HasForeignKey(ar => ar.AirHandlerID);

            builder.HasOne(ar => ar.Recipe)
                .WithMany(r => r.ApplicableHandlers) // Asegúrate de que esta propiedad esté en Recipe
                .HasForeignKey(ar => ar.RecipeID);
        }
    }
}