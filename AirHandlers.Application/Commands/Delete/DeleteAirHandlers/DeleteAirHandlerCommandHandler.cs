using AirHandlers.Application.Abstract;
using AirHandlers.Application.AirHandlers.Commands.DeleteAirHandler;
using AirHandlers.Contracts;
using AirHandlers.Contracts.AirHandlers;
using AirHandlers.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AirHandlers.Application.AirHandlers.CommandHandlers
{
    public class DeleteAirHandlerCommandHandler
        : ICommandHandler<DeleteAirHandlerCommand>
    {
        private readonly IAirHandlerRepository _airHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAirHandlerCommandHandler(
            IAirHandlerRepository airHandlerRepository,
            IUnitOfWork unitOfWork)
        {
            _airHandlerRepository = airHandlerRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteAirHandlerCommand request, CancellationToken cancellationToken)
        {
            // Buscar el manejador de aire por su ID
            var airHandlerToDelete = _airHandlerRepository.GetAirHandlerById<AirHandlerEntity>(request.Id);

            // Si no se encuentra, no se realiza ninguna acción
            if (airHandlerToDelete is null)
                return;

            // Eliminar el manejador de aire utilizando el repositorio
            _airHandlerRepository.DeleteAirHandler(request.Id);

            // Guardar los cambios en la base de datos (sin CancellationToken)
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
