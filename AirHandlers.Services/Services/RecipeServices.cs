using AirHandlers.Contracts.Recipes;
using AirHandlers.Data.Repositories;
using AirHandlers.Domain.Entities;
using AirHandlers.Services.IServices;
using System;
using System.Collections.Generic;
namespace AirHandlers.Services.Services
{
    public class RecipeService : IRecipeServices // Asegúrate de que sea IRecipeServices
    {
        private readonly IRecipeRepository _recipeRepository;

        public RecipeService(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public void AddRecipe(Recipe recipe)
        {
            _recipeRepository.AddRecipe(recipe);
        }

        public Recipe? GetRecipeById(Guid id)
        {
            return _recipeRepository.GetRecipeById<Recipe>(id);
        }

        public IEnumerable<Recipe> GetAllRecipes()
        {
            return _recipeRepository.GetAllRecipes<Recipe>();
        }

        public void UpdateRecipe(Recipe recipe)
        {
            _recipeRepository.UpdateRecipe(recipe);
        }

        public void DeleteRecipe(Guid id)
        {
            var recipe = GetRecipeById(id);
            if (recipe != null)
            {
                _recipeRepository.DeleteRecipe(recipe);
            }
            else
            {
                throw new ArgumentException("No se encontró la receta con el ID especificado.");
            }
        }
    }
}