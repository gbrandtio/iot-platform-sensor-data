using Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorDataController
{
    public class ServiceController
    {
        private IDataHandler sensorDataHandler, geocodeDataHandler, dataStorageHandler;
        public ServiceController(IDataHandler sensorDataHandler, IDataHandler geocodeDataHandler, IDataHandler dataStorageHandler)
        {
            this.sensorDataHandler = sensorDataHandler;
            this.geocodeDataHandler = geocodeDataHandler;
            this.dataStorageHandler = dataStorageHandler;
        }

        /// <summary>
        /// Controls the main flow of the service.
        /// The entry point of the program. Implements the main flow of the service to request/retrieve/store the data.
        /// </summary>
        public void StartDataCollection()
        {
            // Get separate lists for each measurement type. Will help us store different measurement types to their respective tables.
            Dictionary<Type, List<IMeasurement>> dicSeparatedMeasurements = sensorDataHandler.HandleData(new Dictionary<Type, List<IMeasurement>>());

            // Add the city info on each IMeasurement object.
            dicSeparatedMeasurements = geocodeDataHandler.HandleData(dicSeparatedMeasurements);

            // Save the data.
            dataStorageHandler.HandleData(dicSeparatedMeasurements);
        }
    }
}
