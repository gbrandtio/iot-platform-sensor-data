using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Config_Models
{
    public class Strings : Constants
    {
        public partial class String
        {
            private String(string value)
            {
                this.Value = value;
            }

            public string Value { get; private set; }
            public static String Unknown { get { return new String(R_UNKNOWN); } }
            public static String AppSettings { get { return new String(R_APP_SETTINGS); } }
            public static String Add { get { return new String(R_ADD); } }
            public static String Key { get { return new String(R_KEY); } }
            public static String Results { get { return new String(R_RESULTS); } }
            public static String Location { get { return new String(R_LOCATION); } }
            public static String Country { get { return new String(R_COUNTRY); } }
            public static String Longitude { get { return new String(R_LONGITUDE); } }
            public static String Latitude { get { return new String(R_LATITUDE); } }
            public static String ValueR { get { return new String(R_VALUE); } }
        }

        public partial class Geocode
        {
            private Geocode(string value)
            {
                this.Value = value;
            }

            private Geocode(int value)
            {
                this.ValueInt = value;
            }

            public string Value { get; private set; }
            public int ValueInt { get; private set; }
            public static Geocode GeocodeURL { get { return new Geocode(R_GEOURL_LATLANG); } }
            public static Geocode URLKeyParam { get { return new Geocode(R_URL_KEY_PARAM); } }
            public static Geocode FormattedAddress { get { return new Geocode(R_FORMATTED_ADDRESS); } }
            public static Geocode InfoCity { get { return new Geocode(G_GEO_INFO_CITY); } }
            public static Geocode InfoCountry { get { return new Geocode(G_GEO_INFO_COUNTRY); } }
        }

        public partial class Sensor
        {
            private Sensor(string value)
            {
                this.Value = value;
            }

            public string Value { get; private set; }
            public static Sensor SensorApi { get { return new Sensor(ConfigurationManager.AppSettings[R_SENSOR_API]); } }
            public static Sensor SensorDataValues { get { return new Sensor(R_SENSOR_DATA_VALUES); } }
            public static Sensor ValueType { get { return new Sensor(R_VALUE_TYPE); } }
        }

        public partial class Config : Constants
        {
            private Config(string value)
            {
                this.Value = value;
            }

            public Config(long value)
            {
                this.ValueLong = value;
            }
            public string Value { get; private set; }
            public long ValueLong { get; private set; }
            public static Config GeoApiKey { get { return new Config(ConfigurationManager.AppSettings[R_GEO_API_KEY]); } }
            public static Config CountryCode { get { return new Config(ConfigurationManager.AppSettings[R_COUNTRY_CODE]); } }
            public static Config TimerInterval { get { return new Config(R_TIMER_INTERVAL); } }
            public static Config DataStorageMode { get { return new Config(R_DATA_STORAGE_MODE); } }
            public static Config DataStorageMethod { get { return new Config(R_DATA_STORAGE_METHOD); } }
            public static Config LogPath { get { return new Config(ConfigurationManager.AppSettings[R_LOGPATH]); } }
            public static Config DataFilePath { get { return new Config(ConfigurationManager.AppSettings[R_DATA_FILE_PATH]); } }
            public static Config MaxFileSize { get { return new Config(long.Parse(ConfigurationManager.AppSettings[R_MAX_FILESIZE])); } }
        }
    }
}
