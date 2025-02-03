using AirHandlers.Contracts.AirHandlers;
using AirHandlers.DataAccess.Contexts;
using AirHandlers.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirHandlers.Data.Repositories
{
    public class AirHandlerRepository : RepositoryBase<AirHandler>, IAirHandlerRepository
    {
        public AirHandlerRepository(ApplicationDbContext context) : base(context) { }

        public void AddAirHandler(AirHandler airHandler) => AddAsync(airHandler).Wait();

        public T? GetAirHandlerById<T>(Guid id) where T : AirHandler
          => GetByIdAsync(id).Result as T;

        public IEnumerable<T> GetAllAirHandler<T>() where T : AirHandler
          => GetAllAsync().Result as IEnumerable<T>;

        public void UpdateAirHandler(AirHandler airHandler)
          => UpdateAsync(airHandler).Wait();

        public void DeleteAirHandler(Guid id)
          => DeleteAsync(id).Wait();
    }
}