using System;
using AirHandlers.Application.Abstract;

namespace AirHandlers.Application.Rooms.Commands.DeleteRoom
{
    public record DeleteRoomCommand(Guid Id) : ICommand;
}
