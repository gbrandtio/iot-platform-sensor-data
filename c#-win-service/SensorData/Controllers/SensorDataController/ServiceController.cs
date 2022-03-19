using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataController
{
    /// <summary>
    /// Provides functionality that controls the main flow of the application
    /// by calling the HandleData method of each data handler in a specific order.
    /// </summary>
    public class ServiceController : IController
    {
        #region Properties
        /// <summary>
        /// Sensor Data Handler property.
        /// </summary>
        public IDataHandler SensorDataHandler { get; set; }

        /// <summary>
        /// Geocode Data Handler property.
        /// </summary>
        public IDataHandler GeocodeDataHandler { get; set; }

        /// <summary>
        /// Data Storage Handler property.
        /// </summary>
        public IDataHandler DataStorageHandler { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes the values of the internal handlers with the injected values.
        /// </summary>
        /// <param name="sensorDataHandler"></param>
        /// <param name="geocodeDataHandler"></param>
        /// <param name="dataStorageHandler"></param>
        public ServiceController(IDataHandler sensorDataHandler, IDataHandler geocodeDataHandler, IDataHandler dataStorageHandler)
        {
            SensorDataHandler = sensorDataHandler;
            GeocodeDataHandler = geocodeDataHandler;
            DataStorageHandler = dataStorageHandler;
        }
        #endregion

        #region Handle Flow
        /// <summary>
        /// Controls the main flow of the service.
        /// The entry point of the program. Implements the main flow of the service to request/retrieve/store the data.
        /// </summary>
        public void Control()
        {
            // Get separate lists for each measurement type. Will help us store different measurement types to their respective tables.
            Dictionary<Type, List<IMeasurement>> dicSeparatedMeasurements = SensorDataHandler.HandleData(new Dictionary<Type, List<IMeasurement>>());

            // Add the city info on each IMeasurement object.
            dicSeparatedMeasurements = GeocodeDataHandler.HandleData(dicSeparatedMeasurements);

            // Save the data.
            DataStorageHandler.HandleData(dicSeparatedMeasurements);
        }
        #endregion
    }
}
