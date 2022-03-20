using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    /// <summary>
    /// Provides a list of constant values that are used throughout the application.
    /// </summary>
    public class Constants
    {
        // Geocode API
        /// <summary>
        /// The URL of the Geocode API.
        /// </summary>
        protected const string R_GEOURL_LATLANG = @"https://maps.googleapis.com/maps/api/geocode/xml?latlng=";
        /// <summary>
        /// URL key parameter.
        /// </summary>
        protected const string R_URL_KEY_PARAM = @"&key=";
        /// <summary>
        /// Specific to the Geocode API response. A JSON field that contains the full address of the location
        /// as it's value.
        /// </summary>
        protected const string R_FORMATTED_ADDRESS = "formatted_address";
        /// <summary>
        /// Specifies the position in the array where the City information can be found.
        /// </summary>
        protected const int G_GEO_INFO_CITY = 1;
        /// <summary>
        /// Specifies the position in the array where the Country information can be found.
        /// </summary>
        protected const int G_GEO_INFO_COUNTRY = 2;

        // Sensor API
        /// <summary>
        /// Specific to the Sensor API response. The array that contains the information
        /// about the sensor measurements.
        /// </summary>
        protected const string R_SENSOR_DATA_VALUES = "sensordatavalues";
        /// <summary>
        /// The field to select from the sensordatavalues array. This field contains 
        /// the type of the measurement (e.g. humidity, temperature etc).
        /// </summary>
        protected const string R_VALUE_TYPE = "value_type";

        // Configuration
        /// <summary>
        /// Configuration property that contains the private key to be used when interacting 
        /// with the Geocode API.
        /// </summary>
        protected const string R_GEO_API_KEY = "GEO_API_KEY";
        /// <summary>
        /// Configuration property that specifies the country code (2-letter code) to select 
        /// data from when using the Sensor API.
        /// </summary>
        protected const string R_COUNTRY_CODE = "CountryCode";
        /// <summary>
        /// Configuration property that specifies the interval that the service will query, parse, extract, save
        /// the data.
        /// </summary>
        protected const string R_TIMER_INTERVAL = "TimerInterval";
        /// <summary>
        /// Configuration property that specifies the Sensor API to retrieve data from.
        /// </summary>
        protected const string R_SENSOR_API = "SensorAPI";
        /// <summary>
        /// Configuration property that specifies the current (active) data storage method.
        /// </summary>
        protected const string R_DATA_STORAGE_MODE = "DataStorageMode";
        /// <summary>
        /// Configuration property that specifies all the possible data storage methods.
        /// </summary>
        protected const string R_DATA_STORAGE_METHOD = "DataStorageMethod";
        /// <summary>
        /// The path where the application logs will be created and updated.
        /// </summary>
        protected const string R_LOGPATH = "LogPath";
        /// <summary>
        /// The maximum allowed file size of the log files.
        /// </summary>
        protected const string R_MAX_FILESIZE = "MaxFileSize";
        /// <summary>
        /// In case of FILE data storage method, this configuration property will specify where 
        /// the data files will be created and updated.
        /// </summary>
        protected const string R_DATA_FILE_PATH = "DataFilePath";

        // Storage Methods
        /// <summary>
        /// Will store data into a database using the Entity framework.
        /// </summary>
        protected const string R_ENTITY = "ENTITY";
        /// <summary>
        /// Will send the data in an API in order to be stored there.
        /// </summary>
        protected const string R_API = "Api";
        /// <summary>
        /// Will store all the data into files.
        /// </summary>
        protected const string R_FILE = "File";


        // Strings
        #pragma warning disable CS1591
        protected const string R_UNKNOWN = "unknown";
        protected const string R_APP_SETTINGS = "appSettings";
        protected const string R_ADD = "add";
        protected const string R_KEY = "key";
        protected const string R_RESULTS = "results";
        protected const string R_LOCATION = "location";
        protected const string R_COUNTRY = "country";
        protected const string R_LONGITUDE = "longitude";
        protected const string R_LATITUDE = "latitude";
        protected const string R_VALUE = "value";
        #pragma warning restore CS1591
    }
}
