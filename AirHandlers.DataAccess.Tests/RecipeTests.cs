using Microsoft.VisualStudio.TestTools.UnitTesting;
using AirHandlers.Data.Repositories;
using AirHandlers.Domain.Entities;
using AirHandlers.DataAccess.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AirHandlers.DataAccess.Tests.Repositories
{
    [TestClass]
    public class RecipeTests
    {
        private RecipeRepository _repository;
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

            _repository = new RecipeRepository(context);
        }

        [TestCleanup]
        public void Cleanup()
        {
            _connection.Close();
        }

        [TestMethod]
        public void Can_Add_Recipe()
        {
            // Arrange
            var recipe = new Recipe(
                name: "Recipe-XX4-07x9",
                referenceTemperature: 180.0,
                referenceHumidity: 50.0,
                startDate: DateTime.Now,
                endDate: DateTime.Now.AddDays(7)
            );

            // Act
            _repository.AddRecipe(recipe);

            // Assert
            var loadedRecipe = _repository.GetRecipeById<Recipe>(recipe.Id);
            Assert.IsNotNull(loadedRecipe);
        }

        [TestMethod]
        public void Cannot_Get_Recipe_By_Invalid_Id()
        {
            // Act
            var loadedRecipe = _repository.GetRecipeById<Recipe>(Guid.NewGuid());

            // Assert
            Assert.IsNull(loadedRecipe);
        }

        [TestMethod]
        public void Can_Get_All_Recipes()
        {
            // Arrange
            var recipe1 = new Recipe(
                name: "Recipe-XX4-0089",
                referenceTemperature: 175.0,
                referenceHumidity: 45.0,
                startDate: DateTime.Now,
                endDate: DateTime.Now.AddDays(3)
            );

            var recipe2 = new Recipe(
                name: "Recipe-XX4",
                referenceTemperature: 5.0,
                referenceHumidity: 60.0,
                startDate: DateTime.Now,
                endDate: DateTime.Now.AddDays(2)
            );

            _repository.AddRecipe(recipe1);
            _repository.AddRecipe(recipe2);

            // Act
            var allRecipes = _repository.GetAllRecipes<Recipe>();

            // Assert
            Assert.AreEqual(2, allRecipes.Count());
        }

        [TestMethod]
        public void Can_Update_Recipe()
        {
            // Arrange
            var recipe = new Recipe(
                name: "Recipe-78X",
                referenceTemperature: 90.0,
                referenceHumidity: 70.0,
                startDate: DateTime.Now,
                endDate: DateTime.Now.AddDays(5)
            );

            _repository.AddRecipe(recipe);

            recipe.Name = "Recipe-6X1";

            // Act
            _repository.UpdateRecipe(recipe);

            // Assert
            var updatedRecipe = _repository.GetRecipeById<Recipe>(recipe.Id);
            Assert.AreEqual("Recipe-6X1", updatedRecipe.Name);
        }

        [TestMethod]
        public void Can_Delete_Recipe()
        {
            // Arrange
            var recipe = new Recipe(
                name: "Recipe-67X41",
                referenceTemperature: 180.0,
                referenceHumidity: 55.0,
                startDate: DateTime.Now,
                endDate: DateTime.Now.AddDays(10)
            );

            _repository.AddRecipe(recipe);

            // Act
            _repository.DeleteRecipe(recipe);

            // Assert
            var loadedRecipe = _repository.GetRecipeById<Recipe>(recipe.Id);
            Assert.IsNull(loadedRecipe);
        }
    }
}