using System;
using System.Collections.Generic;
using Air_Handlers.Domain.Common;
using AirHandlers.Domain.Relations;

namespace AirHandlers.Domain.Entities
{
    public class Recipe : Entity
    {
        #region Properties
        public Guid ID { get; set; }  // Clave primaria
        public string Name { get; set; } = string.Empty;
        public double ReferenceTemperature { get; set; }
        public double ReferenceHumidity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        // Relación con la tabla intermedia
        public ICollection<AirHandlerRecipe> ApplicableHandlers { get; set; } = new List<AirHandlerRecipe>();
        #endregion

        #region Constructor

        /// <summary>
        /// Modela el constructor de la clase Recipe
        /// </summary>
        /// <param name="name"></param>
        /// <param name="referenceTemperature"></param>
        /// <param name="referenceHumidity"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>        
        public Recipe(string name, double referenceTemperature, double referenceHumidity,
                      DateTime startDate, DateTime endDate)
        {
            Name = name;
            ReferenceTemperature = referenceTemperature;
            ReferenceHumidity = referenceHumidity;
            StartDate = startDate;
            EndDate = endDate;

            // Inicializar colecciones correctamente
            ApplicableHandlers = new List<AirHandlerRecipe>();
        }

        #endregion
    }
}