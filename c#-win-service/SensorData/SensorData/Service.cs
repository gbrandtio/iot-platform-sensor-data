using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers;
using Interfaces;

namespace SensorData
{
    public class Service
    {
        /// <summary>
        /// Controls the main flow of the service.
        /// The entry point of the program. Implements the main flow of the service to request/retrieve/store the data.
        /// </summary>
        public static void StartDataCollection()
        {
            SensorDataHandler sensorDataHandler = new SensorDataHandler();
            // Retrieve the data from the SensorAPI.
            string sensorDataResponse = sensorDataHandler.RetrieveSensorDataValues();

            // Parse the data and transform them.
            List<IMeasurement> allMeasurements = sensorDataHandler.ExtractSensorDataValues(sensorDataResponse);

            // Add the city info on each IMeasurement object.
            GeocodeDataHandler geocodeDataHandler = new GeocodeDataHandler();
            geocodeDataHandler.AddLocationInfo(allMeasurements);

            // Get separate lists for each measurement type. Will help us store different measurement types to their respective tables.
            Dictionary<Type, List<IMeasurement>> dicSeparatedMeasurements = sensorDataHandler.GetSeparatedMeasurementLists(allMeasurements);

            // Save the data.
            DataStorageHandler dataStorageHandler = new DataStorageHandler();
            dataStorageHandler.StoreData(dicSeparatedMeasurements);
        }
    }
}
