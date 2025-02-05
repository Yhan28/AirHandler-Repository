using AirHandlers.Application.Abstract;
using AirHandlers.Application.Rooms.Commands.DeleteRoom;
using AirHandlers.Contracts;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AirHandlers.Application.Rooms.CommandHandlers
{
    public class DeleteRoomCommandHandler
        : ICommandHandler<DeleteRoomCommand>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRoomCommandHandler(
            IRoomRepository roomRepository,
            IUnitOfWork unitOfWork)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteRoomCommand request, CancellationToken cancellationToken)
        {
            // Buscar la habitación por su ID
            var roomToDelete = _roomRepository.GetRoomById<Room>(request.Id);

            // Si no se encuentra, no se realiza ninguna acción
            if (roomToDelete is null)
                return;

            // Eliminar la habitación utilizando el repositorio
            _roomRepository.DeleteRoom(roomToDelete);

            // Guardar los cambios en la base de datos
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
