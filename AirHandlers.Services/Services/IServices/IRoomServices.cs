using AirHandlers.Domain.Entities;
using System;
using System.Collections.Generic;

namespace AirHandlers.Services.IServices
{
    public interface IRoomService
    {
        void AddRoom(Room room);
        Room? GetRoomById(Guid id);
        IEnumerable<Room> GetAllRooms();
        void UpdateRoom(Room room);
        void DeleteRoom(Guid id); // Cambiado a Guid para eliminar por ID
    }
}
