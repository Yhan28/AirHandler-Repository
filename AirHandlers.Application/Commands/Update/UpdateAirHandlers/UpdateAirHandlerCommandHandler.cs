using AirHandlers.Application.Abstract;
using AirHandlers.Application.AirHandlers.Commands.UpdateAirHandler;
using AirHandlers.Contracts;
using AirHandlers.Contracts.AirHandlers;
using AirHandlers.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace AirHandlers.Application.AirHandlers.CommandHandlers
{
    public class UpdateAirHandlerCommandHandler
        : ICommandHandler<UpdateAirHandlerCommand>
    {
        private readonly IAirHandlerRepository _airHandlerRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateAirHandlerCommandHandler(
            IAirHandlerRepository airHandlerRepository,
            IUnitOfWork unitOfWork)
        {
            _airHandlerRepository = airHandlerRepository;
            _unitOfWork = unitOfWork;
        }

        public Task Handle(UpdateAirHandlerCommand request, CancellationToken cancellationToken)
        {
            _airHandlerRepository.UpdateAirHandler(request.AirHandler);
            _unitOfWork.SaveChanges();
            return Task.CompletedTask;
        }
    }
}
