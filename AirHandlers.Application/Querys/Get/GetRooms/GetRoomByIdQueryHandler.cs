using System.Threading;
using System.Threading.Tasks;
using AirHandlers.Application.Abstract;
using AirHandlers.Application.Rooms.Queries.GetRoomById;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.Rooms.CommandHandlers
{
    public class GetRoomByIdQueryHandler
        : IQueryHandler<GetRoomByIdQuery, Room?>
    {
        private readonly IRoomRepository _roomRepository;

        public GetRoomByIdQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public Task<Room?> Handle(GetRoomByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_roomRepository.GetRoomById<Room>(request.Id));
        }
    }
}
