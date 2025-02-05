using AirHandlers.Domain.Entities;
using System;
using System.Collections.Generic;

namespace AirHandlers.Services.IServices
{
    public interface IAirHandlersServices
    {
        void AddAirHandler(AirHandlerEntity airHandler);
        AirHandlerEntity? GetAirHandlerById(Guid id);
        IEnumerable<AirHandlerEntity> GetAllAirHandlers();
        void UpdateAirHandler(AirHandlerEntity airHandler);
        void DeleteAirHandler(Guid id);
    }
}