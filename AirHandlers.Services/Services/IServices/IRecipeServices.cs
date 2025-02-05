using AirHandlers.Domain.Entities;
using System;
using System.Collections.Generic;

namespace AirHandlers.Services.IServices
{
    public interface IRecipeServices
    {
        void AddRecipe(Recipe recipe);
        Recipe? GetRecipeById(Guid id);
        IEnumerable<Recipe> GetAllRecipes();
        void UpdateRecipe(Recipe recipe);
        void DeleteRecipe(Guid id);
    }
}