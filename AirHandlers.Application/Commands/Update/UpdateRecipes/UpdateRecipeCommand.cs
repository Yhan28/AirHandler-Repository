using System;
using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.Recipes.Commands.UpdateRecipes
{
    public record UpdateRecipeCommand(Recipe Recipe) : ICommand;
}
