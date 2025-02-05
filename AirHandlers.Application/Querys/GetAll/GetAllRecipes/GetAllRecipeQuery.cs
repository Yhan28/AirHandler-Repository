using System;
using System.Collections.Generic;
using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.Recipes.Queries.GetAllRecipes
{
    public record GetAllRecipeQuery : IQuery<IEnumerable<Recipe>>;
}
