using AirHandlers.Application.AirHandlers.Commands.CreateAirHandler;
using AirHandlers.Domain.Entities;
using System;
using System.Threading;
using System.Threading.Tasks;
using AirHandlers.Application.Abstract;
using AirHandlers.Contracts.AirHandlers;
using AirHandlers.Contracts;

namespace AirHandlers.Application.AirHandlers.CommandHandlers
{
    public class CreateAirHandlerCommandHandler
        : ICommandHandler<CreateAirHandlerCommand, AirHandlerEntity>
    {
        private readonly IAirHandlerRepository _airHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateAirHandlerCommandHandler(
            IAirHandlerRepository airHandlerRepository,
            IUnitOfWork unitOfWork)
        {
            _airHandlerRepository = airHandlerRepository;
            _unitOfWork = unitOfWork;
        }

        public Task<AirHandlerEntity> Handle(CreateAirHandlerCommand request, CancellationToken cancellationToken)
        {
            // Crear una nueva instancia de AirHandlerEntity usando los datos del comando
            var airHandler = new AirHandlerEntity(
                request.IdentifierCode,
                request.IsOperating,
                request.FilterChangeDate,
                request.ReferenceTemperature,
                request.ReferenceHumidity
            );

            // Agregar la entidad al repositorio
            _airHandlerRepository.AddAirHandler(airHandler);

            // Guardar los cambios en la base de datos
            _unitOfWork.SaveChanges();

            // Devolver la entidad creada
            return Task.FromResult(airHandler);
        }
    }
}
