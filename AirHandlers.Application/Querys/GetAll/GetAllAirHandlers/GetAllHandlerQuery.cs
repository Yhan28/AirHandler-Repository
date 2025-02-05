using System;
using System.Collections.Generic;
using AirHandlers.Application.Abstract;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.AirHandlers.Queries.GetAllAirHandlers
{
    public record GetAllAirHandlersQuery : IQuery<IEnumerable<AirHandlerEntity>>;
}
