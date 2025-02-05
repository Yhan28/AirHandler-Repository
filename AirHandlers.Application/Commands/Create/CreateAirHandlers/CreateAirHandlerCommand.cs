using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;
using System;

namespace AirHandlers.Application.AirHandlers.Commands.CreateAirHandler
{
    public record CreateAirHandlerCommand(
        string IdentifierCode,
        bool IsOperating,
        DateTime FilterChangeDate,
        double ReferenceTemperature,
        double ReferenceHumidity) : ICommand<AirHandlerEntity>;
}