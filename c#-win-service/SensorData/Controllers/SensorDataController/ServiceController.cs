using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataController
{
    public class ServiceController : IController
    {
        #region Properties
        public IDataHandler SensorDataHandler { get; set; }

        public IDataHandler GeocodeDataHandler { get; set; }

        public IDataHandler DataStorageHandler { get; set; }
        #endregion

        #region Constructor
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
