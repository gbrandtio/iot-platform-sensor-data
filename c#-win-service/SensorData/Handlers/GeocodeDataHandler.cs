using Helpers;
using Interfaces;
using Newtonsoft.Json.Linq;
using RestClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handlers
{
    public class GeocodeDataHandler
    {
        #region Parsing Methods
        /// <summary>
        /// Parses the Geocode API JSON response and extracts the specified info from the formatted_address field.
        /// </summary>
        /// <param name="json">The Geocode JSON API response</param>
        /// <returns>The requested info from the formatted address</returns>
        public string ExtractData(string json, int pos, char delimeter) 
        {
            string formattedAddress = ExtractFormattedAddress(json);
            string requestedAddressComponent = SharedValues.UNKNOWN;
            try
            {
                string[] addrArray = formattedAddress.Split(delimeter);
                requestedAddressComponent = addrArray[pos];
            }
            catch (Exception e)
            {

            }
            return requestedAddressComponent;
        }

        /// <summary>
        /// Parses the JSON Geocode API response and extracts the formatted_address field value.
        /// </summary>
        /// <param name="response">Geocode API response</param>
        /// <returns>The formatted_address value as a string</returns>
        private string ExtractFormattedAddress(string response)
        {
            string formattedAddress = SharedValues.UNKNOWN;
            try
            {
                JObject jResponse = JObject.Parse(response);
                JArray jResultsArray = (JArray)jResponse[SharedValues.ARRAY_RESULTS];
                formattedAddress = jResultsArray[0][SharedValues.FORMATTED_ADDRESS].ToString();
            }
            catch (Exception e)
            {

            }
            return formattedAddress;
        }
        #endregion
        public void AddLocationInfo(List<IMeasurement> allMeasurements)
        {
            foreach (IMeasurement measurement in allMeasurements)
            {
                if (measurement != null)
                    measurement.Location.City = FindLocationInfo(measurement.Location.Longitude, measurement.Location.Latitude);
            }
        }
        #region Data Handling Methods

        #endregion

        #region Reverse Geocoding
        public string FindLocationInfo(double longitude, double latitude)
        {
            string localeInfo = SharedValues.UNKNOWN;
            try
            {
                string URL = SharedValues.GEO_API_URL_LATLANG + longitude + "," + latitude + SharedValues.GEO_API_URL_KEY_PARAM + SharedValues.GL_GEO_API_KEY;
                string response = GET.DoRequest(URL);

                localeInfo = ExtractData(response, SharedValues.GEO_INFO_CITY, ',');
                if (localeInfo.Equals(SharedValues.UNKNOWN)) localeInfo = ExtractData(response, SharedValues.GEO_INFO_COUNTRY, ',');
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
