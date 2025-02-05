using System.Threading;
using System.Threading.Tasks;
using AirHandlers.Application.Abstract;
using AirHandlers.Application.AirHandlers.Queries.GetAirHandlerById;
using AirHandlers.Contracts.AirHandlers;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.AirHandlers.CommandHandlers
{
    public class GetAirHandlerByIdQueryHandler
        : IQueryHandler<GetAirHandlerByIdQuery, AirHandlerEntity?>
    {
        private readonly IAirHandlerRepository _airHandlerRepository;

        public GetAirHandlerByIdQueryHandler(IAirHandlerRepository airHandlerRepository)
        {
            _airHandlerRepository = airHandlerRepository;
        }

        public Task<AirHandlerEntity?> Handle(GetAirHandlerByIdQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_airHandlerRepository.GetAirHandlerById<AirHandlerEntity>(request.Id));
        }
    }
}
