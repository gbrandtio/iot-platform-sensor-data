using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handlers;
using Helpers;
using Interfaces;
using Models;
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
        public static void StartDataCollection(string countryCode, string dataHandlingMode)
        {
            // Retrieve the data from the SensorAPI.
            string sensorDataResponse = GET.DoRequest(SharedValues.SENSOR_API + countryCode);

            // Parse the data and transform them.
            SensorDataHandler sensorDataHandler = new SensorDataHandler();
            List<IMeasurement> allMeasurements = sensorDataHandler.ExtractSensorDataValues(sensorDataResponse);

            // Add the city info on each IMeasurement object.
            GeocodeDataHandler geocodeDataHandler = new GeocodeDataHandler();
            foreach (IMeasurement measurement in allMeasurements)
            {
                if (measurement != null)
                    measurement.Location.City = geocodeDataHandler.FindLocationInfo(measurement.Location.Longitude, measurement.Location.Latitude);
            }

            // Get separate lists for each measurement type. Will help us store different measurement types to their respective tables.
            List<IMeasurement> humidityMeasurements = sensorDataHandler.GetSpecificMeasurementObjectList(allMeasurements, typeof(Humidity));
            List<IMeasurement> p1Measurements = sensorDataHandler.GetSpecificMeasurementObjectList(allMeasurements, typeof(P1));
            List<IMeasurement> p2Measurements = sensorDataHandler.GetSpecificMeasurementObjectList(allMeasurements, typeof(Pressure));
            List<IMeasurement> pressureMeasurements = sensorDataHandler.GetSpecificMeasurementObjectList(allMeasurements, typeof(IMeasurement));

            // Save the data to the database.
            if (dataHandlingMode.Equals(SharedValues.DATA_CONF_ENTITY))
            {
                // Save data to the database that is specified by Entity Framework config.
            }
            else if (dataHandlingMode.Equals(SharedValues.DATA_CONF_API))
            {
                // Send data to the configured API and let the API store them in the database.
                throw new NotImplementedException();
            }
            else if (dataHandlingMode.Equals(SharedValues.DATA_CONF_FILE))
            {
                // Format the data and write them to the configured file.
                throw new NotImplementedException();
            }
        }
    }
}
