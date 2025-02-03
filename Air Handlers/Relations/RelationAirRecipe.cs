using AirHandlers.Domain.Entities;
using System;

namespace AirHandlers.Domain.Relations
{
    public class AirHandlerRecipe
    {
        public Guid AirHandlerID { get; set; }  // Clave foránea hacia AirHandler
        public AirHandlerEntity AirHandler { get; set; }  // Navegación hacia AirHandler

        public Guid RecipeID { get; set; }  // Clave foránea hacia Recipe
        public Recipe Recipe { get; set; }  // Navegación hacia Recipe
    }
}