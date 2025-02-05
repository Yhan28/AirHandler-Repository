using AirHandlers.Application.Abstract;
using AirHandlers.Application.Recipes.Commands.UpdateRecipes;
using AirHandlers.Contracts;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AirHandlers.Application.Recipes.CommandHandlers
{
    public class UpdateRecipeCommandHandler
        : ICommandHandler<UpdateRecipeCommand>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRecipeCommandHandler(
            IRecipeRepository recipeRepository,
            IUnitOfWork unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(UpdateRecipeCommand request, CancellationToken cancellationToken)
        {
            _recipeRepository.UpdateRecipe(request.Recipe);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
