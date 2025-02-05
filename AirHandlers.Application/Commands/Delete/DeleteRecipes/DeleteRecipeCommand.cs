using System;
using AirHandlers.Application.Abstract;

namespace AirHandlers.Application.Recipes.Commands.DeleteRecipe
{
    public record DeleteRecipeCommand(Guid Id) : ICommand;
}
