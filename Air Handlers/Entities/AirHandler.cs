using System;
using System.Collections.Generic;
using System;
using System.Collections.Generic;
using Air_Handlers.Domain.Common;
using AirHandlers.Domain.Relations;

namespace AirHandlers.Domain.Entities
{
    public class AirHandler : Entity
    {
        #region Properties
        public Guid ID { get; set; }  // Clave primaria
        public string IdentifierCode { get; set; } = string.Empty;
        public bool IsOperating { get; set; }
        public DateTime FilterChangeDate { get; set; }
        public double ReferenceTemperature { get; set; }
        public double ReferenceHumidity { get; set; }

        // Relación con la tabla intermedia
        public ICollection<AirHandlerRecipe> AssociatedRecipes { get; set; } = new List<AirHandlerRecipe>();

        // Relación con Room
        public ICollection<Room> ServedRooms { get; set; } = new List<Room>();
        #endregion

        #region Constructor
        /// <summary>
        /// Modela la clase AirHandler
        /// </summary>
        /// <param name="identifierCode"></param>
        /// <param name="isOperating"></param>
        /// <param name="filterChangeDate"></param>
        /// <param name="referenceTemperature"></param>
        /// <param name="referenceHumidity"></param>
        public AirHandler(string identifierCode, bool isOperating, DateTime filterChangeDate,
                          double referenceTemperature, double referenceHumidity)
        {
            IdentifierCode = identifierCode;
            IsOperating = isOperating;
            FilterChangeDate = filterChangeDate;
            ReferenceTemperature = referenceTemperature;
            ReferenceHumidity = referenceHumidity;

            // Inicializar colecciones correctamente
            AssociatedRecipes = new List<AirHandlerRecipe>();
            ServedRooms = new List<Room>();
        }
        #endregion
    }
}