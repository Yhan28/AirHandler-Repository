using AirHandlers.Contracts.Recipes;
using AirHandlers.DataAccess.Contexts;
using AirHandlers.Domain.Entities;
using System.Collections.Generic;

namespace AirHandlers.Data.Repositories
{
    public class RecipeRepository : RepositoryBase<Recipe>, IRecipeRepository
    {
        public RecipeRepository(ApplicationDbContext context) : base(context) { }

        public void AddRecipe(Recipe recipe) => AddAsync(recipe).Wait();

        public T? GetRecipeById<T>(Guid id) where T : Recipe
          => GetByIdAsync(id).Result as T;

        public IEnumerable<T> GetAllRecipes<T>() where T : Recipe
          => GetAllAsync().Result as IEnumerable<T>;

        public void UpdateRecipe(Recipe recipe)
          => UpdateAsync(recipe).Wait();

        public void DeleteRecipe(Recipe recipe)
          => DeleteAsync(recipe.Id).Wait(); // Cambiado de "ID" a "Id"
    }
}