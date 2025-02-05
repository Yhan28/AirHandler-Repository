using AirHandlers.Application.Abstract;
using AirHandlers.Application.Rooms.Commands.CreateRoom;
using AirHandlers.Contracts;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AirHandlers.Application.Rooms.CommandHandlers
{
    public class CreateRoomCommandHandler
        : ICommandHandler<CreateRoomCommand, Room>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoomCommandHandler(
            IRoomRepository roomRepository,
            IUnitOfWork unitOfWork)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<Room> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            // Crear una nueva instancia de Room usando los datos del comando
            var room = new Room(
                request.Number,
                request.Volume
            )
            {
                AssociatedHandlerId = request.AssociatedHandlerId // Establecer la relación con el AirHandler
            };

            // Agregar la habitación al repositorio
            _roomRepository.AddRoom(room);

            // Guardar los cambios en la base de datos
            _unitOfWork.SaveChanges();

            // Devolver la habitación creada
            return Task.FromResult(room);
        }
    }
}
