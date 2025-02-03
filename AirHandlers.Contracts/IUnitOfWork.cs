using AirHandlers.Contracts.AirHandlers;
using AirHandlers.Contracts.Recipes;

namespace AirHandlers.Contracts
{
    /// <summary>
    /// Define las propiedades y funcionalidades de un elemento de acceso a datos.
    /// </summary>
    public interface IUnitOfWork
    {
        IAirHandlerRepository AirHandlerRepository { get; }
        IRecipeRepository RecipeRepository { get; }
        IRoomRepository RoomRepository { get; }

        void SaveChanges();
        Task SaveChangesAsync();
    }
}