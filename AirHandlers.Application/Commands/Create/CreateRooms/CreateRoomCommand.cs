using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.Rooms.Commands.CreateRoom
{
    public record CreateRoomCommand(
        int Number,
        double Volume,
        Guid AssociatedHandlerId) : ICommand<Room>;
}
