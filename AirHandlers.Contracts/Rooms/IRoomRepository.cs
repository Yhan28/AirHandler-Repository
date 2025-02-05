using AirHandlers.Domain.Entities;
using System.Collections.Generic;

namespace AirHandlers.Contracts.Recipes
{
    public interface IRoomRepository
    {
        /// <summary>
        /// Crea una nueva Room en la base de datos.
        /// </summary>
        /// <param name="room">The Recipe object to be created.</param>
        void AddRoom(Room room);

        /// <summary>
        /// Crea una nueva Room en la base de datos
        /// </summary>
        /// <param name="room">The Room object to be created.</param>
        void CreateRoom(Room room);

        /// <summary>
        /// Recibe una room por su ID
        /// </summary>
        /// <param name="id">The ID of the Room to retrieve.</param>
        /// <returns>The Room object if found; otherwise, null.</returns>
        T? GetRoomById<T>(Guid id) where T : Room;

        /// <summary>
        /// Recibe todas las room de la base de datos
        /// </summary>
        /// <returns>A collection of all Rooms.</returns>
        public IEnumerable<T> GetAllRooms<T>() where T : Room;

        /// <summary>
        /// Actualiza una room existente en la base de datos
        /// </summary>
        /// <param name="room">The Room object with updated values.</param>
        void UpdateRoom(Room room);

        /// <summary>
        /// Elimina una room de la base de datos por su ID
        /// </summary>
        /// <param name="id">The ID of the Room to delete.</param>
        void DeleteRoom(Room room);
    }
}