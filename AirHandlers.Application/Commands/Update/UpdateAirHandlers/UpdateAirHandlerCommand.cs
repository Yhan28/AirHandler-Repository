using System;
using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.AirHandlers.Commands.UpdateAirHandler
{
    public record UpdateAirHandlerCommand(AirHandlerEntity AirHandler) : ICommand;
}
