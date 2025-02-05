using System;
using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.Recipes.Queries.GetRecipeById
{
    public record GetRecipeByIdQuery(Guid Id) : IQuery<Recipe?>;
}
