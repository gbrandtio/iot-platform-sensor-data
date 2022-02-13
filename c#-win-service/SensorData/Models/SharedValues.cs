using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public static class SharedValues
    {
        // Geocode API related constants.
        public const string GEO_API_KEY = "GEO_API_KEY";
        public const string GEO_API_URL_LATLANG = @"https://maps.googleapis.com/maps/api/geocode/xml?latlng=";
        public const string URL_KEY_PARAM = @"&key=";

        // Constant strings
        public const string UNKNOWN = "unknown";
        public const string TIMER_INTERVAL = "TimerInterval";
        public const string FORMATTED_ADDRESS = "formatted_address";

        // Variables that need to be injected globally
        public static string GL_GEO_API_KEY { get; set; }
    }
}
