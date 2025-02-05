using AirHandlers.Application.Abstract;
using AirHandlers.Application.Recipes.Commands.CreateRecipe;
using AirHandlers.Contracts;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AirHandlers.Application.Recipes.CommandHandlers
{
    public class CreateRecipeCommandHandler
        : ICommandHandler<CreateRecipeCommand, Recipe>
    {
        private readonly IRecipeRepository _recipeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRecipeCommandHandler(
            IRecipeRepository recipeRepository,
            IUnitOfWork unitOfWork)
        {
            _recipeRepository = recipeRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Recipe> Handle(CreateRecipeCommand request, CancellationToken cancellationToken)
        {
            // Crear una nueva instancia de Recipe usando los datos del comando
            var recipe = new Recipe(
                request.Name,
                request.ReferenceTemperature,
                request.ReferenceHumidity,
                request.StartDate,
                request.EndDate
            );

            // Agregar la receta al repositorio
            _recipeRepository.AddRecipe(recipe);

            // Guardar los cambios en la base de datos
            _unitOfWork.SaveChanges();

            // Devolver la receta creada
            return Task.FromResult(recipe);
        }
    }
}
