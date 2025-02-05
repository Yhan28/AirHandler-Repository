using AirHandlers.Contracts.AirHandlers;
using AirHandlers.Data.Repositories;
using AirHandlers.Domain.Entities;
using System;
using System.Collections.Generic;
using AirHandlers.Services.IServices;
namespace AirHandlers.Services.Services
{
    public class AirHandlerService : IAirHandlersServices // Asegúrate de que sea IAirHandlerService
    {
        private readonly IAirHandlerRepository _airHandlerRepository;

        public AirHandlerService(IAirHandlerRepository airHandlerRepository)
        {
            _airHandlerRepository = airHandlerRepository;
        }

        public void AddAirHandler(AirHandlerEntity airHandler)
        {
            _airHandlerRepository.AddAirHandler(airHandler);
        }

        public AirHandlerEntity? GetAirHandlerById(Guid id)
        {
            return _airHandlerRepository.GetAirHandlerById<AirHandlerEntity>(id);
        }

        public IEnumerable<AirHandlerEntity> GetAllAirHandlers()
        {
            return _airHandlerRepository.GetAllAirHandler<AirHandlerEntity>();
        }

        public void UpdateAirHandler(AirHandlerEntity airHandler)
        {
            _airHandlerRepository.UpdateAirHandler(airHandler);
        }

        public void DeleteAirHandler(Guid id)
        {
            _airHandlerRepository.DeleteAirHandler(id);
        }
    }
}