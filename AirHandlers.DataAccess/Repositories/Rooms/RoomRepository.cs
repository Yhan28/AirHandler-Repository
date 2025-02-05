using AirHandlers.Contracts.Recipes;
using AirHandlers.DataAccess.Contexts;
using AirHandlers.Domain.Entities;
using System.Collections.Generic;

namespace AirHandlers.Data.Repositories
{
    public class RoomRepository : RepositoryBase<Room>, IRoomRepository
    {
        public RoomRepository(ApplicationDbContext context) : base(context) { }

        public void AddRoom(Room room) => AddAsync(room).Wait();
        public void CreateRoom(Room room) => AddAsync(room).Wait();

        public T? GetRoomById<T>(Guid id) where T : Room
          => GetByIdAsync(id).Result as T;

        public IEnumerable<T> GetAllRooms<T>() where T : Room
          => GetAllAsync().Result as IEnumerable<T>;

        public void UpdateRoom(Room room)
          => UpdateAsync(room).Wait();

        public void DeleteRoom(Room room)
          => DeleteAsync(room.Id).Wait(); // Cambiado de "ID" a "Id"
    }
}