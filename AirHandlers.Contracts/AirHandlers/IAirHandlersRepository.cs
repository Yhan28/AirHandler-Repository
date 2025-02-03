using AirHandlers.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirHandlers.Contracts.AirHandlers
{
    public interface IAirHandlerRepository
    {
        /// <summary>
        /// Crea un nuevo AirHandler en la base de datos.
        /// </summary>
        /// <param name="airHandler">The AirHandler object to be created.</param>
        void AddAirHandler(AirHandlerEntity airHandler);


        /// <summary>
        /// Recibe un AirHanlder por el ID.
        /// </summary>
        /// <param name="id">The ID of the AirHandler to retrieve.</param>
        /// <returns>The AirHandler object if found; otherwise, null.</returns>
        T? GetAirHandlerById<T>(Guid id) where T : AirHandlerEntity;


        /// <summary>
        /// Recibe todos los AirHandlers de la base de datos.
        /// </summary>
        /// <returns>A collection of all AirHandlers.</returns>
        public IEnumerable<T> GetAllAirHandler<T>() where T:AirHandlerEntity;


        /// <summary>
        /// Actualiza un AirHandler en la base de datos.
        /// </summary>
        /// <param name="airHandler">The AirHandler object with updated values.</param>
        void UpdateAirHandler(AirHandlerEntity airHandler);


        /// <summary>
        /// Elimina un AirHandlers de la base de datos por su id.
        /// </summary>
        /// <param name="id">The ID of the AirHandler to delete.</param>
        void DeleteAirHandler(Guid id);
    }
}
