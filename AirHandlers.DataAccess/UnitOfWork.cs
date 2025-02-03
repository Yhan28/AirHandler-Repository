using AirHandlers.Data;
using AirHandlers.Contracts.AirHandlers;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Contracts;
using AirHandlers.DataAccess.Contexts;
using AirHandlers.Data.Repositories;

namespace AirHandlers.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
        }

        // Propiedades para acceder a los repositorios
        private IAirHandlerRepository _airHandlerRepository;
        private IRecipeRepository _recipeRepository;
        private IRoomRepository _roomRepository;

        public IAirHandlerRepository AirHandlerRepository
            => _airHandlerRepository ??= new AirHandlerRepository(_context);

        public IRecipeRepository RecipeRepository
            => _recipeRepository ??= new RecipeRepository(_context);

        public IRoomRepository RoomRepository
            => _roomRepository ??= new RoomRepository(_context);

        /// <summary>
        /// Guarda los cambios en el contexto actual.
        /// </summary>
        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        /// <summary>
        /// Guarda los cambios en el contexto actual de manera asincrónica.
        /// </summary>
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}