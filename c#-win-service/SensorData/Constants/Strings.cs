using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Constants
{
    /// <summary>
    /// Provides a representation of strongly typed common strings, configuration properties and 
    /// properties related to the APIs used (Geocode, Sensor).
    /// </summary>
    public class Strings : Constants
    {
        /// <summary>
        /// Common used strings.
        /// </summary>
        public partial class String
        {
            private String(string value)
            {
                this.Value = value;
            }

            /// <summary>
            /// Internal value as string.
            /// </summary>
            public string Value { get; private set; }
            /// <summary>
            /// Unknown.
            /// </summary>
            public static String Unknown { get { return new String(R_UNKNOWN); } }
            /// <summary>
            /// appSettings.
            /// </summary>
            public static String AppSettings { get { return new String(R_APP_SETTINGS); } }
            /// <summary>
            /// add.
            /// </summary>
            public static String Add { get { return new String(R_ADD); } }
            /// <summary>
            /// key.
            /// </summary>
            public static String Key { get { return new String(R_KEY); } }
            /// <summary>
            /// results.
            /// </summary>
            public static String Results { get { return new String(R_RESULTS); } }
            /// <summary>
            /// location.
            /// </summary>
            public static String Location { get { return new String(R_LOCATION); } }
            /// <summary>
            /// country.
            /// </summary>
            public static String Country { get { return new String(R_COUNTRY); } }
            /// <summary>
            /// longitude.
            /// </summary>
            public static String Longitude { get { return new String(R_LONGITUDE); } }
            /// <summary>
            /// latitude.
            /// </summary>
            public static String Latitude { get { return new String(R_LATITUDE); } }
            /// <summary>
            /// value.
            /// </summary>
            public static String ValueR { get { return new String(R_VALUE); } }
        }

        /// <summary>
        /// Common dictionary of the Geocode API.
        /// </summary>
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

            /// <summary>
            /// Internal value as string.
            /// </summary>
            public string Value { get; private set; }
            /// <summary>
            /// Internal value as int.
            /// </summary>
            public int ValueInt { get; private set; }
            /// <summary>
            /// The url of Geocode API.
            /// </summary>
            public static Geocode GeocodeURL { get { return new Geocode(R_GEOURL_LATLANG); } }
            /// <summary>
            /// &key=
            /// </summary>
            public static Geocode URLKeyParam { get { return new Geocode(R_URL_KEY_PARAM); } }
            /// <summary>
            /// formatted_address
            /// </summary>
            public static Geocode FormattedAddress { get { return new Geocode(R_FORMATTED_ADDRESS); } }
            /// <summary>
            /// Position of the City information in the split formatted_address string.
            /// </summary>
            public static Geocode InfoCity { get { return new Geocode(G_GEO_INFO_CITY); } }
            /// <summary>
            /// Position of the Country information in the split formatted_address string.
            /// </summary>
            public static Geocode InfoCountry { get { return new Geocode(G_GEO_INFO_COUNTRY); } }
        }

        /// <summary>
        /// Common dictionary for the Sensor API.
        /// </summary>
        public partial class Sensor
        {
            private Sensor(string value)
            {
                this.Value = value;
            }

            /// <summary>
            /// Internal value as string.
            /// </summary>
            public string Value { get; private set; }
            /// <summary>
            /// The configured URL of the sensor API.
            /// </summary>
            public static Sensor SensorApi { get { return new Sensor(ConfigurationManager.AppSettings[R_SENSOR_API]); } }
            /// <summary>
            /// sensordatavalues.
            /// </summary>
            public static Sensor SensorDataValues { get { return new Sensor(R_SENSOR_DATA_VALUES); } }
            /// <summary>
            /// value_type
            /// </summary>
            public static Sensor ValueType { get { return new Sensor(R_VALUE_TYPE); } }
        }

        /// <summary>
        /// Common configuration dictionary.
        /// </summary>
        public partial class Config : Constants
        {
            private Config(string value)
            {
                this.Value = value;
            }

            /// <summary>
            /// Public constructor in cases we need to instantiate a Config object.
            /// </summary>
            /// <param name="value">The value to instantiate the object with.</param>
            public Config(long value)
            {
                this.ValueLong = value;
            }

            /// <summary>
            /// Internal value as string.
            /// </summary>
            public string Value { get; private set; }
            /// <summary>
            /// Internal value as long.
            /// </summary>
            public long ValueLong { get; private set; }
            /// <summary>
            /// The configured key for Geocode API.
            /// </summary>
            public static Config GeoApiKey { get { return new Config(ConfigurationManager.AppSettings[R_GEO_API_KEY]); } }
            /// <summary>
            /// The configured country code (2-letter code).
            /// </summary>
            public static Config CountryCode { get { return new Config(ConfigurationManager.AppSettings[R_COUNTRY_CODE]); } }
            /// <summary>
            /// The configured interval.
            /// </summary>
            public static Config TimerInterval { get { return new Config(ConfigurationManager.AppSettings[R_TIMER_INTERVAL]); } }
            /// <summary>
            /// The configured data storage mode.
            /// </summary>
            public static Config DataStorageMode { get { return new Config(ConfigurationManager.AppSettings[R_DATA_STORAGE_MODE]); } }
            /// <summary>
            /// The configured data storage method.
            /// </summary>
            public static Config DataStorageMethod { get { return new Config(R_DATA_STORAGE_METHOD); } }
            /// <summary>
            /// The path to save the logs.
            /// </summary>
            public static Config LogPath { get { return new Config(ConfigurationManager.AppSettings[R_LOGPATH]); } }
            /// <summary>
            /// The path to save the files in case data storage mode is set to FILE.
            /// </summary>
            public static Config DataFilePath { get { return new Config(ConfigurationManager.AppSettings[R_DATA_FILE_PATH]); } }
            /// <summary>
            /// The configured maximum file size.
            /// </summary>
            public static Config MaxFileSize { get { return new Config(long.Parse(ConfigurationManager.AppSettings[R_MAX_FILESIZE])); } }
        }
    }
}
