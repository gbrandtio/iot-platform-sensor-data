using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers;
using Interfaces;
using Models;
using Models.Config_Models;
using RestClient;

namespace SensorData
{
    /// <summary>
    /// Controls the main flow of the service. The main logic is as follows:
    /// 
    /// 1. Fetch the data from the provided sensor API.
    /// 2. Parse the response data and transform them.
    /// 3. Save the data to the respective table in the database / send them to the configured API.
    /// </summary>
    public class Service
    {
        /// <summary>
        /// The entry point of the program. Implements the main flow of the service and the logic that is being followed.
        /// </summary>
        /// <param name="countryCode">The 2-letter configured country code to retrieve data from.</param>
        /// <param name="dataHandlingMode">The configured mode to handle the data (save them to database, send them to API)</param>
        public static void StartDataCollection(string countryCode)
        {
            // Retrieve the data from the SensorAPI.
            string sensorDataResponse = GET.DoRequest(Strings.Sensor.SensorApi + countryCode);

            // Parse the data and transform them.
            SensorDataHandler sensorDataHandler = new SensorDataHandler();
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
