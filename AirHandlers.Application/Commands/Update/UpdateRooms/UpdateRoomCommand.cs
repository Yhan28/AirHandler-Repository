using System;
using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.Rooms.Commands.UpdateRoom
{
    public record UpdateRoomCommand(Room Room) : ICommand;
}
