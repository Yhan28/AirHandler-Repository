using System.Collections.Generic;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Contracts.Recipes
{
    public interface IRecipeRepository
    {
        /// <summary>
        /// Crea un nuevo Recipe en la base de datos.
        /// </summary>
        /// <param name="recipe">The Recipe object to be created.</param>
        void AddRecipe(Recipe recipe);

        /// <summary>
        /// Recibe un Recipe por el ID.
        /// </summary>
        /// <param name="id">The ID of the Recipe to retrieve.</param>
        /// <returns>The Recipe object if found; otherwise, null.</returns>
        T? GetRecipeById<T>(Guid id) where T : Recipe;

        /// <summary>
        /// Recibe todos los Recipe de la base de datos.
        /// </summary>
        /// <returns>A collection of all Recipes.</returns>
        public IEnumerable<T> GetAllRecipes<T>() where T : Recipe;

        /// <summary>
        /// Actualiza un Recipe en la base de datos.
        /// </summary>
        /// <param name="recipe">The Recipe object with updated values.</param>
        void UpdateRecipe(Recipe recipe);

        /// <summary>
        /// Elimina un Recipe de la base de datos por su id.
        /// </summary>
        /// <param name="id">The ID of the Recipe to delete.</param>
        void DeleteRecipe(Recipe recipe);
    }
}