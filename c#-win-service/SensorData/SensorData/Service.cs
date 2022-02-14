using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Helpers;
using RestClient;

namespace SensorData
{
    /// <summary>
    /// Controls the main flow logic of the service which is as follows:
    /// 1. Fetch the data from the provided sensor API.
    /// 2. Parse the response data and transform them into meaningful objects.
    /// 3. Save the data to the respective table in the database.
    /// </summary>
    public class Service
    {
        public static void StartDataCollection(string countryCode)
        {
            string sensorData = GET.DoRequest(SharedValues.SENSOR_API + SharedValues.COUNTRY_CODE);
        }
    }
}
