using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Factories;
using Interfaces;

namespace SensorData
{
    public class Service
    {
        /// <summary>
        /// Controls the main flow of the service.
        /// The entry point of the program. Implements the main flow of the service to request/retrieve/store the data.
        /// </summary>
        public static void StartDataCollection(IDataHandler sensorDataHandler, IDataHandler geocodeDataHandler, IDataHandler dataStorageHandler)
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
