using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;
using System;

namespace AirHandlers.Application.Recipes.Commands.CreateRecipe
{
    public record CreateRecipeCommand(
        string Name,
        double ReferenceTemperature,
        double ReferenceHumidity,
        DateTime StartDate,
        DateTime EndDate) : ICommand<Recipe>;
}
