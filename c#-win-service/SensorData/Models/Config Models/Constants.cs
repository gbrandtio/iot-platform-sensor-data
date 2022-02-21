using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config_Models
{
    public class Constants
    {
        // Geocode API
        protected const string R_GEOURL_LATLANG = @"https://maps.googleapis.com/maps/api/geocode/xml?latlng=";
        protected const string R_URL_KEY_PARAM = @"&key=";
        protected const string R_FORMATTED_ADDRESS = "formatted_address";
        protected const int G_GEO_INFO_CITY = 1;
        protected const int G_GEO_INFO_COUNTRY = 2;

        // Sensor API
        protected const string R_SENSOR_DATA_VALUES = "sensordatavalues";
        protected const string R_VALUE_TYPE = "value_type";

        // Configuration
        protected const string R_GEO_API_KEY = "GEO_API_KEY";
        protected const string R_COUNTRY_CODE = "CountryCode";
        protected const string R_TIMER_INTERVAL = "TimerInterval";
        protected const string R_SENSOR_API = "SensorAPI";
        protected const string R_DATA_STORAGE_MODE = "DataHandlingMode";
        protected const string R_DATA_STORAGE_METHOD = "DataStorageMethod";
        protected const string R_LOGPATH = "LogPath";
        protected const string R_MAX_FILESIZE = "MaxFileSize";
        protected const string R_DATA_FILE_PATH = "DataFilePath";

        // Storage Methods
        protected const string R_ENTITY = "ENTITY";
        protected const string R_API = "Api";
        protected const string R_FILE = "File";

        // Strings
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
    }
}
