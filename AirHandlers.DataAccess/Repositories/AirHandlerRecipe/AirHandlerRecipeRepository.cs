using AirHandlers.Contracts.AirHandlersRecipe;
using AirHandlers.DataAccess.Contexts;
using AirHandlers.Domain.Relations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirHandlers.Data.Repositories
{
    public class AirHandlerRecipeRepository : IAirHandlerRecipeRepository
    {
        private readonly ApplicationDbContext _context;

        public AirHandlerRecipeRepository(ApplicationDbContext context)
        {
            _context = context;AirHandlerRecipe airHandlerRecipe;
        }

        public async Task AddAsync(AirHandlerRecipe airHandlerRecipe)
        {
            await _context.Set<AirHandlerRecipe>().AddAsync(airHandlerRecipe);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<AirHandlerRecipe>> GetAllAsync()
        {
            return await _context.Set<AirHandlerRecipe>().ToListAsync();
        }

        public async Task<AirHandlerRecipe> GetByIdAsync(Guid airHandlerId, Guid recipeId)
        {
            // Corregido: Pasar ambos valores directamente
            return await _context.Set<AirHandlerRecipe>()
                .FindAsync(airHandlerId, recipeId);
        }

        public async Task DeleteAsync(Guid airHandlerId, Guid recipeId)
        {
            var entity = await GetByIdAsync(airHandlerId, recipeId);
            if (entity != null)
            {
                _context.Set<AirHandlerRecipe>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}