using AirHandlers.Domain.Relations;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirHandlers.Contracts.AirHandlersRecipe

{
    public interface IAirHandlerRecipeRepository
    {
        Task AddAsync(AirHandlerRecipe airHandlerRecipe);
        Task<IEnumerable<AirHandlerRecipe>> GetAllAsync();
        Task<AirHandlerRecipe> GetByIdAsync(Guid airHandlerId, Guid recipeId);
        Task DeleteAsync(Guid airHandlerId, Guid recipeId);
    }
}