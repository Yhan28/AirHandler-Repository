using System;
using System.Collections.Generic;
using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.Rooms.Queries.GetAllRooms
{
    public record GetAllRoomsQuery : IQuery<IEnumerable<Room>>;
}
