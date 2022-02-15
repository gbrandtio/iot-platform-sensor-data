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
    /// Controls the main flow logic of the service which is as follows:
    /// 1. Fetch the data from the provided sensor API.
    /// 2. Parse the response data and transform them.
    /// 3. Save the data to the respective table in the database.
    /// </summary>
    public class Service
    {
        public static void StartDataCollection(string countryCode)
        {
            // Retrieve the data.
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

            // Save the data to the database.
        }
    }
}
