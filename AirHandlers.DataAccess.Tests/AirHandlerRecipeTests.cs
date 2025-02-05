using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirHandlers.Data.Repositories;
using AirHandlers.Domain.Relations;
using AirHandlers.Domain.Entities;
using AirHandlers.DataAccess.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace AirHandlers.DataAccess.Tests.Repositories
{
    [TestClass]
    public class AirHandlerRecipeTests
    {
        private AirHandlerRecipeRepository _airHandlerRecipeRepository;
        private AirHandlerRepository _airHandlerRepository;
        private RecipeRepository _recipeRepository;
        private SqliteConnection _connection;

        [TestInitialize]
        public void Setup()
        {
            // Crear una conexión SQLite en memoria
            _connection = new SqliteConnection("Data Source=:memory:");
            _connection.Open();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlite(_connection)
                .Options;

            var context = new ApplicationDbContext(options);
            context.Database.EnsureCreated();

            // Inicializar los repositorios
            _airHandlerRecipeRepository = new AirHandlerRecipeRepository(context);
            _airHandlerRepository = new AirHandlerRepository(context);
            _recipeRepository = new RecipeRepository(context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _connection.Close();
        }

        [TestMethod]
        public async Task Can_Add_AirHandlerRecipe()
        {
            // Arrange
            var airHandler = new AirHandlerEntity(
                identifierCode: "AH-001",
                isOperating: true,
                filterChangeDate: DateTime.Now.AddDays(30),
                referenceTemperature: 22.5,
                referenceHumidity: 45.0
            );

            var recipe = new Recipe(
                name: "Recipe-0X3",
                referenceTemperature: 180.0,
                referenceHumidity: 50.0,
                startDate: DateTime.Now,
                endDate: DateTime.Now.AddDays(7)
            );

            // Agregar dependencias necesarias
            await _airHandlerRepository.AddAsync(airHandler);
            await _recipeRepository.AddAsync(recipe);

            var airHandlerRecipe = new AirHandlerRecipe
            {
                AirHandlerID = airHandler.Id,
                RecipeID = recipe.Id
            };

            // Act
            await _airHandlerRecipeRepository.AddAsync(airHandlerRecipe);

            // Assert
            var loadedAirHandlerRecipe = await _airHandlerRecipeRepository.GetByIdAsync(airHandler.Id, recipe.Id); // Asegúrate de pasar ambos IDs
            Assert.IsNotNull(loadedAirHandlerRecipe);
        }

        [TestMethod]
        public async Task Cannot_Get_AirHandlerRecipe_By_Invalid_Id()
        {
            // Act
            var loadedAirHandlerRecipe = await _airHandlerRecipeRepository.GetByIdAsync(Guid.NewGuid(), Guid.NewGuid()); // Pasar valores ficticios para ambas claves

            // Assert
            Assert.IsNull(loadedAirHandlerRecipe);
        }

        [TestMethod]
        public async Task Can_Get_All_AirHandlerRecipes()
        {
            // Arrange
            var airHandler = new AirHandlerEntity(
                identifierCode: "AH-002",
                isOperating: true,
                filterChangeDate: DateTime.Now.AddDays(30),
                referenceTemperature: 22.5,
                referenceHumidity: 45.0
            );

            var recipe = new Recipe(
                name: "Recipe-0x8",
                referenceTemperature: 175.0,
                referenceHumidity: 45.0,
                startDate: DateTime.Now,
                endDate: DateTime.Now.AddDays(3)
            );

            await _airHandlerRepository.AddAsync(airHandler);
            await _recipeRepository.AddAsync(recipe);

            await _airHandlerRecipeRepository.AddAsync(new AirHandlerRecipe { AirHandlerID = airHandler.Id, RecipeID = recipe.Id });

            // Act
            var allRecipes = await _airHandlerRecipeRepository.GetAllAsync();

            // Assert
            Assert.AreEqual(1, allRecipes.Count());
        }

        [TestMethod]
        public async Task Can_Delete_AirHandlerRecipe()
        {
            // Arrange
            var airHandler = new AirHandlerEntity(
                identifierCode: "AH-003",
                isOperating: true,
                filterChangeDate: DateTime.Now.AddDays(30),
                referenceTemperature: 22.5,
                referenceHumidity: 45.0
           );

            var recipe = new Recipe(
                name: "Recipe-07X3",
                referenceTemperature: 180.0,
                referenceHumidity: 55.0,
                startDate: DateTime.Now,
                endDate: DateTime.Now.AddDays(10)
            );

            await _airHandlerRepository.AddAsync(airHandler);
            await _recipeRepository.AddAsync(recipe);

            var airHandlerRecipe = new AirHandlerRecipe { AirHandlerID = airHandler.Id, RecipeID = recipe.Id };
            await _airHandlerRecipeRepository.AddAsync(airHandlerRecipe);

            // Act 
            await _airHandlerRecipeRepository.DeleteAsync(airHandler.Id, recipe.Id); // Asegúrate de pasar ambos IDs

            // Assert 
            var loadedAirhandlerRecipe = await _airHandlerRecipeRepository.GetByIdAsync(airHandler.Id, recipe.Id);
            Assert.IsNull(loadedAirhandlerRecipe);
        }
    }
}