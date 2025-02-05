using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirHandlers.Application.Abstract;
using AirHandlers.Application.Recipes.Queries.GetAllRecipes;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.Recipes.CommandHandlers
{
    public class GetAllRecipesQueryHandler
        : IQueryHandler<GetAllRecipeQuery, IEnumerable<Recipe>>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetAllRecipesQueryHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public Task<IEnumerable<Recipe>> Handle(GetAllRecipeQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_recipeRepository.GetAllRecipes<Recipe>());
        }
    }
}
