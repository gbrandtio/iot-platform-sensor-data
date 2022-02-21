using Interfaces;
using Models.Config_Models;
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
            string requestedAddressComponent = Strings.String.Unknown.Value;
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
            string formattedAddress = Strings.String.Unknown.Value;
            try
            {
                JObject jResponse = JObject.Parse(response);
                JArray jResultsArray = (JArray)jResponse[Strings.String.Results.Value];
                formattedAddress = jResultsArray[0][Strings.Geocode.FormattedAddress.Value].ToString();
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
            string localeInfo = Strings.String.Unknown.Value;
            try
            {
                string URL = Strings.Geocode.GeocodeURL.Value + longitude + "," + latitude + Strings.Geocode.URLKeyParam + Strings.Config.GeoApiKey;
                string response = GET.DoRequest(URL);

                localeInfo = ExtractData(response, Strings.Geocode.InfoCity.ValueInt, ',');
                if (localeInfo.Equals(Strings.String.Unknown.Value)) localeInfo = ExtractData(response, Strings.Geocode.InfoCountry.ValueInt, ',');
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
