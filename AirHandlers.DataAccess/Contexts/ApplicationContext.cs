using Microsoft.EntityFrameworkCore;
using AirHandlers.Domain.Entities;
using AirHandlers.DataAccess.FluentConfigurations.AirHandlers;
using AirHandlers.DataAccess.FluentConfigurations.Recipes;
using AirHandlers.DataAccess.FluentConfigurations.Rooms;
using AirHandlers.Domain.Relations;
using Microsoft.EntityFrameworkCore.Design;
using AirHandlers.DataAccess.FluentConfigurations;

namespace AirHandlers.DataAccess.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AirHandlerEntity> AirHandlers { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<AirHandlerRecipe> AirHandlerRecipes { get; set; } // Agregar DbSet para la tabla intermedia

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AirHandlerTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RecipeTypeConfiguration());
            modelBuilder.ApplyConfiguration(new RoomTypeConfiguration());
            modelBuilder.ApplyConfiguration(new AirHandlerRecipeTypeConfiguration());

            // Configuración de relaciones muchos a muchos entre AirHandler y Recipe
            modelBuilder.Entity<AirHandlerRecipe>()
                .HasKey(ar => new { ar.AirHandlerID, ar.RecipeID }); // Clave compuesta

            modelBuilder.Entity<AirHandlerRecipe>()
                .HasOne(ar => ar.AirHandler)
                .WithMany(a => a.AssociatedRecipes)
                .HasForeignKey(ar => ar.AirHandlerID);

            modelBuilder.Entity<AirHandlerRecipe>()
                .HasOne(ar => ar.Recipe)
                .WithMany(r => r.ApplicableHandlers)
                .HasForeignKey(ar => ar.RecipeID);

            // Configuración de la relación uno a muchos entre AirHandler y Room
            modelBuilder.Entity<Room>()
                .HasOne(r => r.AssociatedHandler) // Propiedad de navegación en Room
                .WithMany(a => a.ServedRooms) // Propiedad de navegación en AirHandler
                .HasForeignKey(r => r.AssociatedHandlerId); // Clave foránea en Room
        }

        /// <summary>
        /// Habilita características en tiempo de diseño para la base de datos del proyecto.
        /// Ejemplo: Migraciones.
        /// </summary>
        public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                // Crear un constructor de opciones para el DbContext
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

                try
                {
                    // Define la cadena de conexión aquí (ajústala según tu configuración)
                    var connectionString = "Data Source=AirHandlerDB.sqlite"; // Cambia el nombre si es necesario
                    optionsBuilder.UseSqlite(connectionString); // Usa SQLite como proveedor
                }
                catch (Exception ex)
                {
                    // Manejo de errores en caso de que algo falle
                    Console.WriteLine($"Error al configurar el DbContext: {ex.Message}");
                    throw;
                }

                // Devuelve una instancia del contexto configurado
                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
    }
}