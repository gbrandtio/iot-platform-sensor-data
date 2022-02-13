using RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Helpers;

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
    /// Provides also the ability to find a city based on the provided longitude and latitude via Google Geocoding API.
    /// </summary>
    public class Location
    {
        #region Constructor
        public Location() {}
        public Location(double longitude, double latitude, string country)
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Country = country;
            this.City = FindCity(this.Longitude, this.Latitude);
        }
        #endregion

        #region Properties
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Country { get; set; }

        public string City { get; set; }
        #endregion

        #region Private Methods
        private string FindCity(double longitude, double latitude)
        {
            string city = String.Empty;
            try
            {
                string URL = SharedValues.GEO_API_URL_LATLANG + longitude + "," + latitude + SharedValues.URL_KEY_PARAM + SharedValues.GL_GEO_API_KEY;
                string response = GET.DoRequest(URL);
                string address = Parser.GeocodeRes_ExtractFormattedAddress(response);
                city = ExtractInfo(address, 1);
                if (city.Equals(SharedValues.UNKNOWN)) city = ExtractInfo(address, 2); 
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return city;
        }

        private string ExtractInfo(string address, int pos)
        {
            string requestedAddressComponent = SharedValues.UNKNOWN;
            try
            {
                string[] addrArray = address.Split(',');
                requestedAddressComponent = addrArray[pos];
            }
            catch (Exception e)
            {

            }
            return requestedAddressComponent;
        }
        #endregion
    }
}
