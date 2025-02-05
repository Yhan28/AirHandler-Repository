using System;
using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.AirHandlers.Queries.GetAirHandlerById
{
    public record GetAirHandlerByIdQuery(Guid Id) : IQuery<AirHandlerEntity?>;
}