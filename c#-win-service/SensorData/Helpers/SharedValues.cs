﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public static class SharedValues
    {
        // Geocode API
        public const string GEO_API_URL_LATLANG = @"https://maps.googleapis.com/maps/api/geocode/xml?latlng=";
        public const string GEO_API_URL_KEY_PARAM = @"&key=";
        public const string ARRAY_RESULTS = "results";
        public const string FORMATTED_ADDRESS = "formatted_address";
        public const int GEO_INFO_CITY = 1;
        public const int GEO_INFO_COUNTRY = 2;

        // Sensor API
        public const string SENSOR_DATA_VALUES = "sensordatavalues";
        public const string LOCATION = "location";
        public const string COUNTRY = "country";
        public const string LONGITUDE = "longitude";
        public const string LATITUDE = "latitude";
        public const string VALUE_TYPE = "value_type";
        public const string VALUE = "value";

        // Configuration
        public const string GEO_API_KEY = "GEO_API_KEY";
        public const string COUNTRY_CODE = "CountryCode";
        public const string TIMER_INTERVAL = "TimerInterval";
        public const string SENSOR_API = "SensorAPI";
        public const string DATA_HANDLING_MODE = "DataHandlingMode";
        public const string DATA_CONF_ENTITY = "ENTITY";
        public const string DATA_CONF_API = "API";
        public const string DATA_CONF_FILE = "FILE";

        // Defaults
        public const string UNKNOWN = "unknown";

        // Inject Variables
        public static string GL_GEO_API_KEY { get; set; }
    }
}
