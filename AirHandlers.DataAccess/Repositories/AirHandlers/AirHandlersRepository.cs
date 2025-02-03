using AirHandlers.Contracts.AirHandlers;
using AirHandlers.DataAccess.Contexts;
using AirHandlers.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AirHandlers.Data.Repositories
{
    public class AirHandlerRepository : RepositoryBase<AirHandlerEntity>, IAirHandlerRepository
    {
        public AirHandlerRepository(ApplicationDbContext context) : base(context) { }

        public void AddAirHandler(AirHandlerEntity airHandler) => AddAsync(airHandler).Wait();

        public T? GetAirHandlerById<T>(Guid id) where T : AirHandlerEntity
          => GetByIdAsync(id).Result as T;

        public IEnumerable<T> GetAllAirHandler<T>() where T : AirHandlerEntity
          => GetAllAsync().Result as IEnumerable<T>;

        public void UpdateAirHandler(AirHandlerEntity airHandler)
          => UpdateAsync(airHandler).Wait();

        public void DeleteAirHandler(Guid id)
          => DeleteAsync(id).Wait();
    }
}