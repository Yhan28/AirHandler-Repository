using AirHandlers.Contracts.Recipes; // Asegúrate de que el espacio de nombres sea correcto.
using AirHandlers.Data.Repositories;
using AirHandlers.Domain.Entities;
using AirHandlers.Services.IServices;
using System;
using System.Collections.Generic;

namespace AirHandlers.Services.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public void AddRoom(Room room)
        {
            _roomRepository.AddRoom(room);
        }

        public Room? GetRoomById(Guid id)
        {
            return _roomRepository.GetRoomById<Room>(id);
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _roomRepository.GetAllRooms<Room>();
        }

        public void UpdateRoom(Room room)
        {
            _roomRepository.UpdateRoom(room);
        }

        public void DeleteRoom(Guid id)
        {
            var room = GetRoomById(id);
            if (room != null)
            {
                _roomRepository.DeleteRoom(room);
            }
            else
            {
                throw new ArgumentException("No se encontró la habitación con el ID especificado.");
            }
        }
    }
}
