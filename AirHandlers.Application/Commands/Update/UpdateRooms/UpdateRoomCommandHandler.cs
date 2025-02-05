using AirHandlers.Application.Abstract;
using AirHandlers.Application.Rooms.Commands.UpdateRoom;
using AirHandlers.Contracts;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AirHandlers.Application.Rooms.CommandHandlers
{
    public class UpdateRoomCommandHandler
        : ICommandHandler<UpdateRoomCommand>
    {
        private readonly IRoomRepository _roomRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoomCommandHandler(
            IRoomRepository roomRepository,
            IUnitOfWork unitOfWork)
        {
            _roomRepository = roomRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(UpdateRoomCommand request, CancellationToken cancellationToken)
        {
            _roomRepository.UpdateRoom(request.Room);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
