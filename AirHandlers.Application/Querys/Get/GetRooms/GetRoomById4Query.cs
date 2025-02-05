using System;
using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.Rooms.Queries.GetRoomById
{
    public record GetRoomByIdQuery(Guid Id) : IQuery<Room?>;
}
