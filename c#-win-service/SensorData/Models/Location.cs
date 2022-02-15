using RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Helpers;
using Interfaces;
using Parsers;

namespace Models
{
    /// <summary>
    /// Represents a location on the map based on the longitude, latitude and contry. Based on the provided 
    /// longitude and latitude, this class will find and store the city that the specified location belongs to.
    /// Members: 
    /// - Longitude: The longitude of the specific location.
    /// - Latitude: The latitude of the location.
    /// - Country: The country of the location.
    /// - City: The city that the location belongs to.
    /// Provides also the ability to find location info based on the provided longitude and latitude via Google Geocoding API.
    /// </summary>
    public class Location : ILocation
    {
        #region Constructor
        public Location() {}
        public Location(double longitude, double latitude, string country)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Country = country;
            this.City = FindLocationInfo(this.Longitude, this.Latitude);
        }
        #endregion

        #region Properties
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
        #endregion

        #region Reverse Geocoding
        public string FindLocationInfo(double longitude, double latitude)
        {
            string localeInfo = SharedValues.UNKNOWN;
            try
            {
                string URL = SharedValues.GEO_API_URL_LATLANG + longitude + "," + latitude + SharedValues.GEO_API_URL_KEY_PARAM + SharedValues.GL_GEO_API_KEY;
                string response = GET.DoRequest(URL);

                IParser geocodeResponseParser = new GeocodeResponseParser();
                string formattedAddress = geocodeResponseParser.ExtractData(response);
                localeInfo = geocodeResponseParser.ExtractSpecificInfo(formattedAddress, SharedValues.GEO_INFO_CITY, ',');
                if (localeInfo.Equals(SharedValues.UNKNOWN)) localeInfo = geocodeResponseParser.ExtractSpecificInfo(formattedAddress, SharedValues.GEO_INFO_COUNTRY, ','); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return localeInfo;
        }
        #endregion
    }
}
