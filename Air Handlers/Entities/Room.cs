using System;
using Air_Handlers.Domain.Common;

namespace AirHandlers.Domain.Entities
{
    public class Room : Entity
    {
        #region Properties
        public int Number { get; set; }
        public double Volume { get; set; } // Volumen en metros cúbicos

        // Propiedad de navegación para establecer relación con AirHandler
        public Guid AssociatedHandlerId { get; set; }  // Clave foránea para la relación con AirHandler
        public AirHandlerEntity AssociatedHandler { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Modela la clase Room.
        /// </summary>
        /// <param name="number"></param>
        /// <param name="volume"></param>
        public Room(int number, double volume)
        {
            Number = number;
            Volume = volume;
            // AssociatedHandlerId se puede establecer después al asignar una manejadora.
        }
        #endregion
    }
}