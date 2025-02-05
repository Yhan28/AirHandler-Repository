using System.Threading;
using System.Threading.Tasks;
using AirHandlers.Application.Abstract;
using AirHandlers.Application.Recipes.Queries.GetRecipeById;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.Recipes.CommandHandlers
{
    public class GetRecipeByIdQueryHandler
        : IQueryHandler<GetRecipeByIdQuery, Recipe?>
    {
        private readonly IRecipeRepository _recipeRepository;

        public GetRecipeByIdQueryHandler(IRecipeRepository recipeRepository)
        {
            _recipeRepository = recipeRepository;
        }

        public Task<Recipe?> Handle(GetRecipeByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_recipeRepository.GetRecipeById<Recipe>(request.Id));
        }
    }
}
