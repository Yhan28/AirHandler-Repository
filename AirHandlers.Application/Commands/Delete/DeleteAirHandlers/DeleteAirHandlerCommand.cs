using AirHandlers.Application.Abstract;
using System;

namespace AirHandlers.Application.AirHandlers.Commands.DeleteAirHandler
{
    public record DeleteAirHandlerCommand(Guid Id) : ICommand;
}
