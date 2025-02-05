using AirHandlers.Application.Abstract;
using AirHandlers.Application.Rooms.Queries.GetAllRooms;
using AirHandlers.Contracts.Recipes;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.Rooms.CommandHandlers
{
    public class GetAllRoomsQueryHandler
        : IQueryHandler<GetAllRoomsQuery, IEnumerable<Room>>
    {
        private readonly IRoomRepository _roomRepository;

        public GetAllRoomsQueryHandler(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public Task<IEnumerable<Room>> Handle(GetAllRoomsQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_roomRepository.GetAllRooms<Room>());
        }
    }
}
