using AirHandlers.Application.Abstract;
using AirHandlers.Application.Recipes.Commands.DeleteRecipe;
using AirHandlers.Contracts;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AirHandlers.Application.Recipes.CommandHandlers
{
    public class DeleteRecipeCommandHandler
        : ICommandHandler<DeleteRecipeCommand>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRecipeCommandHandler(
            IRecipeRepository recipeRepository,
            IUnitOfWork unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteRecipeCommand request, CancellationToken cancellationToken)
        {
            // Buscar la receta por su ID
            var recipeToDelete = _recipeRepository.GetRecipeById<Recipe>(request.Id);

            // Si no se encuentra, no se realiza ninguna acción
            if (recipeToDelete is null)
                return;

            // Eliminar la receta utilizando el repositorio
            _recipeRepository.DeleteRecipe(recipeToDelete);

            // Guardar los cambios en la base de datos
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
