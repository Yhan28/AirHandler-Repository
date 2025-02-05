using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AirHandlers.Application.Abstract;
using AirHandlers.Application.AirHandlers.Queries.GetAllAirHandlers;
using AirHandlers.Contracts.AirHandlers;
using AirHandlers.Domain.Entities;

namespace AirHandlers.Application.AirHandlers.CommandHandlers
{
    public class GetAllAirHandlersQueryHandler
        : IQueryHandler<GetAllAirHandlersQuery, IEnumerable<AirHandlerEntity>>
    {
        private readonly IAirHandlerRepository _airHandlerRepository;

        public GetAllAirHandlersQueryHandler(IAirHandlerRepository airHandlerRepository)
        {
            _airHandlerRepository = airHandlerRepository;
        }

        public Task<IEnumerable<AirHandlerEntity>> Handle(GetAllAirHandlersQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_airHandlerRepository.GetAllAirHandler<AirHandlerEntity>());
        }
    }
}
